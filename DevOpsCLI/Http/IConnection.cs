// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Http
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;

    /// <summary>
    /// A connection for making HTTP requests against URI endpoints.
    /// </summary>
    public interface IConnection : IDisposable
    {
        Task<IApiResponse<T>> Get<T>(Uri uri, IDictionary<string, object> parameters, string accepts);

        Task<IApiResponse<T>> Patch<T>(Uri uri, object body, IDictionary<string, object> parameters, string accepts);

        Task<IApiResponse<T>> Put<T>(Uri uri, object body, IDictionary<string, object> parameters, string accepts);

        Task<IApiResponse<T>> Post<T>(Uri uri, object body, IDictionary<string, object> parameters, string accepts);

        Task<HttpStatusCode> Delete(Uri uri);
    }
}
