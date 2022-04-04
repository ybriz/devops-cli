// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Jmelosegui.DevOps.Client.Models.Requests;

    public sealed class ProjectApiClient : IProjectApiClient
    {
        private const string EndPoint = "_apis/projects";

        public ProjectApiClient(IConnection connection)
        {
            this.Connection = connection;
        }

        /// <summary>
        /// Gets the underlying connection.
        /// </summary>
        public IConnection Connection { get; private set; }

        public async Task<IEnumerable<TeamProjectReference>> GetAllAsync(TeamProjectListRequest request)
        {
            var parameters = new Dictionary<string, object>
            {
                { "api-version", "4.0" },
            };

            if (request != null)
            {
                FluentDictionary.For(parameters)
                            .Add("stateFilter", request.StateFilter);
            }

            var response = await this.Connection.Get<GenericCollectionResponse<TeamProjectReference>>(new Uri(EndPoint, UriKind.Relative), parameters, null)
                                                .ConfigureAwait(false);

            return response.Body.Values;
        }

        public async Task<string> GetAsync(Guid projectId)
        {
            var parameters = new Dictionary<string, object>();

            FluentDictionary.For(parameters)
                            .Add("api-version", "4.0")
                            .Add("projectId", projectId);

            var endPoint = new Uri(EndPoint, UriKind.Relative);

            var response = await this.Connection.Get<string>(endPoint, parameters, null)
                                                .ConfigureAwait(false);

            return response.Body;
        }
    }
}
