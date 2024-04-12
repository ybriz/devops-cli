// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using System;
    using Jmelosegui.DevOps.Client;
    using Jmelosegui.DevOps.Client.Models;
    using Jmelosegui.DevOpsCLI.Model;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;

    [Command("create", Description = "Create a Team.")]
    internal class TeamCreateCommand : ProjectCommandBase
    {
        public TeamCreateCommand(ApplicationConfiguration settings, ILogger<TeamCreateCommand> logger)
            : base(settings, logger)
        {
        }

        [Option(
            "-n|--name",
            "Team Name",
            CommandOptionType.SingleValue)]
        public string Name { get; set; }

        [Option(
            "-d|--description",
            "Team Description",
            CommandOptionType.SingleValue)]
        public string Description { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            base.OnExecute(app);

            while (this.NonInteractive == false && string.IsNullOrEmpty(this.Name))
            {
                this.Name = Prompt.GetString("> Team Name:", null, ConsoleColor.DarkGray);
            }

            CreateTeamRequest request = new CreateTeamRequest
            {
                Name = this.Name,
                Description = this.Description,
            };

            var result = this.DevOpsClient.Team.CreateAsync(this.ProjectName, request).GetAwaiter().GetResult();

            Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));

            return ExitCodes.Ok;
        }
    }
}
