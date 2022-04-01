// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IReleaseDefinitionApiClient
    {
        Task<IEnumerable<ReleaseDefinition>> GetAllAsync(string projectName, ReleaseDefinitionListRequest request = null);

        Task<string> GetAsync(string projectName, int releaseDefinitionId);

        Task<string> AddOrUpdateAsync(string projectName, int releaseDefinitionId, string jsonBody);
    }
}