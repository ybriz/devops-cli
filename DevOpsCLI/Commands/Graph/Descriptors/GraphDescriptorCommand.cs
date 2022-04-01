// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Subcommand(typeof(GraphDescriptorExportCommand))]
    [Command("descriptor", Description = "Commands for managing descriptors.")]
    public class GraphDescriptorCommand : CommandBase
    {
        public GraphDescriptorCommand(ILogger<GraphDescriptorCommand> logger)
            : base(logger)
        {
        }
    }
}
