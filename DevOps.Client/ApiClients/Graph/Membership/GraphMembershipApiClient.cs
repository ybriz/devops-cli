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
        private const string MembershipEndPoint = "_apis/graph/Memberships";

        /// <summary>
        /// Retrieve Memberships.
        /// </summary>
        /// <param name="request">Payload used in the request <see cref="GraphMembershipListRequest"/>.</param>
        /// <returns>IEnumerable of <see cref="GraphMembership"/>.</returns>
        public async Task<IEnumerable<GraphMembership>> MembershipGetAllAsync(GraphMembershipListRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(request.SubjectDescriptor, nameof(request.SubjectDescriptor));

            var parameters = new Dictionary<string, object>
            {
                { "api-version", "6.0-preview.1" },
            };

            if (request != null)
            {
                FluentDictionary.For(parameters)
                            .Add("direction", request.Direction, () => !string.IsNullOrEmpty(request.Direction));
            }

            var response = await this.Connection.Get<GenericCollectionResponse<GraphMembership>>(new Uri($"{MembershipEndPoint}/{request.SubjectDescriptor}", UriKind.Relative), parameters, null, CancellationToken.None, this.BaseUrl)
                                                .ConfigureAwait(false);

            return response.Body.Values;
        }
    }
}
