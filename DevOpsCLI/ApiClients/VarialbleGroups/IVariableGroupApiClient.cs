// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.ApiClients
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Jmelosegui.DevOpsCLI.Models;

    public interface IVariableGroupApiClient
    {
        Task<IEnumerable<VariableGroup>> GetAllAsync(string projectName);

        Task<string> GetAsync(string projectName, int variableGroupId);

        Task<string> AddOrUpdateAsync(string projectName, int variableGroupId, string jsonBody);
    }
}