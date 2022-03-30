// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using System;
    using System.Linq;
    using Jmelosegui.DevOps.Client;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("export", Description = "Show variable group details.")]
    public class VariableGroupExportCommand : CommandBase
    {
        public VariableGroupExportCommand(ILogger<VariableGroupExportCommand> logger)
            : base(logger)
        {
        }

        [Option(
            "--variable-group-id",
            "Variable group id",
            CommandOptionType.SingleValue)]
        public int VariableGroupId { get; set; }

        [Option(
        "--variable-group-name",
        "Variable Group Name",
        CommandOptionType.SingleValue)]
        public string VariableGroupName { get; set; }

        [Option(
            "--output-file",
            "File to export the variable group details. If this value is not provided the output will be the console.",
            CommandOptionType.SingleValue)]
        public string OutputFile { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            base.OnExecute(app);

            while (this.VariableGroupId <= 0 && string.IsNullOrEmpty(this.VariableGroupName))
            {
                string value = Prompt.GetString("> VariableGroup Id or Name:", null, ConsoleColor.DarkGray);

                if (int.TryParse(value, out int variableGroupId))
                {
                    this.VariableGroupId = variableGroupId;
                }
                else
                {
                    this.VariableGroupName = value;
                }
            }

            if (this.VariableGroupId == 0)
            {
                var variableGroupId = this.GetVariableGroupByName(this.VariableGroupName);

                if (variableGroupId != null)
                {
                    this.VariableGroupId = variableGroupId.Id;
                }
                else
                {
                    Console.Error.WriteLine($"Cannot find a variable group named: {this.VariableGroupName}");
                    return ExitCodes.ResourceNotFound;
                }
            }

            try
            {
                string variableGroup = this.DevOpsClient.VariableGroup.GetAsync(this.ProjectName, this.VariableGroupId).GetAwaiter().GetResult();

                this.PrintOrExport(variableGroup, this.OutputFile);

                return ExitCodes.Ok;
            }
            catch (NotFoundException ex)
            {
                Console.Error.WriteLine(ex.Message);
                return ExitCodes.ResourceNotFound;
            }
        }

        private VariableGroup GetVariableGroupByName(string value)
        {
            var request = new VariableGroupListRequest
            {
                SearchText = value,
            };

            VariableGroup variableGroup = this.DevOpsClient
                                                    .VariableGroup
                                                    .GetAllAsync(this.ProjectName, request)
                                                    .GetAwaiter()
                                                    .GetResult()
                                                    .FirstOrDefault(rd => rd.Name.Equals(value, StringComparison.OrdinalIgnoreCase));

            return variableGroup;
        }
    }
}
