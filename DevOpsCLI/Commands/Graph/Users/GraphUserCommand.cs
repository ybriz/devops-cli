// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using Jmelosegui.DevOpsCLI.Commands.Graph.Users;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Subcommand(typeof(UserExportCommand))]
    [Subcommand(typeof(GraphUserListCommand))]
    [Command("user", Description = "Commands for managing users.")]
    public class GraphUserCommand : CommandBase
    {
        public GraphUserCommand(ApplicationConfiguration settings, ILogger<GraphUserCommand> logger)
            : base(settings, logger)
        {
        }
    }
}
