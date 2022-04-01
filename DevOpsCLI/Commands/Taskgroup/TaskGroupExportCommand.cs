// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using System;
    using System.Linq;
    using Jmelosegui.DevOps.Client;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("export", Description = "Show task group details.")]
    public class TaskGroupExportCommand : ProjectCommandBase
    {
        public TaskGroupExportCommand(ILogger<TaskGroupExportCommand> logger)
            : base(logger)
        {
        }

        [Option(
            "--task-group-id",
            "Task group id",
            CommandOptionType.SingleValue)]
        public Guid TaskGroupId { get; set; }

        [Option(
        "--task-group-name",
        "Task group name",
        CommandOptionType.SingleValue)]
        public string TaskGroupName { get; set; }

        [Option(
            "--output-file",
            "File to export the task group details. If this value is not provided the output will be the console.",
            CommandOptionType.SingleValue)]
        public string OutputFile { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            base.OnExecute(app);

            while (this.TaskGroupId == Guid.Empty && string.IsNullOrEmpty(this.TaskGroupName))
            {
                string value = Prompt.GetString("> TaskGroupId:", null, ConsoleColor.DarkGray);
                if (Guid.TryParse(value, out Guid taskGroupId))
                {
                    this.TaskGroupId = taskGroupId;
                }
                else
                {
                    this.TaskGroupName = value;
                }
            }

            if (this.TaskGroupId == Guid.Empty)
            {
                var taskGroup = this.GetTaskGroupByName(this.TaskGroupName);

                if (taskGroup != null)
                {
                    this.TaskGroupId = taskGroup.Id;
                }
                else
                {
                    Console.Error.WriteLine($"Cannot find a task group named: {this.TaskGroupName}");
                    return ExitCodes.ResourceNotFound;
                }
            }

            try
            {
                string taskGroup = this.DevOpsClient.TaskGroup.GetAsync(this.ProjectName, this.TaskGroupId).GetAwaiter().GetResult();

                this.PrintOrExport(taskGroup, this.OutputFile);

                return ExitCodes.Ok;
            }
            catch (NotFoundException ex)
            {
                Console.Error.WriteLine(ex.Message);
                return ExitCodes.ResourceNotFound;
            }
        }

        private TaskGroup GetTaskGroupByName(string taskGroupName)
        {
            TaskGroup taskGroup = this.DevOpsClient
                                                    .TaskGroup
                                                    .GetAllAsync(this.ProjectName)
                                                    .GetAwaiter()
                                                    .GetResult()
                                                    .FirstOrDefault(rd => rd.Name.Equals(taskGroupName, StringComparison.OrdinalIgnoreCase));

            return taskGroup;
        }
    }
}
