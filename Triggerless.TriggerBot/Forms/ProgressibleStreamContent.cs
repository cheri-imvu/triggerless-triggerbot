using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Triggerless.TriggerBot.Forms
{
    public class ProgressableStreamContent : HttpContent
    {
        private const int defaultBufferSize = 4096;
        private readonly Stream content;
        private readonly int bufferSize;
        private readonly Action<long, long> progress;

        public ProgressableStreamContent(Stream content, int bufferSize, Action<long, long> progress)
        {
            this.content = content ?? throw new ArgumentNullException(nameof(content));
            this.bufferSize = bufferSize > 0 ? bufferSize : defaultBufferSize;
            this.progress = progress;
        }

        protected override async Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            var buffer = new byte[bufferSize];
            long size = content.Length;
            long uploaded = 0;

            using (content)
            {
                int bytesRead;
                while ((bytesRead = await content.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    await stream.WriteAsync(buffer, 0, bytesRead);
                    uploaded += bytesRead;
                    progress?.Invoke(uploaded, size);
                }
            }
        }

        protected override bool TryComputeLength(out long length)
        {
            length = content.Length;
            return true;
        }
    }
}
