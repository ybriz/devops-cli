// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Jmelosegui.DevOps.Client.Models;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class TeamApiClient : ITeamApiClient
    {
        private const string EndPoint = "_apis/projects";

        public TeamApiClient(IConnection connection)
        {
            this.Connection = connection;
        }

        /// <summary>
        /// Gets the underlying connection.
        /// </summary>
        public IConnection Connection { get; private set; }

        public async Task<IEnumerable<Team>> GetAllAsync(string projectName)
        {
            Ensure.ArgumentNotNullOrEmptyString(projectName, nameof(projectName));

            var parameters = new Dictionary<string, object>
            {
                { "api-version", "4.1" },
            };

            var endPointUrl = new Uri($"{EndPoint}/{projectName}/teams/", UriKind.Relative);
            var response = await this.Connection
                                     .Get<GenericCollectionResponse<Team>>(endPointUrl, parameters, null)
                                     .ConfigureAwait(false);

            return response.Body.Values;
        }

        public async Task<Team> GetAsync(string projectName, Guid teamId)
        {
            Ensure.ArgumentNotNullOrEmptyString(projectName, nameof(projectName));
            Ensure.ArgumentNotNullOrEmptyString(teamId.ToString(), nameof(teamId));

            var parameters = new Dictionary<string, object>
            {
                { "api-version", "4.1" },
            };

            var endPointUrl = new Uri($"{EndPoint}/{projectName}/teams/{teamId}", UriKind.Relative);

            var response = await this.Connection.Get<Team>(endPointUrl, parameters, null)
                                    .ConfigureAwait(false);

            return response.Body;
        }

        public async Task<Team> CreateAsync(string projectName, CreateTeamRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(projectName, nameof(projectName));

            var parameters = new Dictionary<string, object>();

            FluentDictionary.For(parameters)
                            .Add("api-version", "4.1");

            var endPointUrl = new Uri($"{EndPoint}/{projectName}/teams/", UriKind.Relative);

            IApiResponse<Team> result = await this.Connection
                                                     .Post<Team>(endPointUrl, request, parameters, null)
                                                     .ConfigureAwait(false);

            return result.Body;
        }
    }
}