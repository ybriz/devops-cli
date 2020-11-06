namespace Jmelosegui.DevOpsCLI.Model
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    internal class ArtifactDTO
    {
        [JsonProperty(Required = Required.Always)]
        public string Alias { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string Id { get; set; }
    }
}
