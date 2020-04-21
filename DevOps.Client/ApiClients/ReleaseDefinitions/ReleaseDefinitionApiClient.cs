// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public sealed class ReleaseDefinitionApiClient : IReleaseDefinitionApiClient
    {
        private const string EndPoint = "_apis/release/definitions";

        public ReleaseDefinitionApiClient(IConnection connection)
        {
            this.Connection = connection;
        }

        /// <summary>
        /// Gets the underlying connection.
        /// </summary>
        public IConnection Connection { get; private set; }

        public async Task<IEnumerable<ReleaseDefinition>> GetAllAsync(string projectName)
        {
            var parameters = new Dictionary<string, object>
            {
                { "api-version", "4.1" },
            };

            var response = await this.Connection.Get<GenericCollectionResponse<ReleaseDefinition>>(new Uri($"{projectName}/{EndPoint}", UriKind.Relative), parameters, null)
                                           .ConfigureAwait(false);

            return response.Body.Values;
        }

        public async Task<string> GetAsync(string projectName, int releaseDefinitionId)
        {
            var parameters = new Dictionary<string, object>
            {
                { "api-version", "4.1" },
            };

            var endPointUrl = new Uri($"{projectName}/{EndPoint}/{releaseDefinitionId}", UriKind.Relative);

            var response = await this.Connection
                         .Get<string>(endPointUrl, parameters, null)
                         .ConfigureAwait(false);

            return response.Body;
        }
    }
}