// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;

namespace Jmelosegui.DevOpsCLI.Models
{
    public class ReleaseListRequest
    {
        public int ReleaseDefinitionId { get; set; }

        public int Top { get; set; }

        public IEnumerable<string> ExpandPropterties { get; set; }
    }
}