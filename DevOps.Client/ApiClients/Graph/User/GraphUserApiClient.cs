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
        private const string UserEndPoint = "_apis/graph/users";

        /// <summary>
        /// Retrieve Users.
        /// </summary>
        /// <param name="request">Payload used in the request <see cref="GraphUserListRequest"/>.</param>
        /// <returns>IEnumerable of <see cref="GraphUser"/>.</returns>
        public async Task<IEnumerable<GraphUser>> UserGetAllAsync(GraphUserListRequest request)
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

            var response = await this.Connection.Get<GenericCollectionResponse<GraphUser>>(new Uri($"{UserEndPoint}", UriKind.Relative), parameters, null, CancellationToken.None, this.BaseUrl)
                                                .ConfigureAwait(false);

            return response.Body.Values;
        }

        public async Task<GraphUser> UserGetAsync(string userDescriptor)
        {
            var parameters = new Dictionary<string, object>();

            FluentDictionary.For(parameters)
                            .Add("api-version", "6.0-preview.1")
                            .Add("userDescriptor", userDescriptor);

            var response = await this.Connection.Get<GraphUser>(new Uri($"{UserEndPoint}", UriKind.Relative), parameters, null, CancellationToken.None, this.BaseUrl)
                                                .ConfigureAwait(false);

            return response.Body;
        }
    }
}
