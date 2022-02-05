// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("pipeline", Description = "Commands for managing Pipelines.")]
    [Subcommand(typeof(PipelineListCommand))]
    public class PipelineCommand : CommandBase
    {
        public PipelineCommand(ILogger<PipelineCommand> logger)
            : base(logger)
        {
        }
    }
}