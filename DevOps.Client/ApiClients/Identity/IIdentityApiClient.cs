// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public partial interface IIdentityApiClient
    {
        Task<IEnumerable<Identity>> GetAllAsync(IdentityListRequest request);

        Task<ConnectionData> GetConnectionData();
    }
}
