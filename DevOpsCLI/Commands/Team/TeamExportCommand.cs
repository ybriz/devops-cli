// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using System;
    using Jmelosegui.DevOps.Client;
    using Jmelosegui.DevOps.Client.Models;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json.Linq;

    [Command("export", Description = "Get a Team.")]
    internal class TeamExportCommand : ProjectCommandBase
    {
        public TeamExportCommand(ApplicationConfiguration settings, ILogger<TeamExportCommand> logger)
            : base(settings, logger)
        {
        }

        [Option(
            "--id|",
            "Team Id",
            CommandOptionType.SingleValue)]
        public Guid Id { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            base.OnExecute(app);

            while (this.NonInteractive == false && this.Id == Guid.Empty)
            {
                string value = Prompt.GetString("> Team Id:", null, ConsoleColor.DarkGray);
                if (Guid.TryParse(value, out Guid teamId))
                {
                    this.Id = teamId;
                }
            }

            Team team = this.DevOpsClient.Team.GetAsync(this.ProjectName, this.Id).GetAwaiter().GetResult();

            this.PrintOrExport(team);

            return ExitCodes.Ok;
        }
    }
}
