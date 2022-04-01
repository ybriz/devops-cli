// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using System;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    public abstract class ProjectCommandBase : CommandBase
    {
        protected ProjectCommandBase(ILogger<ProjectCommandBase> logger)
            : base(logger)
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
                while (string.IsNullOrEmpty(this.ProjectName))
                {
                    this.ProjectName = Prompt.GetString("> ProjectName:", null, ConsoleColor.DarkGray);
                }
            }

            return ExitCodes.Ok;
        }
    }
}
