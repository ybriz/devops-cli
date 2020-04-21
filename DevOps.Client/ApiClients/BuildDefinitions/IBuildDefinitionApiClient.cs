// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBuildDefinitionApiClient
    {
        Task<IEnumerable<BuildDefinition>> GetAllAsync(string projectName);
    }
}