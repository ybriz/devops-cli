// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Authentication
{
    using System.Collections.Generic;
    using Jmelosegui.DevOpsCLI.Helpers;
    using Jmelosegui.DevOpsCLI.Http;

    internal class Authenticator
    {
        private readonly Dictionary<AuthenticationType, IAuthenticationHandler> authenticators =
           new Dictionary<AuthenticationType, IAuthenticationHandler>
           {
                { AuthenticationType.Anonymous, new AnonymousAuthenticator() },
                { AuthenticationType.Basic, new BasicAuthenticator() },
           };

        public Authenticator(Credentials credentials)
        {
            Ensure.ArgumentNotNull(credentials, nameof(credentials));

            this.Credentials = credentials;
        }

        public Credentials Credentials { get; }

        public void Apply(IRequest request)
        {
            Ensure.ArgumentNotNull(request, nameof(request));

            this.authenticators[this.Credentials.AuthenticationType].Authenticate(request, this.Credentials);
        }
    }
}