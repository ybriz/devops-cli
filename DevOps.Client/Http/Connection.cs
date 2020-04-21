// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    public class Connection : IConnection
    {
        private readonly Authenticator authenticator;
        private readonly HttpClient httpClient;
        private readonly JsonHttpPipeline jsonPipeline;

        public Connection(Uri serviceUrl)
            : this(serviceUrl, Credentials.Anonymous)
        {
        }

        public Connection(Uri serviceUrl, Credentials credentials)
        {
            Ensure.ArgumentNotNull(serviceUrl, nameof(serviceUrl));
            Ensure.ArgumentNotNull(credentials, nameof(credentials));

            this.ServiceUrl = serviceUrl;

            this.authenticator = new Authenticator(credentials);
            this.jsonPipeline = new JsonHttpPipeline();
            this.httpClient = new HttpClient();
        }

        public Uri ServiceUrl { get; }

        public Task<IApiResponse<T>> Get<T>(Uri uri, IDictionary<string, object> parameters, string accepts)
        {
            Ensure.ArgumentNotNull(uri, nameof(uri));

            return this.SendData<T>(uri.ApplyParameters(parameters), HttpMethod.Get, null, accepts, null, CancellationToken.None);
        }

        public Task<IApiResponse<T>> Patch<T>(Uri uri, object body, IDictionary<string, object> parameters, string accepts)
        {
            Ensure.ArgumentNotNull(uri, nameof(uri));
            Ensure.ArgumentNotNull(body, nameof(body));

            return this.SendData<T>(uri.ApplyParameters(parameters), HttpMethod.Patch, body, accepts, null, CancellationToken.None);
        }

        public async Task<HttpStatusCode> Patch(Uri uri, object body, IDictionary<string, object> parameters, string accepts)
        {
            Ensure.ArgumentNotNull(uri, nameof(uri));
            Ensure.ArgumentNotNull(body, nameof(body));

            var request = new Request
            {
                Method = HttpMethod.Patch,
                BaseAddress = this.ServiceUrl,
                Endpoint = uri.ApplyParameters(parameters),
                ContentType = "application/json",
            };

            if (!string.IsNullOrEmpty(accepts))
            {
                request.Headers["Accept"] = accepts;
            }

            if (body != null)
            {
                request.Body = body;
                this.jsonPipeline.SerializeRequest(request);
            }

            var response = await this.Run<object>(request, CancellationToken.None).ConfigureAwait(false);
            return response.HttpResponse.StatusCode;
        }

        public Task<IApiResponse<T>> Put<T>(Uri uri, object body, IDictionary<string, object> parameters, string accepts)
        {
            Ensure.ArgumentNotNull(uri, nameof(uri));
            Ensure.ArgumentNotNull(body, nameof(body));

            return this.SendData<T>(uri.ApplyParameters(parameters), HttpMethod.Put, body, accepts, null, CancellationToken.None);
        }

        public Task<IApiResponse<T>> Post<T>(Uri uri, object body, IDictionary<string, object> parameters, string accepts)
        {
            Ensure.ArgumentNotNull(uri, nameof(uri));
            Ensure.ArgumentNotNull(body, nameof(body));

            return this.SendData<T>(uri.ApplyParameters(parameters), HttpMethod.Post, body, accepts, null, CancellationToken.None);
        }

        public async Task<HttpStatusCode> Delete(Uri uri)
        {
            Ensure.ArgumentNotNull(uri, nameof(uri));

            var request = new Request
            {
                Method = HttpMethod.Delete,
                BaseAddress = this.ServiceUrl,
                Endpoint = uri,
            };

            var response = await this.Run<object>(request, CancellationToken.None).ConfigureAwait(false);
            return response.HttpResponse.StatusCode;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.httpClient != null)
                {
                    this.httpClient.Dispose();
                }
            }
        }

        private static Dictionary<HttpStatusCode, Func<IResponse, Exception>> HttpExceptionMap()
        {
            return new Dictionary<HttpStatusCode, Func<IResponse, Exception>>
            {
                { HttpStatusCode.Unauthorized, response => new AuthorizationException(response) },
                { HttpStatusCode.Forbidden, response => new ForbiddenException(response) },
                { HttpStatusCode.NotFound, response => new NotFoundException(response) },
            };
        }

        private static void HandleErrors(IResponse response)
        {
            Func<IResponse, Exception> exceptionFunc;
            if (HttpExceptionMap().TryGetValue(response.StatusCode, out exceptionFunc))
            {
                throw exceptionFunc(response);
            }

            if ((int)response.StatusCode >= 400)
            {
                throw new ApiException(response);
            }
        }

        private Task<IApiResponse<T>> SendData<T>(
            Uri uri,
            HttpMethod method,
            object body,
            string accepts,
            string contentType,
            CancellationToken cancellationToken,
            string twoFactorAuthenticationCode = null,
            Uri serviceUrl = null)
        {
            Ensure.ArgumentNotNull(uri, nameof(uri));

            var request = new Request
            {
                Method = method,
                BaseAddress = serviceUrl ?? this.ServiceUrl,
                Endpoint = uri,
            };

            return this.SendDataInternal<T>(body, accepts, contentType, cancellationToken, twoFactorAuthenticationCode, request);
        }

        private Task<IApiResponse<T>> SendDataInternal<T>(object body, string accepts, string contentType, CancellationToken cancellationToken, string twoFactorAuthenticationCode, Request request)
        {
            if (!string.IsNullOrEmpty(accepts))
            {
                request.Headers["Accept"] = accepts;
            }

            if (body != null)
            {
                request.Body = body;
                request.ContentType = contentType ?? "application/json";
            }

            return this.Run<T>(request, cancellationToken);
        }

        private async Task<IApiResponse<T>> Run<T>(IRequest request, CancellationToken cancellationToken)
        {
            this.jsonPipeline.SerializeRequest(request);
            var response = await this.RunRequest(request, cancellationToken).ConfigureAwait(false);
            return this.jsonPipeline.DeserializeResponse<T>(response);
        }

        private async Task<IResponse> RunRequest(IRequest request, CancellationToken cancellationToken)
        {
            this.authenticator.Apply(request);

            HttpResponseMessage responseMessage = await this.httpClient.SendAsync(request.ToHttpMessage(), cancellationToken).ConfigureAwait(false);
            Response response = await Response.FromHttpResponseMessage(responseMessage);
            HandleErrors(response);
            return response;
        }
    }
}