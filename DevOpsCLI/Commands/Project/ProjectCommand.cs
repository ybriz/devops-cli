// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Subcommand(typeof(ProjectExportCommand))]
    [Subcommand(typeof(ProjectListCommand))]
    [Command("project", Description = "Commands for managing TeamProject.")]
    public class ProjectCommand : CommandBase
    {
        public ProjectCommand(ILogger<ProjectCommand> logger)
            : base(logger)
        {
        }
    }
}
