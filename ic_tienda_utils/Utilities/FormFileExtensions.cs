using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace ic_tienda_utils.Utilities
{
    public static class FormFileExtensions
    {
        public static IFormFile CreateFormFile(
            this byte[] fileBytes,
            string fileName,
            string contentType = "application/pdf")
        {
            var memoryStream = new MemoryStream(fileBytes);
            return new FormFileImplementation(
                memoryStream,
                0,
                memoryStream.Length,
                null,
                fileName,
                contentType);
        }

        private class FormFileImplementation : IFormFile
        {
            private readonly Stream _stream;
            private readonly string _fileName;
            private readonly string _contentType;

            public FormFileImplementation(
                Stream stream,
                long baseStreamOffset,
                long length,
                string name,
                string fileName,
                string contentType)
            {
                _stream = stream;
                _fileName = fileName;
                _contentType = contentType;
                Headers = new HeaderDictionary();
                Length = length;
                Name = name ?? string.Empty;
            }

            public string ContentType => _contentType;
            public string ContentDisposition => null;
            public IHeaderDictionary Headers { get; }
            public long Length { get; }
            public string Name { get; }
            public string FileName => _fileName;

            public Stream OpenReadStream() => _stream;

            public void CopyTo(Stream target) => _stream.CopyTo(target);

            public Task CopyToAsync(Stream target, CancellationToken cancellationToken = default)
                => _stream.CopyToAsync(target, cancellationToken);
        }
    }
}