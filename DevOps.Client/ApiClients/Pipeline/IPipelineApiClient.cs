// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Jmelosegui.DevOps.Client.Models;
    using Jmelosegui.DevOps.Client.Models.Requests;

    public interface IPipelineApiClient
    {
        Task<IEnumerable<Pipeline>> GetAllAsync(string projectName, PipelineListRequest pipelineListRequest = null);

        Task<Pipeline> CreateAsync(string projectName, PipelineCreateRequest pipelineCreateRequest);

        Task<Pipeline> GetAsync(string projectName, int id);
    }
}
