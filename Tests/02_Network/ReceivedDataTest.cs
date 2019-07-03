using System;
using System.Linq;
using System.Text;
using Network.Transport.Tcp;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class ReceivedDataTest
    {
        ReceivingStream stream;

        [SetUp]
        public void Setup()
        {
            stream = new ReceivingStream();
        }

        [TestCase(1025)]
        [TestCase(374580)]
        public void CanReadMessage(int initialData)
        {
            var bytes = BitConverter.GetBytes(initialData);
            var data = BitConverter.GetBytes(bytes.Length);
            var buffer = new byte[data.Length + bytes.Length];

            Buffer.BlockCopy(data, 0, buffer, 0, data.Length);
            Buffer.BlockCopy(bytes, 0, buffer, data.Length, bytes.Length);

            stream.ReadMessage(buffer);

            Assert.IsTrue(stream.HasMessage);
            Assert.AreEqual(initialData, BitConverter.ToInt32(stream.GetMessage(), 0));
            Assert.IsFalse(stream.HasMessage);
        }

        [Test]
        public void CanRead_TwoMessagesInOneReceivedBuffer()
        {
            const string msg1 = "T";
            const string msg2 = "Test111";
            var data1 = Data.AddSizeToMessage(Encoding.UTF8.GetBytes(msg1));
            var data2 = Data.AddSizeToMessage(Encoding.UTF8.GetBytes(msg2));

            var buffer = new byte[data1.Length + data2.Length];

            Buffer.BlockCopy(data1, 0, buffer, 0, data1.Length);
            Buffer.BlockCopy(data2, 0, buffer, data1.Length, data2.Length);

            stream.ReadMessage(buffer);

            Assert.IsTrue(stream.HasMessage);
            Assert.AreEqual(msg1, Encoding.UTF8.GetString(stream.GetMessage()));
            Assert.IsTrue(stream.HasMessage);
            Assert.AreEqual(msg2, Encoding.UTF8.GetString(stream.GetMessage()));
            Assert.IsFalse(stream.HasMessage);
        }

        [Test]
        public void OneMessageInsideProtocol_AnotherAdded_CanGetBothMessages()
        {
            const string msg1 = "Tet";
            const string msg2 = "Test111";
            var data1 = Data.AddSizeToMessage(Encoding.UTF8.GetBytes(msg1));
            var data2 = Data.AddSizeToMessage(Encoding.UTF8.GetBytes(msg2));

            stream.ReadMessage(data1);
            stream.ReadMessage(data2);

            Assert.IsTrue(stream.HasMessage);
            Assert.AreEqual(msg1, Encoding.UTF8.GetString(stream.GetMessage()));
            Assert.IsTrue(stream.HasMessage);
            Assert.AreEqual(msg2, Encoding.UTF8.GetString(stream.GetMessage()));
            Assert.IsFalse(stream.HasMessage);
        }

        [Test]
        public void OneBigMessage_CanBeAddedAndRead()
        {
            var message = new string('$', 65537);

            var data = Data.AddSizeToMessage(Encoding.UTF8.GetBytes(message));

            stream.ReadMessage(data);
            Assert.IsTrue(stream.HasMessage);
            Assert.AreEqual(message, Encoding.UTF8.GetString(stream.GetMessage()));
        }

        [TestCase(new byte[] { 15, 0, 1, 2 })]
        [TestCase(new byte[] { 10, 0, 2, 5 })]
        public void OnlyLengthArrived_HasPartialMessage(byte[] data)
        {
            stream.ReadMessage(data);

            Assert.IsFalse(stream.HasMessage);
            Assert.IsTrue(stream.HasIncompleteMessage);
        }

        [Test]
        public void CanContinuouslyRead()
        {
            const int firstDataChunk = 64123;
            const int secondDataChunk = -1987652;

            var firstData = BitConverter.GetBytes(firstDataChunk);
            var firstLength = BitConverter.GetBytes(firstData.Length);
            var secondData = BitConverter.GetBytes(secondDataChunk);
            var secondLength = BitConverter.GetBytes(secondData.Length);

            var buffer = new byte[firstLength.Length + firstData.Length - 1];

            Buffer.BlockCopy(firstLength, 0, buffer, 0, firstLength.Length);
            Buffer.BlockCopy(firstData, 0, buffer, firstLength.Length, firstData.Length - 1);

            stream.ReadMessage(buffer);
            Assert.IsFalse(stream.HasMessage);
            Assert.IsTrue(stream.HasIncompleteMessage);

            var restFirstAndSecondBuffers = new byte[1 + secondLength.Length + secondData.Length];
            Buffer.BlockCopy(firstData, firstData.Length - 1, restFirstAndSecondBuffers, 0, 1);
            Buffer.BlockCopy(secondLength, 0, restFirstAndSecondBuffers, 1, secondLength.Length);
            Buffer.BlockCopy(secondData, 0, restFirstAndSecondBuffers, 1 + secondLength.Length, secondData.Length);

            stream.ReadMessage(restFirstAndSecondBuffers);

            Assert.IsTrue(stream.HasMessage);
            Assert.AreEqual(firstDataChunk, BitConverter.ToInt32(stream.GetMessage(), 0));
            Assert.IsTrue(stream.HasMessage);
            Assert.AreEqual(secondDataChunk, BitConverter.ToInt32(stream.GetMessage(), 0));

            Assert.IsFalse(stream.HasMessage);
            Assert.IsFalse(stream.HasIncompleteMessage);
        }
        [Test]
        public void OnePartialMessageInsideProtocol_BufferWithThatCompletesAdded_CanGetBothMessages()
        {
            const string msg1 = "Test";
            const string msg2 = "Impl111";
            var data1 = Data.AddSizeToMessage(Encoding.UTF8.GetBytes(msg1));
            var data2 = Data.AddSizeToMessage(Encoding.UTF8.GetBytes(msg2));

            var firstChunk = new byte[data1.Length - 1];
            var secondChunk = new byte[data2.Length + 1];
            Buffer.BlockCopy(data1, 0, firstChunk, 0, data1.Length - 1);
            Buffer.BlockCopy(data1, data1.Length - 1, secondChunk, 0, 1);
            Buffer.BlockCopy(data2, 0, secondChunk, 1, data2.Length);

            stream.ReadMessage(firstChunk);
            stream.ReadMessage(secondChunk);

            Assert.IsTrue(stream.HasMessage);
            Assert.AreEqual(msg1, Encoding.UTF8.GetString(stream.GetMessage()));
            Assert.IsTrue(stream.HasMessage);
            Assert.AreEqual(msg2, Encoding.UTF8.GetString(stream.GetMessage()));
            Assert.IsFalse(stream.HasMessage);
        }

        [Test]
        public void PartialLengthArrived_InTwoChunks_CanGetMessages()
        {
            var message1 = new string('!', 16);
            var message2 = "Test!";

            var data1 = Data.AddSizeToMessage(Encoding.UTF8.GetBytes(message1));
            var data2 = Data.AddSizeToMessage(Encoding.UTF8.GetBytes(message2));

            var firstChunk = new byte[3]; // 3 bytes of the length of the message1
            var secondChunk = new byte[data1.Length]; // first data sent and 3 bytes of the message2 length
            var thirdChunk = new byte[data2.Length - 3]; // rest of the message2 sent
            Buffer.BlockCopy(data1, 0, firstChunk, 0, 3);

            stream.ReadMessage(firstChunk);
            Assert.IsFalse(stream.HasMessage);
            Assert.IsTrue(stream.HasIncompleteMessage);

            Buffer.BlockCopy(data1, 3, secondChunk, 0, data1.Length - 3);
            Buffer.BlockCopy(data2, 0, secondChunk, data1.Length - 3, 3);

            stream.ReadMessage(secondChunk);
            Assert.IsTrue(stream.HasMessage);
            Assert.AreEqual(message1, Encoding.UTF8.GetString(stream.GetMessage()));
            Assert.IsFalse(stream.HasMessage);
            Assert.IsTrue(stream.HasIncompleteMessage);

            Buffer.BlockCopy(data2, 3, thirdChunk, 0, data2.Length - 3);
            stream.ReadMessage(thirdChunk);
            Assert.IsTrue(stream.HasMessage);
            Assert.AreEqual(message2, Encoding.UTF8.GetString(stream.GetMessage()));
            Assert.IsFalse(stream.HasMessage);
            Assert.IsFalse(stream.HasIncompleteMessage);
        }

        [Test]
        public void MessageArrivesByteByByte_CanBeRead()
        {
            var message = "11";
            var data = Data.AddSizeToMessage(Encoding.UTF8.GetBytes(message));
            var firstChunk = new byte[1];
            var secondChunk = new byte[1];
            var thirdChunk = new byte[1];
            var forthChunk = new byte[1];
            var fifthChunk = new byte[1];
            var sixthChunk = new byte[1];

            Buffer.BlockCopy(data, 0, firstChunk, 0, 1);
            stream.ReadMessage(firstChunk);
            Assert.IsFalse(stream.HasMessage);
            Assert.IsTrue(stream.HasIncompleteMessage);

            Buffer.BlockCopy(data, 1, secondChunk, 0, 1);
            stream.ReadMessage(secondChunk);
            Assert.IsFalse(stream.HasMessage);
            Assert.IsTrue(stream.HasIncompleteMessage);

            Buffer.BlockCopy(data, 2, thirdChunk, 0, 1);
            stream.ReadMessage(thirdChunk);
            Assert.IsFalse(stream.HasMessage);
            Assert.IsTrue(stream.HasIncompleteMessage);

            Buffer.BlockCopy(data, 3, forthChunk, 0, 1);
            stream.ReadMessage(forthChunk);
            Assert.IsFalse(stream.HasMessage);
            Assert.IsTrue(stream.HasIncompleteMessage);

            Buffer.BlockCopy(data, 4, fifthChunk, 0, 1);
            stream.ReadMessage(fifthChunk);
            Assert.IsFalse(stream.HasMessage);
            Assert.IsTrue(stream.HasIncompleteMessage);

            Buffer.BlockCopy(data, 5, sixthChunk, 0, 1);
            stream.ReadMessage(sixthChunk);
            Assert.IsTrue(stream.HasMessage);
            Assert.IsFalse(stream.HasIncompleteMessage);
            Assert.AreEqual(message, Encoding.UTF8.GetString(stream.GetMessage()));
        }

        [TestCase(new byte[] { 0 })]
        [TestCase(new byte[] { 0, 0 })]
        [TestCase(new byte[] { 0, 0, 0 })]
        [TestCase(new byte[] { 0, 0, 1 })]
        public void ReceivedOnlyPartOfTheLength_HasPartialMessage(byte[] data)
        {
            stream.ReadMessage(data);

            Assert.IsFalse(stream.HasMessage, "Should not detect message");
            Assert.IsTrue(stream.HasIncompleteMessage, "Should have partial message");
        }

        [TestCase(null)]
        [TestCase(new byte[] { 0, 0, 0, 0, 1 })] // zero length
        [TestCase(new byte[] { 255, 0, 0, 255 })] // negative length
        public void IncorrectlyFormedMessage_Throws(byte[] data)
        {
            Assert.Throws<ReceivingStream.IncorrectMessageFormat>(() => stream.ReadMessage(data));
        }

        [Test]
        public void CanReceiveHundredMessages_InSequence()
        {
            var array = Enumerable.Range(0, 100).ToArray();
            foreach (var item in array)
            {
                var bytes = Encoding.UTF8.GetBytes(item.ToString());
                var message = Data.AddSizeToMessage(bytes);
                stream.ReadMessage(message);
            }

            var index = -1;
            var actual = new string[array.Length];
            while (stream.HasMessage)
            {
                actual[++index] = Encoding.UTF8.GetString(stream.GetMessage());
            }

            Assert.AreEqual(array.Length, actual.Length);
            for (var i = 0; i < array.Length - 1; i++)
            {
                Assert.IsTrue(int.Parse(actual[i]) < int.Parse(actual[i + 1]), $"Should be {actual[i]} < {actual[i + 1]}");
            }
        }
        [TestCase(100, 1025)]
        [TestCase(1000, 65537)]
        public void CanReceiveATonOfBigMessages_InSequence(int messagesAmount, int bytesInMessages)
        {
            for (var i = 0; i < messagesAmount; i++)
            {
                var data = new string('1', bytesInMessages);
                var bytes = Encoding.UTF8.GetBytes(data);
                var message = Data.AddSizeToMessage(bytes);
                stream.ReadMessage(message);
            }

            var index = -1;
            var actual = new string[messagesAmount];
            while (stream.HasMessage)
            {
                actual[++index] = Encoding.UTF8.GetString(stream.GetMessage());
            }

            Assert.AreEqual(messagesAmount, actual.Length, "Count of read messages from protocol is different than amount of messages added to protocol");
            Assert.IsTrue(actual.All(m => m.Length == bytesInMessages), "The message has unexpected length");
        }

        [Test]
        public void CanReadBigMessage_WhenLast_IsPartial()
        {
            var data = new string('}', 103);
            var bytes = Encoding.UTF8.GetBytes(data);
            var message = Data.AddSizeToMessage(bytes);

            var buffer = new byte[512];
            Buffer.BlockCopy(message, 0, buffer, 0, message.Length);
            Buffer.BlockCopy(message, 0, buffer, message.Length, message.Length);
            Buffer.BlockCopy(message, 0, buffer, message.Length * 2, message.Length);
            Buffer.BlockCopy(message, 0, buffer, message.Length * 3, message.Length);
            Buffer.BlockCopy(message, 0, buffer, message.Length * 4, message.Length - 23);

            stream.ReadMessage(buffer);
            
            for (var i = 0; i < 4; i++)
            {
                Assert.IsTrue(stream.HasMessage);
                Assert.AreEqual(103, stream.GetMessage().Length);
            }
            Assert.IsFalse(stream.HasMessage);
            Assert.IsTrue(stream.HasIncompleteMessage);
            
            var completion = new byte[23];
            Buffer.BlockCopy(message, message.Length - 23, completion, 0, 23);
            stream.ReadMessage(completion);
            
            Assert.AreEqual(103, stream.GetMessage().Length);
            Assert.IsFalse(stream.HasMessage);
            Assert.IsFalse(stream.HasIncompleteMessage);
        }
    }
}
