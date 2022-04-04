// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI
{
    using System;
    using System.IO;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public sealed class ApplicationConfiguration
    {
        public ApplicationConfiguration()
        {
            this.Defaults = new DefaultParameters();
        }

        public DefaultParameters Defaults { get; set; }

        public string GetConfigurationFile()
        {
            string homeUserProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            return Path.Join(homeUserProfile, ".devops", "config.json");
        }

        internal void Save()
        {
            string configurationFile = this.GetConfigurationFile();

            string configurationDirectory = Path.GetDirectoryName(configurationFile);
            if (!Directory.Exists(configurationDirectory))
            {
                Directory.CreateDirectory(configurationDirectory);
            }

            var options = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                WriteIndented = true,
            };

            File.WriteAllText(configurationFile, JsonSerializer.Serialize(this, options));
        }

        public class DefaultParameters
        {
            public string Organization { get; set; }

            public string Project { get; set; }
        }
    }
}
