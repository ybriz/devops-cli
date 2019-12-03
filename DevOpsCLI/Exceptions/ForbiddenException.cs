// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Exceptions
{
    using System;
    using System.Diagnostics;
    using System.Net;
    using Jmelosegui.DevOpsCLI.Http;

    public class ForbiddenException : ApiException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ForbiddenException"/> class.
        /// Constructs an instance of ForbiddenException.
        /// </summary>
        /// <param name="response">The HTTP payload from the server.</param>
        public ForbiddenException(IResponse response)
            : this(response, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ForbiddenException"/> class.
        /// Constructs an instance of ForbiddenException.
        /// </summary>
        /// <param name="response">The HTTP payload from the server.</param>
        /// <param name="innerException">The inner exception.</param>
        public ForbiddenException(IResponse response, Exception innerException)
            : base(response, innerException)
        {
            Debug.Assert(
                response != null && response.StatusCode == HttpStatusCode.Forbidden,
                "ForbiddenException created with wrong status code");
        }

        public override string Message
        {
            get { return "Request Forbidden"; }
        }
    }
}
