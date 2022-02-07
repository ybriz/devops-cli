// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client.Models.Requests
{
    public sealed class RepositoryListRequest
    {
        public bool IncludeHidden { get; set; }

        public bool IncludeAllUrls { get; set; }

        public bool IncludeLinks { get; set; }
    }
}
