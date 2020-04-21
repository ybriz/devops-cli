// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client
{
    using System;
    using System.Diagnostics;
    using System.Net;

    public class AuthorizationException : ApiException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationException"/> class.
        /// Constructs an instance of AuthorizationException.
        /// </summary>
        /// <param name="response">The HTTP payload from the server.</param>
        public AuthorizationException(IResponse response)
            : this(response, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationException"/> class.
        /// Constructs an instance of AuthorizationException.
        /// </summary>
        /// <param name="response">The HTTP payload from the server.</param>
        /// <param name="innerException">The inner exception.</param>
        public AuthorizationException(IResponse response, Exception innerException)
            : base(response, innerException)
        {
            Debug.Assert(
                response != null && response.StatusCode == HttpStatusCode.Unauthorized,
                "AuthorizationException created with wrong status code");
        }

        public override string Message
        {
            get { return "Token is invalid or expired"; }
        }
    }
}
