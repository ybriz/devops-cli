// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client
{
    using System;
    using System.Diagnostics;
    using System.Text;
    using static System.FormattableString;

    public class BasicAuthenticator : IAuthenticationHandler
    {
        public void Authenticate(IRequest request, Credentials credentials)
        {
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(credentials, nameof(credentials));
            Ensure.ArgumentNotNull(credentials.Login, "credentials.Login");
            Debug.Assert(credentials.Password != null, "It should be impossible for the password to be null");

            byte[] credentialBytes = Encoding.UTF8.GetBytes(Invariant($"{credentials.Login}:{credentials.Password}"));

            var header = Invariant($"Basic {Convert.ToBase64String(credentialBytes)}");

            request.Headers["Authorization"] = header;
        }
    }
}
