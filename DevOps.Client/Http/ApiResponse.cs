// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client
{
    /// <summary>
    /// Wrapper for a response from the API.
    /// </summary>
    /// <typeparam name="T">Payload contained in the response.</typeparam>
    public class ApiResponse<T> : IApiResponse<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiResponse{T}"/> class.
        /// Create a ApiResponse from an existing request.
        /// </summary>
        /// <param name="response">An existing request to wrap.</param>
        public ApiResponse(IResponse response)
            : this(response, GetBodyAsObject(response))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiResponse{T}"/> class.
        /// Create a ApiResponse from an existing request and object.
        /// </summary>
        /// <param name="response">An existing request to wrap.</param>
        /// <param name="bodyAsObject">The payload from an existing request.</param>
        public ApiResponse(IResponse response, T bodyAsObject)
        {
            Ensure.ArgumentNotNull(response, "response");

            this.HttpResponse = response;
            this.Body = bodyAsObject;
        }

        /// <summary>
        /// Gets the payload of the response.
        /// </summary>
        public T Body { get; private set; }

        /// <summary>
        /// Gets the context of the response.
        /// </summary>
        public IResponse HttpResponse { get; private set; }

        private static T GetBodyAsObject(IResponse response)
        {
            var body = response.Body;
            if (body is T)
            {
                return (T)body;
            }

            return default(T);
        }
    }
}
