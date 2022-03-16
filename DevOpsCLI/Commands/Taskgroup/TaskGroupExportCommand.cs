// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using System;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("export", Description = "Show task group details.")]
    public class TaskGroupExportCommand : CommandBase
    {
        public TaskGroupExportCommand(ILogger<TaskGroupExportCommand> logger)
            : base(logger)
        {
        }

        [Option(
            "--task-group-id",
            "Task group id",
            CommandOptionType.SingleValue)]
        public string TaskGroupId { get; set; }

        [Option(
            "--output-file",
            "File to export the task group details. If this value is not provided the output will be the console.",
            CommandOptionType.SingleValue)]
        public string OutputFile { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            base.OnExecute(app);

            while (string.IsNullOrEmpty(this.TaskGroupId))
            {
                this.TaskGroupId = Prompt.GetString("> TaskGroupId:", null, ConsoleColor.DarkGray);
            }

            if (!Guid.TryParse(this.TaskGroupId, out Guid taskGroupId))
            {
                throw new InvalidCastException("Invalid TaskGroupId format");
            }

            string taskGroup = this.DevOpsClient.TaskGroup.GetAsync(this.ProjectName, taskGroupId).GetAwaiter().GetResult();

            this.PrintOrExport(taskGroup, this.OutputFile);

            return ExitCodes.Ok;
        }
    }
}
