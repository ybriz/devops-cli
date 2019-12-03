// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Http
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Jmelosegui.DevOpsCLI.Helpers;

    public class Response : IResponse
    {
        public Response(HttpStatusCode statusCode, object responseBody, Dictionary<string, string> headers, string contentType)
        {
            this.StatusCode = statusCode;
            this.Body = responseBody;
            this.Headers = headers;
            this.ContentType = contentType;
        }

        public object Body { get; }

        public IReadOnlyDictionary<string, string> Headers { get; }

        public HttpStatusCode StatusCode { get; }

        public string ContentType { get; }

        public static async Task<Response> FromHttpResponseMessage(HttpResponseMessage responseMessage)
        {
            Ensure.ArgumentNotNull(responseMessage, "responseMessage");

            object responseBody = null;
            string contentType = null;

            var binaryContentTypes = new[]
            {
                "application/zip",
                "application/x-gzip",
                "application/octet-stream",
            };

            using (var content = responseMessage.Content)
            {
                if (content != null)
                {
                    contentType = GetContentMediaType(responseMessage.Content);

                    if (contentType != null && (contentType.StartsWith("image/") || binaryContentTypes
                        .Any(item => item.Equals(contentType, StringComparison.OrdinalIgnoreCase))))
                    {
                        responseBody = await responseMessage.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
                    }
                    else
                    {
                        responseBody = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    }
                }
            }

            return new Response(
                responseMessage.StatusCode,
                responseBody,
                responseMessage.Headers.ToDictionary(h => h.Key, h => h.Value.First()),
                contentType);
        }

        private static string GetContentMediaType(HttpContent httpContent)
        {
            if (httpContent.Headers != null && httpContent.Headers.ContentType != null)
            {
                return httpContent.Headers.ContentType.MediaType;
            }

            return null;
        }
    }
}
