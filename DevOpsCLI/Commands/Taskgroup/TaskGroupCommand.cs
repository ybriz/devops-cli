// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("task-group", Description = "Commands for managing task Groups.")]
    [Subcommand(typeof(TaskGroupListCommand))]
    [Subcommand(typeof(TaskGroupExportCommand))]
    [Subcommand(typeof(TaskGroupImportCommand))]
    public class TaskGroupCommand : ProjectCommandBase
    {
        public TaskGroupCommand(ApplicationConfiguration settings, ILogger<TaskGroupCommand> logger)
            : base(settings, logger)
        {
        }
    }
}