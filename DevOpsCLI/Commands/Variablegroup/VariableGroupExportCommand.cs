// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using Jmelosegui.DevOpsCLI.ApiClients;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("export", Description = "Show variable group details.")]
    public class VariableGroupExportCommand : CommandBase
    {
        public VariableGroupExportCommand(ILogger<VariableGroupExportCommand> logger)
            : base(logger)
        {
        }

        [Option("-p|--project", "Tfs project name", CommandOptionType.SingleValue)]
        public string ProjectName { get; set; }

        [Option("--variable-group-id", "Variable group id", CommandOptionType.SingleValue)]
        public int VariableGroupId { get; set; }

        [Option("--output-file", "File to export the variable group details. If this value is not provided the output will be the console.", CommandOptionType.SingleValue)]
        public string OutputFile { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            base.OnExecute(app);

            while (string.IsNullOrEmpty(this.ProjectName))
            {
                this.ProjectName = Prompt.GetString("> ProjectName:", null, ConsoleColor.DarkGray);
            }

            while (this.VariableGroupId <= 0)
            {
                int.TryParse(Prompt.GetString("> VariableGroupId:", null, ConsoleColor.DarkGray), out int variableGroupId);
                this.VariableGroupId = variableGroupId;
            }

            string variableGroup = this.DevOpsClient.VariableGroup.GetAsync(this.ProjectName, this.VariableGroupId).Result;

            if (string.IsNullOrEmpty(this.OutputFile))
            {
                Console.Write(variableGroup);
            }
            else
            {
                string outputDirectory = Path.GetDirectoryName(this.OutputFile);

                if (!string.IsNullOrEmpty(outputDirectory) && !Directory.Exists(outputDirectory))
                {
                    Directory.CreateDirectory(outputDirectory);
                }

                File.WriteAllText(this.OutputFile, variableGroup);
            }

            return ExitCodes.Ok;
        }
    }
}
