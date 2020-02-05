// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class ReleaseDefinition
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public string Path { get; set; }

        [JsonProperty("_links")]
        public LinkDetails Links { get; set; }

        public List<ReleaseEnvironment> Environments { get; set; }
    }
}