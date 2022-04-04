// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("list", Description = "Lists the contents of the config file.")]
    public class ConfigurationListCommand
    {
        private readonly ILogger<ConfigurationListCommand> logger;
        private readonly ApplicationConfiguration configuration;

        public ConfigurationListCommand(
            ApplicationConfiguration configuration,
            ILogger<ConfigurationListCommand> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        private int OnExecute(CommandLineApplication app)
        {
            var options = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                WriteIndented = true,
            };

            var jsonConfiguration = JsonSerializer.Serialize(this.configuration, options);
            Console.WriteLine(jsonConfiguration);

            return ExitCodes.Ok;
        }
    }
}