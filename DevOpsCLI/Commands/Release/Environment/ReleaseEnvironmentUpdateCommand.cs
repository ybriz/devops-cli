// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using System;
    using Jmelosegui.DevOpsCLI.Models;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("update", Description = "Update the status of a release environment.")]
    public class ReleaseEnvironmentUpdateCommand : CommandBase
    {
        public ReleaseEnvironmentUpdateCommand(ILogger<CommandBase> logger)
            : base(logger)
        {
        }

        [Option("-p|--project", "Tfs project name", CommandOptionType.SingleValue)]
        public string ProjectName { get; set; }

        [Option("-rid|--release-id", "Release id", CommandOptionType.SingleValue)]
        public int ReleaseId { get; set; }

        [Option("-eid|--environment-id", "Environment Id", CommandOptionType.SingleValue)]
        public int EnvironmentId { get; set; }

        [Option("-s|--status", "Comments", CommandOptionType.SingleValue)]
        public string Status { get; set; }

        [Option("--comment", "Comments", CommandOptionType.SingleValue)]
        public string Comment { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            base.OnExecute(app);

            while (string.IsNullOrEmpty(this.ProjectName))
            {
                this.ProjectName = Prompt.GetString("> ProjectName:", null, ConsoleColor.DarkGray);
            }

            while (this.ReleaseId <= 0)
            {
                int.TryParse(Prompt.GetString("> ReleaseDefinitionId:", null, ConsoleColor.DarkGray), out int releaseDefinitionId);
                this.ReleaseId = releaseDefinitionId;
            }

            while (this.EnvironmentId <= 0)
            {
                int.TryParse(Prompt.GetString("> EnvironmentId:", null, ConsoleColor.DarkGray), out int environmentId);
                this.EnvironmentId = environmentId;
            }

            EnvironmentStatus status;

            Enum.TryParse(this.Status, out status);

            while (status <= 0)
            {
                Enum.TryParse(Prompt.GetString("> Environment Status:", null, ConsoleColor.DarkGray), out status);
            }

            var result = this.DevOpsClient.Release.UpdateEnvironmentAsync(this.ProjectName, this.ReleaseId, this.EnvironmentId, status, this.Comment).Result;

            Console.WriteLine(result);

            return ExitCodes.Ok;
        }
    }
}
