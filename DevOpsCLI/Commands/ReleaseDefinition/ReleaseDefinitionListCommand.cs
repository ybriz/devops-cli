// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using System.Collections.Generic;
    using System.Linq;
    using Jmelosegui.DevOps.Client;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("list", Description = "Get a list of release definitions.")]
    public class ReleaseDefinitionListCommand : CommandBase
    {
        public ReleaseDefinitionListCommand(ILogger<ReleaseDefinitionListCommand> logger)
            : base(logger)
        {
        }

        [Option(
            "--all-properties",
            "If present each item in the output list will contain all the available properties",
            CommandOptionType.NoValue)]
        public bool IncludeAllProperties { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            base.OnExecute(app);

            IEnumerable<ReleaseDefinition> releaseDefinitions = this.DevOpsClient.ReleaseDefinition.GetAllAsync(this.ProjectName).Result;

            if (this.IncludeAllProperties)
            {
                this.PrintOrExport(releaseDefinitions.OrderByDescending(r => r.Id));
            }
            else
            {
                this.PrintOrExport(releaseDefinitions.Select(r => new { r.Id, r.Name })
                                                     .OrderByDescending(r => r.Id));
            }

            return ExitCodes.Ok;
        }
    }
}
