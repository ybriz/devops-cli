// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBuildApiClient
    {
        Task<IEnumerable<Build>> GetAllAsync(string projectName, BuildListRequest releaseListRequest = null);
    }
}
