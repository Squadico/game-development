using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Network.Transport
{
    public static class StreamExtensions
    {
        public static async Task<byte[]> ReadMessageAsync(this NetworkStream stream, CancellationToken token)
        {
            using (var memory = new MemoryStream())
            {
                var buffer = new byte[Data.ReceivedBufferLength];

                var bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, token);
                if (bytesRead == 0)
                    return null;

                memory.Write(buffer, 0, bytesRead);

                while (stream.DataAvailable)
                {
                    bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, token);
                    memory.Write(buffer, 0, bytesRead);
                }

                return memory.ToArray();
            }
        }
    }
}
