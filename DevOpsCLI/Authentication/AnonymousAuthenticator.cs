// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Authentication
{
    using Jmelosegui.DevOpsCLI.Http;

    internal class AnonymousAuthenticator : IAuthenticationHandler
    {
        public void Authenticate(IRequest request, Credentials credentials)
        {
            // Do nothing. Retain your anonymity.
        }
    }
}