// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json;
    using Jmelosegui.DevOps.Client;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("list", Description = "Get a list of variable groups.")]
    public class VariableGroupListCommand : CommandBase
    {
        public VariableGroupListCommand(ILogger<VariableGroupListCommand> logger)
            : base(logger)
        {
        }

        [Option(
        "--search-text",
        "Get variable gorups with names starting with searchText.",
        CommandOptionType.SingleValue)]
        public string SearchText { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            base.OnExecute(app);

            VariableGroupListRequest request = null;
            if (!string.IsNullOrEmpty(this.SearchText))
            {
                request = new VariableGroupListRequest
                {
                    SearchText = this.SearchText,
                };
            }

            IEnumerable<VariableGroup> list = this.DevOpsClient.VariableGroup.GetAllAsync(this.ProjectName, request).GetAwaiter().GetResult();

            Console.WriteLine();

            string json = JsonSerializer.Serialize(list);

            Console.WriteLine(json);

            return ExitCodes.Ok;
        }
    }
}
