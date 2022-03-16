// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using System;
    using System.IO;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("import", Description = "CReate or update task group.")]
    public class TaskGroupImportCommand : CommandBase
    {
        public TaskGroupImportCommand(ILogger<TaskGroupExportCommand> logger)
            : base(logger)
        {
        }

        [Option(
            "--task-group-id",
            "Task group id. if this value is not provided the import command will create a new task group otherwise it will attempt to update the task group with the provided identifier.",
            CommandOptionType.SingleValue)]
        public string TaskGroupId { get; set; }

        [Option(
            "--input-file",
            "File containing the task group details to add or update on the target project.",
            CommandOptionType.SingleValue)]
        public string InputFile { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            base.OnExecute(app);

            while (string.IsNullOrEmpty(this.InputFile))
            {
                this.InputFile = Prompt.GetString("> InputFile:", null, ConsoleColor.DarkGray);
            }

            if (!File.Exists(this.InputFile))
            {
                throw new FileNotFoundException("Specified input file cannot be found", this.InputFile);
            }

            string jsonBody = File.ReadAllText(this.InputFile);

            var isValid = Guid.TryParse(this.TaskGroupId, out Guid taskGroupId);
            if (!string.IsNullOrEmpty(this.TaskGroupId) && !isValid)
            {
                throw new InvalidCastException("Invalid TaskGroupId format");
            }

            string taskGroup = this.DevOpsClient.TaskGroup.AddOrUpdateAsync(this.ProjectName, taskGroupId, jsonBody).GetAwaiter().GetResult();

            Console.WriteLine(taskGroup);

            return ExitCodes.Ok;
        }
    }
}
