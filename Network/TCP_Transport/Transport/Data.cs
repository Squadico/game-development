using System;

namespace Network.Transport
{
    public class Data
    {
        public const int ReceivedBufferLength = 512;
        public const byte PrefixLength = 4;

        public static byte[] AddSizeToMessage(byte[] message)
        {
            var length = BitConverter.GetBytes(message.Length);
            var packet = new byte[message.Length + PrefixLength];
            Buffer.BlockCopy(length, 0, packet, 0, PrefixLength);
            Buffer.BlockCopy(message, 0, packet, PrefixLength, message.Length);
            return packet;
        }
    }
}
