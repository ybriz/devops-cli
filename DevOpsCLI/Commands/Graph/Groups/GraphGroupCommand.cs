// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using Jmelosegui.DevOpsCLI.Commands.Graph.Groups;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Subcommand(typeof(GraphGroupListCommand))]
    [Command("group", Description = "Commands for managing groups.")]
    public class GraphGroupCommand : CommandBase
    {
        public GraphGroupCommand(ILogger<GraphGroupCommand> logger)
            : base(logger)
        {
        }
    }
}
