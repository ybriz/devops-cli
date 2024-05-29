// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Subcommand(typeof(GraphDescriptorCommand))]
    [Subcommand(typeof(GraphGroupCommand))]
    [Subcommand(typeof(GraphUserCommand))]
    [Subcommand(typeof(GraphMembershipCommand))]
    [Subcommand(typeof(GraphServicePrincipalCommand))]
    [Command("graph", Description = "Commands for managing users, groups, and group memberships.")]
    public class GraphCommand : CommandBase
    {
        public GraphCommand(ApplicationConfiguration settings, ILogger<GraphCommand> logger)
            : base(settings, logger)
        {
        }
    }
}
