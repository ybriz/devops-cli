// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public class UpdateEnvironmentRequest
    {
        public string Comments { get; set; }

        public string ScheduledDeploymentTime { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public EnvironmentStatus Status { get; set; }

        public IDictionary<string, ConfigurationVariableValue> Variables { get; set; }
    }
}
