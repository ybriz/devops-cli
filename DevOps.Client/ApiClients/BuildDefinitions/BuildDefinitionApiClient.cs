// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public sealed class BuildDefinitionApiClient : IBuildDefinitionApiClient
    {
        private const string EndPoint = "_apis/build/definitions";

        public BuildDefinitionApiClient(IConnection connection)
        {
            this.Connection = connection;
        }

        /// <summary>
        /// Gets the underlying connection.
        /// </summary>
        public IConnection Connection { get; private set; }

        public async Task<string> AddOrUpdateAsync(string projectName, int buildDefinitionId, string jsonBody)
        {
            Ensure.ArgumentNotNullOrEmptyString(projectName, nameof(projectName));

            var parameters = new Dictionary<string, object>
            {
                { "api-version", "4.1" },
            };
            Uri endPointUrl;
            IApiResponse<string> response;

            if (buildDefinitionId > 0)
            {
                endPointUrl = new Uri($"{projectName}/{EndPoint}/{buildDefinitionId}/", UriKind.Relative);
                response = await this.Connection
                                     .Put<string>(endPointUrl, jsonBody, parameters, null)
                                     .ConfigureAwait(false);
            }
            else
            {
                endPointUrl = new Uri($"{projectName}/{EndPoint}/", UriKind.Relative);
                response = await this.Connection
                                     .Post<string>(endPointUrl, jsonBody, parameters, null)
                                     .ConfigureAwait(false);
            }

            return response.Body;
        }

        public async Task<IEnumerable<BuildDefinition>> GetAllAsync(string projectName)
        {
            var parameters = new Dictionary<string, object>();

            FluentDictionary.For(parameters)
                            .Add("api-version", "4.1");

            var response = await this.Connection.Get<GenericCollectionResponse<BuildDefinition>>(new Uri($"{projectName}/{EndPoint}", UriKind.Relative), parameters, null)
                                           .ConfigureAwait(false);

            return response.Body.Values;
        }

        public async Task<string> GetAsync(string projectName, int buildDefinitionId)
        {
            var parameters = new Dictionary<string, object>
            {
                { "api-version", "4.1" },
            };

            var endPointUrl = new Uri($"{projectName}/{EndPoint}/{buildDefinitionId}", UriKind.Relative);

            var response = await this.Connection
                         .Get<string>(endPointUrl, parameters, null)
                         .ConfigureAwait(false);

            return response.Body;
        }
    }
}