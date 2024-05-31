// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client.Models
{
    using System;

    public sealed class PipelineConfiguration
    {
        public string Type { get; set; }

        public string Path { get; set; }

        public Repository Repository { get; set; }
    }
}
