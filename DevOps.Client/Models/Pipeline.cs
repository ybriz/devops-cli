// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client.Models
{
    public sealed class Pipeline
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Revision { get; set; }

        public string Url { get; set; }

        public string Folder { get; set; }
    }
}
