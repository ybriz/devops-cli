// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITaskGroupApiClient
    {
        Task<IEnumerable<TaskGroup>> GetAllAsync(string projectName);

        Task<string> GetAsync(string projectName, Guid taskGroupId);

        Task<string> AddOrUpdateAsync(string projectName, Guid taskGroupId, string jsonBody);
    }
}