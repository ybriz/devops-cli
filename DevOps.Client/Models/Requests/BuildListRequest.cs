﻿// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client
{
    using System.Collections.Generic;

    public class BuildListRequest
    {
        public int BuildDefinitionId { get; set; }

        public int Top { get; set; }

        public string BuildNumber { get; set; }

        public IEnumerable<string> IncludeProperties { get; set; }
    }
}
