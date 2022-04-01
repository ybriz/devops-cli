// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// A connection for making HTTP requests against URI endpoints.
    /// </summary>
    public interface IConnection : IDisposable
    {
        Uri ServiceUrl { get; }

        Task<IApiResponse<T>> Get<T>(Uri uri, IDictionary<string, object> parameters, string accepts, CancellationToken cancellationToken = default, Uri baseUrl = null);

        Task<IApiResponse<T>> Patch<T>(Uri uri, object body, IDictionary<string, object> parameters, string accepts, CancellationToken cancellationToken = default, Uri baseUrl = null);

        Task<HttpStatusCode> Patch(Uri uri, object body, IDictionary<string, object> parameters, string accepts, CancellationToken cancellationToken = default, Uri baseUrl = null);

        Task<IApiResponse<T>> Put<T>(Uri uri, object body, IDictionary<string, object> parameters, string accepts, CancellationToken cancellationToken = default, Uri baseUrl = null);

        Task<IApiResponse<T>> Post<T>(Uri uri, object body, IDictionary<string, object> parameters, string accepts, CancellationToken cancellationToken = default, Uri baseUrl = null);

        Task<HttpStatusCode> Delete(Uri uri, CancellationToken cancellationToken = default, Uri baseUrl = null);
    }
}
