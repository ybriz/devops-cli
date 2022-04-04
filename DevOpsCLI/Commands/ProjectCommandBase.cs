// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using System;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    public abstract class ProjectCommandBase : CommandBase
    {
        protected ProjectCommandBase(ApplicationConfiguration settings, ILogger<ProjectCommandBase> logger)
            : base(settings, logger)
        {
        }

        [Option(
        "-p|--project",
        "Tfs project name",
        CommandOptionType.SingleValue)]
        public string ProjectName { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            base.OnExecute(app);

            if (!this.IsCommandGroup)
            {
                if (string.IsNullOrEmpty(this.ProjectName) && !string.IsNullOrEmpty(this.Settings?.Defaults?.Project))
                {
                    this.ProjectName = this.Settings.Defaults.Project;
                }

                while (this.NonInteractive == false && string.IsNullOrEmpty(this.ProjectName))
                {
                    this.ProjectName = Prompt.GetString("> ProjectName:", null, ConsoleColor.DarkGray);
                }
            }

            return ExitCodes.Ok;
        }
    }
}
