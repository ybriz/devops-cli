// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using System;
    using Jmelosegui.DevOps.Client.Models;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("export", Description = "Get a pipeline.")]
    internal class PipelineGetCommand : CommandBase
    {
        public PipelineGetCommand(ILogger<CommandBase> logger)
            : base(logger)
        {
        }

        [Option(
            "--id|",
            "Pipeline Id",
            CommandOptionType.SingleValue)]
        public int? Id { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            base.OnExecute(app);

            while (!this.Id.HasValue)
            {
                if (int.TryParse(Prompt.GetString("> Pipeline Id", null, ConsoleColor.DarkGray), out int tempId))
                {
                    this.Id = tempId;
                }
            }

            Pipeline pipeline = this.DevOpsClient.Pipeline.GetAsync(this.ProjectName, this.Id.Value).GetAwaiter().GetResult();

            this.PrintOrExport(pipeline);

            return ExitCodes.Ok;
        }
    }
}
