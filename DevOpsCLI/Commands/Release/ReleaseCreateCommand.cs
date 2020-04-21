// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using System;
    using System.Collections.Generic;
    using Jmelosegui.DevOps.Client;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;

    [Command("create", Description = "Request (create) a release given a release definition.")]
    public class ReleaseCreateCommand : CommandBase
    {
        public ReleaseCreateCommand(ILogger<ReleaseCreateCommand> logger)
            : base(logger)
        {
        }

        [Option(
            "-rdid|--release-definition-id",
            "Release definition id",
            CommandOptionType.SingleValue)]
        public int ReleaseDefinitionId { get; set; }

        [Option(
            "-desc|--description",
            "Description",
            CommandOptionType.SingleValue)]
        public string Description { get; set; }

        [Option(
            "-r|--reason",
            "Reason",
            CommandOptionType.SingleValue)]
        public string Reason { get; set; }

        [Option(
            "--is-draft",
            "IsDraft",
            CommandOptionType.NoValue)]
        public bool IsDraft { get; set; }

        [Option(
            "-e|--environment",
            "Manual Environment",
            CommandOptionType.MultipleValue)]
        public IEnumerable<string> ManualEnvironments { get; private set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            base.OnExecute(app);

            while (this.ReleaseDefinitionId <= 0)
            {
                int.TryParse(Prompt.GetString("> ReleaseDefinitionId:", null, ConsoleColor.DarkGray), out int releaseDefinitionId);
                this.ReleaseDefinitionId = releaseDefinitionId;
            }

            CreateReleaseRequest request = new CreateReleaseRequest
            {
                DefinitionId = this.ReleaseDefinitionId,
                Description = this.Description,
                IsDraft = this.IsDraft,
                ManualEnvironments = this.ManualEnvironments,
                Reason = this.Reason,
            };

            var result = this.DevOpsClient.Release.CreateAsync(this.ProjectName, request).Result;

            Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));

            return ExitCodes.Ok;
        }
    }
}
