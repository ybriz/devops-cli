// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using System.Collections.Generic;
    using Jmelosegui.DevOps.Client;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("list", Description = "Get a list of build definitions.")]
    public class BuildDefinitionListCommand : ProjectCommandBase
    {
        public BuildDefinitionListCommand(ILogger<BuildDefinitionListCommand> logger)
            : base(logger)
        {
        }

        protected override int OnExecute(CommandLineApplication app)
        {
            base.OnExecute(app);

            IEnumerable<BuildDefinition> results = this.DevOpsClient.BuildDefinition.GetAllAsync(this.ProjectName).GetAwaiter().GetResult();

            this.PrintOrExport(results);

            return ExitCodes.Ok;
        }
    }
}
