// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("list", Description = "Get a list of task groups.")]
    public class TaskGroupListCommand : ProjectCommandBase
    {
        public TaskGroupListCommand(ApplicationConfiguration settings, ILogger<TaskGroupListCommand> logger)
            : base(settings, logger)
        {
        }

        protected override int OnExecute(CommandLineApplication app)
        {
            base.OnExecute(app);

            var list = this.DevOpsClient.TaskGroup.GetAllAsync(this.ProjectName).GetAwaiter().GetResult();

            this.PrintOrExport(list);

            return ExitCodes.Ok;
        }
    }
}
