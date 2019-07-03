using System;
using System.Collections.Concurrent;
using System.IO;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Tests")]
namespace Network.Transport.Tcp
{
    public class ReceivingStream
    {
        public bool HasMessage => !CompletedMessages.IsEmpty;
        internal bool HasIncompleteMessage => !IncompleteMessages.IsEmpty;

        ConcurrentQueue<byte[]> IncompleteMessages;
        ConcurrentQueue<byte[]> CompletedMessages;

        public ReceivingStream()
        {
            CompletedMessages = new ConcurrentQueue<byte[]>();
            IncompleteMessages = new ConcurrentQueue<byte[]>();
        }

        public void ReadMessage(byte[] buffer)
        {
            if (buffer == null) throw new IncorrectMessageFormat();

            if (buffer.Length < Data.PrefixLength)
            {
                AddToPartialMessageQueue(buffer);
                return;
            }

            if (HasIncompleteMessage)
            {
                AddToPartialMessageQueue(buffer);
                return;
            }

            using (var stream = new MemoryStream(buffer))
                TryReadMessages(stream);
        }

        void AddToPartialMessageQueue(byte[] buffer)
        {
            using (var stream = new MemoryStream())
            {
                //Fetch previously saved data
                while (HasIncompleteMessage)
                {
                    IncompleteMessages.TryDequeue(out var data);
                    stream.Write(data, (int)stream.Position, data.Length);
                }
                //adding other part of data buffer to the stream
                stream.Write(buffer, 0, buffer.Length);

                //Reset position to Beginning after all the data was added to the stream
                stream.Position = 0;

                TryReadMessages(stream);
            }
        }

        void TryReadMessages(MemoryStream stream)
        {
            while (stream.Position < stream.Length)
            {
                var size = stream.Length - stream.Position;
                if (size < Data.PrefixLength)
                {
                    using (var result = new MemoryStream())
                    {
                        stream.CopyTo(result, (int)size);
                        IncompleteMessages.Enqueue(result.ToArray());
                    }
                    return;
                }

                var length = GetMessageSize(stream);

                if (length <= 0) throw new IncorrectMessageFormat();
                if (length > stream.Length - stream.Position)
                {
                    using (var result = new MemoryStream())
                    {
                        stream.Seek(-Data.PrefixLength, SeekOrigin.Current);
                        stream.CopyTo(result, (int)size + Data.PrefixLength);
                        IncompleteMessages.Enqueue(result.ToArray());
                    }
                    return;
                }

                var data = new byte[length];
                stream.Read(data, 0, length);

                CompletedMessages.Enqueue(data);
            }
        }

        public byte[] GetMessage()
        {
            byte[] data = null;

            if (HasMessage)
                CompletedMessages.TryDequeue(out data);

            return data;
        }

        static int GetMessageSize(Stream stream)
        {
            var length = new byte[Data.PrefixLength];
            stream.Read(length, 0, Data.PrefixLength);
            return BitConverter.ToInt32(length, 0);
        }

        public class IncorrectMessageFormat : Exception { }
    }
}
