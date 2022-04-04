// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using System;
    using Jmelosegui.DevOps.Client;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Subcommand(typeof(ConfigurationListCommand))]
    [Subcommand(typeof(ConfigureDefaultsCommand))]
    [Command("configure", Description = "Configure the DevOps CLI or view the configuration.")]
    public class ConfigureCommand
    {
        private readonly ILogger<ConfigureCommand> logger;
        private readonly ApplicationConfiguration settings;

        public ConfigureCommand(ApplicationConfiguration settings, ILogger<ConfigureCommand> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        protected virtual int OnExecute(CommandLineApplication app)
        {
            app.ShowHelp(true);
            return ExitCodes.Ok;
        }
    }
}
