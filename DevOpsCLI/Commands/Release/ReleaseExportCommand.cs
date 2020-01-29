// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using System;
    using System.IO;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("export", Description = "Show the release details.")]
    public class ReleaseExportCommand : CommandBase
    {
        public ReleaseExportCommand(ILogger<CommandBase> logger)
            : base(logger)
        {
        }

        [Option("-p|--project", "Tfs project name", CommandOptionType.SingleValue)]
        public string ProjectName { get; set; }

        [Option("-rid|--release-id", "Release id", CommandOptionType.SingleValue)]
        public int ReleaseId { get; set; }

        [Option("--output-file", "File to export the release details. If this value is not provided the output will be the console.", CommandOptionType.SingleValue)]
        public string OutputFile { get; set; }

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

            string release = this.DevOpsClient.Release.GetAsync(this.ProjectName, this.ReleaseId).Result;

            if (string.IsNullOrEmpty(this.OutputFile))
            {
                Console.Write(release);
            }
            else
            {
                string outputDirectory = Path.GetDirectoryName(this.OutputFile);

                if (!string.IsNullOrEmpty(outputDirectory) && !Directory.Exists(outputDirectory))
                {
                    Directory.CreateDirectory(outputDirectory);
                }

                File.WriteAllText(this.OutputFile, release);
            }

            return ExitCodes.Ok;
        }
    }
}
