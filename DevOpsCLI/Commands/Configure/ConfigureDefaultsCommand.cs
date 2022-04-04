// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using System;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("defaults", Description = "Configure default values for common parameters (i.e. serviceUrl, project).")]
    public class ConfigureDefaultsCommand
    {
        private readonly ILogger<ConfigureCommand> logger;
        private readonly ApplicationConfiguration settings;

        public ConfigureDefaultsCommand(ApplicationConfiguration settings, ILogger<ConfigureCommand> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        [Option(
            "--service-url",
            "Set the default service url (Azure DevOps Organization) to use if none is provided.",
            CommandOptionType.SingleValue)]
        public string ServiceUrl { get; set; }

        [Option(
            "--project",
            "Set the default project name to use if none is provided.",
            CommandOptionType.SingleValue)]
        public string ProjectName { get; set; }

        protected virtual int OnExecute(CommandLineApplication app)
        {
            if (string.IsNullOrEmpty(this.ProjectName)
                && string.IsNullOrEmpty(this.ServiceUrl))
            {
                throw new Exception("Expected at least one argument.");
            }

            this.settings.Defaults.Organization = this.ServiceUrl ?? this.settings.Defaults.Organization;
            this.settings.Defaults.Project = this.ProjectName;
            this.settings.Save();

            return ExitCodes.Ok;
        }
    }
}
