// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands.Graph.Groups
{
    using Jmelosegui.DevOps;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;
    using System;

    [Command("export", Description = "Get a group by its descriptor.")]
    public sealed class GroupExportCommand : CommandBase
    {
        public GroupExportCommand(ILogger<GroupExportCommand> logger)
            : base(logger)
        {
        }

        [Option(
          "--group-descriptor",
          "The descriptor of the desired graph group.",
          CommandOptionType.SingleValue)]
        public string GroupDescriptor { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            base.OnExecute(app);

            while (string.IsNullOrEmpty(this.GroupDescriptor))
            {
                this.GroupDescriptor = Prompt.GetString("> Group Descriptor", null, ConsoleColor.DarkGray);
            }

            GraphGroup graphGroup = this.DevOpsClient.Graph.GroupGetAsync(this.GroupDescriptor).GetAwaiter().GetResult();

            this.PrintOrExport(graphGroup);

            return ExitCodes.Ok;
        }
    }
}
