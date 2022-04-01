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
        private const string GroupEndPoint = "_apis/graph/groups";

        /// <summary>
        /// Retrieve User Groups.
        /// </summary>
        /// <param name="request">Payload used in the request <see cref="GraphGroupListRequest"/>.</param>
        /// <returns>IEnumerable of <see cref="GraphGroup"/>.</returns>
        public async Task<IEnumerable<GraphGroup>> GroupGetAllAsync(GraphGroupListRequest request)
        {
            var parameters = new Dictionary<string, object>
            {
                { "api-version", "6.0-preview.1" },
            };

            if (request != null)
            {
                FluentDictionary.For(parameters)
                            .Add("scopeDescriptor", request.ScopeDescriptor, () => !string.IsNullOrEmpty(request.ScopeDescriptor));
            }

            var response = await this.Connection.Get<GenericCollectionResponse<GraphGroup>>(new Uri($"{GroupEndPoint}", UriKind.Relative), parameters, null, CancellationToken.None, this.BaseUrl)
                                                .ConfigureAwait(false);

            return response.Body.Values;
        }

        public async Task<GraphGroup> GroupGetAsync(string groupDescriptor)
        {
            var parameters = new Dictionary<string, object>();

            FluentDictionary.For(parameters)
                            .Add("api-version", "6.0-preview.1")
                            .Add("groupDescriptor", groupDescriptor);

            var response = await this.Connection.Get<GraphGroup>(new Uri($"{GroupEndPoint}", UriKind.Relative), parameters, null, CancellationToken.None, this.BaseUrl)
                                                .ConfigureAwait(false);

            return response.Body;
        }
    }
}
