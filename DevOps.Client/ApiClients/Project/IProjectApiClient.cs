// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Jmelosegui.DevOps.Client.Models.Requests;

    public interface IProjectApiClient
    {
        Task<string> GetAsync(Guid projectId);

        Task<IEnumerable<TeamProjectReference>> GetAllAsync(TeamProjectListRequest request);
    }
}
