// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using System;
    using System.Linq;
    using Jmelosegui.DevOps.Client;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("export", Description = "Show BuildDefinition details.")]
    public class BuildDefinitionExportCommand : CommandBase
    {
        public BuildDefinitionExportCommand(ILogger<BuildDefinitionExportCommand> logger)
            : base(logger)
        {
        }

        [Option(
            "-bdid|--build-definition-id",
            "Build definition id",
            CommandOptionType.SingleValue)]
        public int BuildDefinitionId { get; set; }

        [Option(
            "-bdn|--build-definition-name",
            "Build definition name",
            CommandOptionType.SingleValue)]
        public string BuildDefinitionName { get; set; }

        [Option(
            "--output-file",
            "File to export the release definition details. If this value is not provided the output will be the console.",
            CommandOptionType.SingleValue)]
        public string OutputFile { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            base.OnExecute(app);

            while (this.BuildDefinitionId <= 0 && string.IsNullOrEmpty(this.BuildDefinitionName))
            {
                string value = Prompt.GetString("> ReleaseDefinition Id or Name:", null, ConsoleColor.DarkGray);

                if (int.TryParse(value, out int buildDefinitionId))
                {
                    this.BuildDefinitionId = buildDefinitionId;
                }
                else
                {
                    this.BuildDefinitionName = value;
                }
            }

            if (this.BuildDefinitionId == 0)
            {
                var buildDefinition = this.GetBuildDefinitionByName(this.BuildDefinitionName);

                if (buildDefinition != null)
                {
                    this.BuildDefinitionId = buildDefinition.Id;
                }
                else
                {
                    Console.WriteLine($"Cannot find a build definition named: {this.BuildDefinitionName}");
                    return ExitCodes.ResourceNotFound;
                }
            }

            try
            {
                string jsonReleaseDefinition = this.DevOpsClient.BuildDefinition.GetAsync(this.ProjectName, this.BuildDefinitionId).GetAwaiter().GetResult();

                this.PrintOrExport(jsonReleaseDefinition, this.OutputFile);

                return ExitCodes.Ok;
            }
            catch (NotFoundException ex)
            {
                Console.Error.WriteLine(ex.Message);
                return ExitCodes.ResourceNotFound;
            }
        }

        private BuildDefinition GetBuildDefinitionByName(string value)
        {
            BuildDefinition buildDefinition = this.DevOpsClient
                                                    .BuildDefinition
                                                    .GetAllAsync(this.ProjectName)
                                                    .GetAwaiter()
                                                    .GetResult()
                                                    .FirstOrDefault(rd => rd.Name.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0);

            return buildDefinition;
        }
    }
}
