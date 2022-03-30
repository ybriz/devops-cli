// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using System;
    using System.IO;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("import", Description = "Create or update release definition.")]
    public sealed class ReleaseDefinitionImportCommand : ReleaseCommandBase
    {
        public ReleaseDefinitionImportCommand(ILogger<ReleaseDefinitionImportCommand> logger)
            : base(logger)
        {
        }

        [Option(
           "--release-definition-id",
           "Release definition id. if this value is not provided the import command will create a new release definition otherwise it will attempt to update the release definition with the provided identifier.",
           CommandOptionType.SingleValue)]
        public int ReleaseDefinitionId { get; set; }

        [Option(
            "--input-file",
            "File containing the release definition details to add or update on the target project.",
            CommandOptionType.SingleValue)]
        public string InputFile { get; set; }

        protected override string HostPrefix => "vsrm.";

        protected override int OnExecute(CommandLineApplication app)
        {
            base.OnExecute(app);

            while (string.IsNullOrEmpty(this.InputFile))
            {
                this.InputFile = Prompt.GetString("> InputFile:", null, ConsoleColor.DarkGray);
            }

            if (!File.Exists(this.InputFile))
            {
                throw new FileNotFoundException("Specified input file cannot be found", this.InputFile);
            }

            string jsonBody = File.ReadAllText(this.InputFile);

            string releaseDefinition = this.DevOpsClient.ReleaseDefinition.AddOrUpdateAsync(this.ProjectName, this.ReleaseDefinitionId, jsonBody).GetAwaiter().GetResult();

            Console.WriteLine(releaseDefinition);

            return ExitCodes.Ok;
        }
    }
}
