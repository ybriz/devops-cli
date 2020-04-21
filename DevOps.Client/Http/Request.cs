// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Text;

    public class Request : IRequest
    {
        public Request()
        {
            this.Headers = new Dictionary<string, string>();
            this.Parameters = new Dictionary<string, string>();
        }

        public object Body { get; set; }

        public Dictionary<string, string> Headers { get; private set; }

        public HttpMethod Method { get; set; }

        public Dictionary<string, string> Parameters { get; private set; }

        public Uri BaseAddress { get; set; }

        public Uri Endpoint { get; set; }

        public string ContentType { get; set; }

        public HttpRequestMessage ToHttpMessage()
        {
            HttpRequestMessage requestMessage = null;
            try
            {
                if (!this.BaseAddress.AbsoluteUri.EndsWith("/"))
                {
                    this.BaseAddress = new Uri($"{this.BaseAddress.AbsoluteUri}/");
                }

                var fullUri = this.Endpoint.IsAbsoluteUri ? this.Endpoint : new Uri(this.BaseAddress, this.Endpoint);
                requestMessage = new HttpRequestMessage(this.Method, fullUri);

                foreach (var header in this.Headers)
                {
                    requestMessage.Headers.Add(header.Key, header.Value);
                }

                var httpContent = this.Body as HttpContent;
                if (httpContent != null)
                {
                    requestMessage.Content = httpContent;
                }

                var body = this.Body as string;
                if (body != null)
                {
                    requestMessage.Content = new StringContent(body, Encoding.UTF8, this.ContentType);
                }

                var bodyStream = this.Body as Stream;
                if (bodyStream != null)
                {
                    requestMessage.Content = new StreamContent(bodyStream);
                    requestMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(this.ContentType);
                }
            }
            catch (Exception)
            {
                if (requestMessage != null)
                {
                    requestMessage.Dispose();
                }

                throw;
            }

            return requestMessage;
        }
    }
}