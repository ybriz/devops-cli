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
        public ReleaseExportCommand(ILogger<ReleaseExportCommand> logger)
            : base(logger)
        {
        }

        [Option(
            "-rid|--release-id",
            "Release id",
            CommandOptionType.SingleValue)]
        public int ReleaseId { get; set; }

        [Option(
            "--output-file",
            "File to export the release details. If this value is not provided the output will be the console.",
            CommandOptionType.SingleValue)]
        public string OutputFile { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            base.OnExecute(app);

            while (this.ReleaseId <= 0)
            {
                int.TryParse(Prompt.GetString("> Release Id:", null, ConsoleColor.DarkGray), out int releaseId);
                this.ReleaseId = releaseId;
            }

            string release = this.DevOpsClient.Release.GetAsync(this.ProjectName, this.ReleaseId).Result;

            this.PrintOrExport(release, this.OutputFile);

            return ExitCodes.Ok;
        }
    }
}
