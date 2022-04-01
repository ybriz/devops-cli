// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public partial class IdentityApiClient : IIdentityApiClient
    {
        private const string EndPoint = "_apis/identities";

        public IdentityApiClient(IConnection connection)
        {
            this.Connection = connection;
        }

        /// <summary>
        /// Gets the underlying connection.
        /// </summary>
        public IConnection Connection { get; private set; }

        protected Uri BaseUrl => this.GetBaseUrl();

        public async Task<IEnumerable<Identity>> GetAllAsync(IdentityListRequest request)
        {
            var parameters = new Dictionary<string, object>
            {
                { "api-version", "5.0" },
            };

            if (request != null)
            {
                FluentDictionary.For(parameters)
                            .Add("descriptors", request.Descriptors, () => !string.IsNullOrEmpty(request.Descriptors))
                            .Add("identityIds", request.IdentityIds, () => !string.IsNullOrEmpty(request.IdentityIds))
                            .Add("subjectDescriptors", request.SubjectDescriptors, () => !string.IsNullOrEmpty(request.SubjectDescriptors))
                            .Add("searchFilter", request.SearchFilter, () => !string.IsNullOrEmpty(request.SearchFilter))
                            .Add("filterValue", request.FilterValue, () => !string.IsNullOrEmpty(request.FilterValue))
                            .Add("queryMembership", request.QueryMembership.ToString());
            }

            var response = await this.Connection.Get<GenericCollectionResponse<Identity>>(new Uri($"{EndPoint}", UriKind.Relative), parameters, null)
                                                .ConfigureAwait(false);

            return response.Body.Values;
        }

        private Uri GetBaseUrl()
        {
            if (this.Connection.ServiceUrl.Host == "dev.azure.com")
            {
                var baseUrl = $"https://vssps.{this.Connection.ServiceUrl.Host}{this.Connection.ServiceUrl.AbsolutePath}";

                return new Uri(baseUrl);
            }

            return null;
        }
    }
}
