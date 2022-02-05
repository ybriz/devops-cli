// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Jmelosegui.DevOps.Client.Models;
    using Jmelosegui.DevOps.Client.Models.Requests;

    public class PipelineApiClient : IPipelineApiClient
    {
        private const string EndPoint = "_apis/pipelines";

        public PipelineApiClient(IConnection connection)
        {
            this.Connection = connection;
        }

        /// <summary>
        /// Gets the underlying connection.
        /// </summary>
        public IConnection Connection { get; private set; }

        public async Task<IEnumerable<Pipeline>> GetAllAsync(string projectName, PipelineListRequest pipelineListRequest = null)
        {
            var parameters = new Dictionary<string, object>();

            FluentDictionary.For(parameters)
                            .Add("api-version", "6.0-preview.1");

            var response = await this.Connection.Get<GenericCollectionResponse<Pipeline>>(new Uri($"{projectName}/{EndPoint}", UriKind.Relative), parameters, null)
                                                .ConfigureAwait(false);

            return response.Body.Values;
        }
    }
}
