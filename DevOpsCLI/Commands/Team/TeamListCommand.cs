// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using System.Collections.Generic;
    using System.Linq;
    using Jmelosegui.DevOps.Client;
    using Jmelosegui.DevOps.Client.Models;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("list", Description = "Get a list of Teams.")]
    internal class TeamListCommand : ProjectCommandBase
    {
        public TeamListCommand(ApplicationConfiguration settings, ILogger<TeamListCommand> logger)
            : base(settings, logger)
        {
        }

        protected override int OnExecute(CommandLineApplication app)
        {
            base.OnExecute(app);

            IEnumerable<Team> teams = this.DevOpsClient.Team.GetAllAsync(this.ProjectName).GetAwaiter().GetResult();

            this.PrintOrExport(teams.Select(r => new { r.Id, r.Name })
                                        .OrderByDescending(r => r.Id));

            return ExitCodes.Ok;
        }
    }
}
