using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System;
using System.Net;

public class ProgressableStreamContent : HttpContent
{
    private const int defaultBufferSize = 4096;
    private readonly HttpContent content;
    private readonly int bufferSize;
    private readonly Action<long, long> progress;

    public ProgressableStreamContent(HttpContent content, Action<long, long> progress, int bufferSize = defaultBufferSize)
    {
        this.content = content ?? throw new ArgumentNullException(nameof(content));
        this.progress = progress ?? throw new ArgumentNullException(nameof(progress));
        this.bufferSize = bufferSize;

        foreach (var header in content.Headers)
            Headers.TryAddWithoutValidation(header.Key, header.Value);
    }

    protected override async Task SerializeToStreamAsync(Stream stream, TransportContext context)
    {
        var buffer = new byte[bufferSize];
        TryComputeLength(out long size);
        var uploaded = 0L;

        using (var inputStream = await content.ReadAsStreamAsync())
        {
            int length;
            while ((length = await inputStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                await stream.WriteAsync(buffer, 0, length);
                uploaded += length;
                progress?.Invoke(uploaded, size);
            }
        }
    }

    protected override bool TryComputeLength(out long length)
    {
        if (content.Headers.ContentLength != null)
        {
            length = content.Headers.ContentLength.Value;
            return true;
        }

        length = -1;
        return false;
    }
}
