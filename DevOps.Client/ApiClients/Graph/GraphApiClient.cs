// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client
{
    using System;

    public partial class GraphApiClient : IGraphApiClient
    {
        public GraphApiClient(IConnection connection)
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
            if (this.Connection.ServiceUrl.Host != "dev.azure.com")
            {
                throw new InvalidOperationException("Graph APIs are only available in Azure DevOps. Please ensure your service url is in the format (https://dev.azure.com/{yourorganization})");
            }

            var baseUrl = $"https://vssps.{this.Connection.ServiceUrl.Host}{this.Connection.ServiceUrl.AbsolutePath}";

            return new Uri(baseUrl);
        }
    }
}
