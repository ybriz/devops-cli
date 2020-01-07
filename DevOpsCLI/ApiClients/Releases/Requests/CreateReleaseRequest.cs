// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.ApiClients.Releases.Requests
{
    using System.Collections.Generic;

    public class CreateReleaseRequest
    {
        public int DefinitionId { get; set; }

        public string Description { get; set; }

        public bool IsDraft { get; set; }

        public IEnumerable<string> ManualEnvironments { get; set; }

        public string Reason { get; set; }

        public Dictionary<string, ConfigurationVariableValue> Variables { get; set; }
    }

    public class ConfigurationVariableValue
    {
        public bool AllowOverride { get; set; }

        public bool IsSecret { get; set; }

        public string Value { get; set; }
    }
}
