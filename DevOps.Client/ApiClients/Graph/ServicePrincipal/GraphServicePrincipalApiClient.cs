// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public partial class GraphApiClient
    {
        private const string ServicePrincipalEndPoint = "_apis/graph/serviceprincipals";

        /// <summary>
        /// Retrieve a Service Principal.
        /// </summary>
        /// <param name="request">Payload used in the request <see cref="GraphServicePrincipalListRequest"/>.</param>
        /// <returns>IEnumerable of <see cref="GraphServicePrincipal"/>.</returns>
        public async Task<IEnumerable<GraphServicePrincipal>> ServicePrincipalGetAllAsync(GraphServicePrincipalListRequest request)
        {
            var parameters = new Dictionary<string, object>
            {
                { "api-version", "7.1-preview.1" },
            };

            if (request != null)
            {
                FluentDictionary.For(parameters)
                            .Add("scopeDescriptor", request.ScopeDescriptor, () => !string.IsNullOrEmpty(request.ScopeDescriptor));
            }

            var response = await this.Connection.Get<GenericCollectionResponse<GraphServicePrincipal>>(new Uri($"{ServicePrincipalEndPoint}", UriKind.Relative), parameters, null, CancellationToken.None, this.BaseUrl)
                                                .ConfigureAwait(false);

            return response.Body.Values;
        }

        public async Task<GraphServicePrincipal> ServicePrincipalGetAsync(string servicePrincipalDescriptor)
        {
            var parameters = new Dictionary<string, object>();

            FluentDictionary.For(parameters)
                            .Add("api-version", "7.1-preview.1")
                            .Add("servicePrincipalDescriptor", servicePrincipalDescriptor);

            var response = await this.Connection.Get<GraphServicePrincipal>(new Uri($"{ServicePrincipalEndPoint}", UriKind.Relative), parameters, null, CancellationToken.None, this.BaseUrl)
                                                .ConfigureAwait(false);

            return response.Body;
        }
    }
}
