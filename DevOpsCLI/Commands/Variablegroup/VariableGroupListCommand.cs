// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using System;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("list", Description = "Get a list of variable groups.")]
    public class VariableGroupListCommand : CommandBase
    {
        public VariableGroupListCommand(ILogger<VariableGroupListCommand> logger)
            : base(logger)
        {
        }

        protected override int OnExecute(CommandLineApplication app)
        {
            base.OnExecute(app);

            var list = this.DevOpsClient.VariableGroup.GetAllAsync(this.ProjectName).GetAwaiter().GetResult();

            Console.WriteLine();

            foreach (var variableGroup in list)
            {
                Console.WriteLine($"{variableGroup.Name} ({variableGroup.Id})");
            }

            return ExitCodes.Ok;
        }
    }
}
