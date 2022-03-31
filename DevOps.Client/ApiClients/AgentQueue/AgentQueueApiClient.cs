// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class AgentQueueApiClient : IAgentQueueApiClient
    {
        private const string EndPoint = "_apis/distributedtask/queues";

        public AgentQueueApiClient(IConnection connection)
        {
            this.Connection = connection;
        }

        /// <summary>
        /// Gets the underlying connection.
        /// </summary>
        public IConnection Connection { get; private set; }

        public async Task<IEnumerable<AgentQueue>> GetAllAsync(string projectName, AgentQueueListRequest request = null)
        {
            Ensure.ArgumentNotNullOrEmptyString(projectName, nameof(projectName));

            var parameters = new Dictionary<string, object>
            {
                { "api-version", "5.1-preview.1" },
            };

            if (request != null)
            {
                FluentDictionary.For(parameters)
                                .Add("queueName", request.SearchText, () => !string.IsNullOrEmpty(request.SearchText));
            }

            var endPointUrl = new Uri($"{projectName}/{EndPoint}", UriKind.Relative);
            var response = await this.Connection
                                     .Get<GenericCollectionResponse<AgentQueue>>(endPointUrl, parameters, null)
                                     .ConfigureAwait(false);

            return response.Body.Values;
        }

        public async Task<string> GetAsync(string projectName, int agentQueueId)
        {
            Ensure.ArgumentNotNullOrEmptyString(projectName, nameof(projectName));

            var parameters = new Dictionary<string, object>
            {
                { "api-version", "5.1-preview.1" },
            };

            var endPointUrl = new Uri($"{projectName}/{EndPoint}/{agentQueueId}", UriKind.Relative);

            var response = await this.Connection
                                     .Get<string>(endPointUrl, parameters, null)
                                     .ConfigureAwait(false);

            return response.Body;
        }
    }
}
