// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Models
{
    using Newtonsoft.Json;

    public class Release
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public string LogsContainerUrl { get; set; }

        [JsonProperty("_links")]
        public LinkDetails Links { get; set;  }

        public ReleaseDefinition ReleaseDefinition { get; set; }

        public int ReleaseDifinitionRevision { get; set; }
    }
}