// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client
{
    public class ConfigurationVariableValue
    {
        public bool AllowOverride { get; set; }

        public bool IsSecret { get; set; }

        public string Value { get; set; }
    }
}
