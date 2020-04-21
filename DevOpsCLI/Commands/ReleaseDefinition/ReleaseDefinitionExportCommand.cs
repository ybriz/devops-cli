// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using System;
    using System.Linq;
    using Jmelosegui.DevOps.Client;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("export", Description = "Show ReleaseDefinition details.")]
    public class ReleaseDefinitionExportCommand : CommandBase
    {
        public ReleaseDefinitionExportCommand(ILogger<VariableGroupExportCommand> logger)
            : base(logger)
        {
        }

        [Option(
            "-rdid|--release-definition-id",
            "Release definition id",
            CommandOptionType.SingleValue)]
        public int ReleaseDefinitionId { get; set; }

        [Option(
            "-rdn|--release-definition-name",
            "Release definition id",
            CommandOptionType.SingleValue)]
        public string ReleaseDefinitionName { get; set; }

        [Option(
            "--output-file",
            "File to export the release definition details. If this value is not provided the output will be the console.",
            CommandOptionType.SingleValue)]
        public string OutputFile { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            base.OnExecute(app);

            while (this.ReleaseDefinitionId <= 0 && string.IsNullOrEmpty(this.ReleaseDefinitionName))
            {
                string value = Prompt.GetString("> ReleaseDefinition Id or Name:", null, ConsoleColor.DarkGray);

                if (int.TryParse(value, out int releaseDefinitionId))
                {
                    this.ReleaseDefinitionId = releaseDefinitionId;
                }
                else
                {
                    this.ReleaseDefinitionName = value;
                }
            }

            if (this.ReleaseDefinitionId == 0)
            {
                var releaseDefinition = this.GetReleaseDefinitionByName(this.ReleaseDefinitionName);

                if (releaseDefinition != null)
                {
                    this.ReleaseDefinitionId = releaseDefinition.Id;
                }
                else
                {
                    Console.WriteLine($"Cannot find a release definition named: {this.ReleaseDefinitionName}");
                    return ExitCodes.ReleaseDefinitionNotFound;
                }
            }

            string jsonReleaseDefinition = this.DevOpsClient.ReleaseDefinition.GetAsync(this.ProjectName, this.ReleaseDefinitionId).Result;

            this.PrintOrExport(jsonReleaseDefinition, this.OutputFile);

            return ExitCodes.Ok;
        }

        private ReleaseDefinition GetReleaseDefinitionByName(string value)
        {
            ReleaseDefinition releaseDefinition = this.DevOpsClient
                                                    .ReleaseDefinition
                                                    .GetAllAsync(this.ProjectName)
                                                    .Result
                                                    .FirstOrDefault(rd => rd.Name.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0);

            return releaseDefinition;
        }
    }
}
