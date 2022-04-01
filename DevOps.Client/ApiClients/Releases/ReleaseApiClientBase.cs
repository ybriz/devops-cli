// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client
{
    using System;

    public abstract class ReleaseApiClientBase
    {
        public ReleaseApiClientBase(IConnection connection)
        {
            this.Connection = connection;
        }

        /// <summary>
        /// Gets the underlying connection.
        /// </summary>
        public IConnection Connection { get; private set; }

        protected Uri BaseUrl => this.GetBaseUrl();

        private Uri GetBaseUrl()
        {
            if (this.Connection.ServiceUrl.Host == "dev.azure.com")
            {
                var baseUrl = $"https://vsrm.{this.Connection.ServiceUrl.Host}{this.Connection.ServiceUrl.AbsolutePath}";

                return new Uri(baseUrl);
            }

            return null;
        }
    }
}
