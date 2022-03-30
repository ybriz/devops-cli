// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using System;
    using Jmelosegui.DevOps.Client;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("update", Description = "Update the status of a release environment.")]
    public class ReleaseEnvironmentUpdateCommand : ReleaseCommandBase
    {
        public ReleaseEnvironmentUpdateCommand(ILogger<ReleaseEnvironmentUpdateCommand> logger)
            : base(logger)
        {
        }

        [Option("-rid|--release-id", "Release id", CommandOptionType.SingleValue)]
        public int ReleaseId { get; set; }

        [Option("-eid|--environment-id", "Environment Id", CommandOptionType.SingleValue)]
        public int EnvironmentId { get; set; }

        [Option("-s|--status", "Status to update to (valid values here https://docs.microsoft.com/en-us/rest/api/azure/devops/release/releases/update%20release%20environment?view=azure-devops-rest-5.1#environmentstatus )", CommandOptionType.SingleValue)]
        public string Status { get; set; }

        [Option("-c|--comments", "Comments", CommandOptionType.SingleValue)]
        public string Comments { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            base.OnExecute(app);

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

            Enum.TryParse(value: this.Status, ignoreCase: true, out status);

            while (status <= 0)
            {
                Enum.TryParse(Prompt.GetString("> Environment Status:", null, ConsoleColor.DarkGray), ignoreCase: true, out status);
            }

            var result = this.DevOpsClient.Release.UpdateEnvironmentAsync(this.ProjectName, this.ReleaseId, this.EnvironmentId, status, this.Comments).GetAwaiter().GetResult();

            Console.WriteLine(result);

            return ExitCodes.Ok;
        }
    }
}
