// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Model
{
    using Newtonsoft.Json;

    internal class ArtifactDTO
    {
        [JsonProperty(Required = Required.Always)]
        public string Alias { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string Id { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; }
    }
}
