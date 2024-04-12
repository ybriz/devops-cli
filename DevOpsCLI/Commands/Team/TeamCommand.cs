// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("team", Description = "Commands for managing Teams.")]
    [Subcommand(typeof(TeamListCommand))]
    [Subcommand(typeof(TeamExportCommand))]
    [Subcommand(typeof(TeamCreateCommand))]
    public class TeamCommand : ProjectCommandBase
    {
        public TeamCommand(ApplicationConfiguration settings, ILogger<TeamCommand> logger)
            : base(settings, logger)
        {
        }
    }
}