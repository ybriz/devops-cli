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
    public class ReleaseDefinitionListCommand : ReleaseCommandBase
    {
        public ReleaseDefinitionListCommand(ILogger<ReleaseDefinitionListCommand> logger)
            : base(logger)
        {
        }

        [Option(
            "--search-text",
            "Get release definitions with names starting with searchText.",
            CommandOptionType.SingleValue)]
        public string SearchText { get; set; }

        [Option(
            "--all-properties",
            "If present each item in the output list will contain all the available properties",
            CommandOptionType.NoValue)]
        public bool IncludeAllProperties { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            base.OnExecute(app);

            ReleaseDefinitionListRequest request = null;
            if (!string.IsNullOrEmpty(this.SearchText))
            {
                request = new ReleaseDefinitionListRequest();
                request.SearchText = this.SearchText;
            }

            IEnumerable<ReleaseDefinition> releaseDefinitions = this.DevOpsClient.ReleaseDefinition.GetAllAsync(this.ProjectName, request).GetAwaiter().GetResult();

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
