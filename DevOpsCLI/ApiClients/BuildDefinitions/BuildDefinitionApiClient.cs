// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.ApiClients
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Jmelosegui.DevOpsCLI.Http;
    using Jmelosegui.DevOpsCLI.Models;

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

        public async Task<IEnumerable<BuildDefinition>> GetAllAsync(string projectName)
        {
            var parameters = new Dictionary<string, string>
            {
                { "api-version", "4.1" },
            };

            var response = await this.Connection.Get<GenericCollectionResponse<BuildDefinition>>(new Uri($"{projectName}/{EndPoint}", UriKind.Relative), parameters, null)
                                           .ConfigureAwait(false);

            return response.Body.Values;
        }
    }
}