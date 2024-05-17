// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using Jmelosegui.DevOpsCLI.Commands.Graph.Memberships;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Subcommand(typeof(GraphMembershipListCommand))]
    [Command("membership", Description = "Commands for managing membership.")]
    public class GraphMembershipCommand : CommandBase
    {
        public GraphMembershipCommand(ApplicationConfiguration settings, ILogger<GraphMembershipCommand> logger)
            : base(settings, logger)
        {
        }
    }
}
