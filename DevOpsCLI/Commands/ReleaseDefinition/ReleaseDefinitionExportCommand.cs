// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using System;
    using System.IO;
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
            "--output-file",
            "File to export the release definition details. If this value is not provided the output will be the console.",
            CommandOptionType.SingleValue)]
        public string OutputFile { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            base.OnExecute(app);

            while (this.ReleaseDefinitionId <= 0)
            {
                int.TryParse(Prompt.GetString("> ReleaseDefinitionId:", null, ConsoleColor.DarkGray), out int releaseDefinitionId);
                this.ReleaseDefinitionId = releaseDefinitionId;
            }

            string releaseDefinition = this.DevOpsClient.ReleaseDefinition.GetAsync(this.ProjectName, this.ReleaseDefinitionId).Result;

            this.PrintOrExport(releaseDefinition, this.OutputFile);

            return ExitCodes.Ok;
        }
    }
}
