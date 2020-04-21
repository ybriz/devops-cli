// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class BuildApiClient : IBuildApiClient
    {
        private const string EndPoint = "_apis/build/builds";

        public BuildApiClient(IConnection connection)
        {
            this.Connection = connection;
        }

        /// <summary>
        /// Gets the underlying connection.
        /// </summary>
        public IConnection Connection { get; private set; }

        public async Task<IEnumerable<Build>> GetAllAsync(string projectName, BuildListRequest buildListRequest = null)
        {
            var parameters = new Dictionary<string, object>();

            FluentDictionary.For(parameters)
                            .Add("api-version", "4.1")
                            .Add("definitions", buildListRequest.BuildDefinitionId, () => buildListRequest.BuildDefinitionId > 0)
                            .Add("$top", buildListRequest.Top, () => buildListRequest.Top > 0);

            var response = await this.Connection.Get<GenericCollectionResponse<Build>>(new Uri($"{projectName}/{EndPoint}", UriKind.Relative), parameters, null)
                               .ConfigureAwait(false);

            return response.Body.Values;
        }
    }
}
