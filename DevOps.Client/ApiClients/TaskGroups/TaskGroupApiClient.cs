// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class TaskGroupApiClient : ITaskGroupApiClient
    {
        private const string EndPoint = "_apis/distributedtask/taskgroups";

        public TaskGroupApiClient(IConnection connection)
        {
            this.Connection = connection;
        }

        /// <summary>
        /// Gets the underlying connection.
        /// </summary>
        public IConnection Connection { get; private set; }

        public async Task<IEnumerable<TaskGroup>> GetAllAsync(string projectName)
        {
            Ensure.ArgumentNotNullOrEmptyString(projectName, nameof(projectName));

            var parameters = new Dictionary<string, object>
            {
                { "api-version", "4.1-preview.1" },
            };

            var endPointUrl = new Uri($"{projectName}/{EndPoint}", UriKind.Relative);
            var response = await this.Connection
                                     .Get<GenericCollectionResponse<TaskGroup>>(endPointUrl, parameters, null)
                                     .ConfigureAwait(false);

            return response.Body.Values;
        }

        public async Task<string> GetAsync(string projectName, Guid taskGroupId)
        {
            Ensure.ArgumentNotNullOrEmptyString(projectName, nameof(projectName));

            var parameters = new Dictionary<string, object>
            {
                { "api-version", "4.1-preview.1" },
            };

            var endPointUrl = new Uri($"{projectName}/{EndPoint}/{taskGroupId}", UriKind.Relative);

            var response = await this.Connection
                                     .Get<GenericCollectionResponse<JObject>>(endPointUrl, parameters, null)
                                     .ConfigureAwait(false);

            if (response.Body?.Values.Count <= 0)
            {
                return null;
            }

            return JsonConvert.SerializeObject(response.Body.Values[0]);
        }

        public async Task<string> AddOrUpdateAsync(string projectName, Guid taskGroupId, string jsonBody)
        {
            Ensure.ArgumentNotNullOrEmptyString(projectName, nameof(projectName));

            var parameters = new Dictionary<string, object>
            {
                { "api-version", "4.1-preview.1" },
            };
            Uri endPointUrl;
            IApiResponse<string> response;

            if (taskGroupId != Guid.Empty)
            {
                endPointUrl = new Uri($"{projectName}/{EndPoint}/{taskGroupId}/", UriKind.Relative);
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
    }
}