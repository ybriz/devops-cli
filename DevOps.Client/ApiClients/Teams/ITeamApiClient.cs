// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITeamApiClient
    {
        Task<IEnumerable<Team>> GetAllAsync(string projectName);

        Task<Team> GetAsync(string projectName, Guid teamId);

        Task<Team> CreateAsync(string projectName, CreateTeamRequest request);
    }
}