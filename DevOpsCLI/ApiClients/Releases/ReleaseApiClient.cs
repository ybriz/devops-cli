// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.ApiClients
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Jmelosegui.DevOpsCLI.ApiClients.Releases.Requests;
    using Jmelosegui.DevOpsCLI.Helpers;
    using Jmelosegui.DevOpsCLI.Http;
    using Jmelosegui.DevOpsCLI.Models;

    public sealed class ReleaseApiClient : IReleaseApiClient
    {
        private const string EndPoint = "_apis/release/releases";

        public ReleaseApiClient(IConnection connection)
        {
            this.Connection = connection;
        }

        /// <summary>
        /// Gets the underlying connection.
        /// </summary>
        public IConnection Connection { get; private set; }

        public async Task<Release> CreateAsync(string projectName, CreateReleaseRequest request)
        {
            var parameters = new Dictionary<string, object>();

            FluentDictionary.For(parameters)
                            .Add("api-version", "5.0");

            IApiResponse<Release> result = await this.Connection
                                                     .Post<Release>(new Uri($"{projectName}/{EndPoint}", UriKind.Relative), request, parameters, null)
                                                     .ConfigureAwait(false);

            return result.Body;
        }

        public async Task<IEnumerable<Release>> GetAllAsync(string projectName)
        {
            var parameters = new Dictionary<string, object>
            {
                { "api-version", "5.0" },
            };

            var response = await this.Connection.Get<GenericCollectionResponse<Release>>(new Uri($"{projectName}/{EndPoint}", UriKind.Relative), parameters, null)
                                                .ConfigureAwait(false);

            return response.Body.Values;
        }

        public async Task<string> GetAsync(string projectName, int releaseId)
        {
            var parameters = new Dictionary<string, object>
            {
                { "api-version", "5.0" },
            };

            var endPointUrl = new Uri($"{projectName}/{EndPoint}/{releaseId}", UriKind.Relative);

            var response = await this.Connection
                         .Get<string>(endPointUrl, parameters, null)
                         .ConfigureAwait(false);

            return response.Body;
        }
    }
}