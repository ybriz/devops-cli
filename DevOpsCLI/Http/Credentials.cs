// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Http
{
    using Jmelosegui.DevOpsCLI.Authentication;
    using Jmelosegui.DevOpsCLI.Helpers;

    public class Credentials
    {
        public static readonly Credentials Anonymous = new Credentials();

        public Credentials(string login, string password)
        {
            Ensure.ArgumentNotNullOrEmptyString(password, nameof(password));

            this.Login = login;
            this.Password = password;
            this.AuthenticationType = AuthenticationType.Basic;
        }

        private Credentials()
        {
            this.AuthenticationType = AuthenticationType.Anonymous;
        }

        public string Login
        {
            get;
        }

        public string Password
        {
            get;
        }

        public AuthenticationType AuthenticationType
        {
            get;
        }
    }
}