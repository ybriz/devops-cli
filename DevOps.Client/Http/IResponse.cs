// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client
{
    using System.Collections.Generic;
    using System.Net;

    /// <summary>
    /// Represents a generic HTTP response.
    /// </summary>
    public interface IResponse
    {
        /// <summary>
        /// Gets raw response body. Typically a string, but when requesting files (i.e certificates), it will be a byte array.
        /// </summary>
        object Body { get; }

        /// <summary>
        /// Gets information about the API.
        /// </summary>
        IReadOnlyDictionary<string, string> Headers { get; }

        /// <summary>
        /// Gets the response status code.
        /// </summary>
        HttpStatusCode StatusCode { get; }

        /// <summary>
        /// Gets the content type of the response.
        /// </summary>
        string ContentType { get; }
    }

    /// <summary>
    /// A response from an API call that includes the deserialized object instance.
    /// </summary>
    public interface IApiResponse<out T>
    {
        /// <summary>
        /// Gets object deserialized from the JSON response body.
        /// </summary>
        T Body { get; }

        /// <summary>
        /// Gets the original non-deserialized http response.
        /// </summary>
        IResponse HttpResponse { get; }
    }
}
