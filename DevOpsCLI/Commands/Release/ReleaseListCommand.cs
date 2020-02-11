// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Jmelosegui.DevOpsCLI.Models;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("list", Description = "Get a list of release.")]
    public class ReleaseListCommand : CommandBase
    {
        public ReleaseListCommand(ILogger<ReleaseListCommand> logger)
            : base(logger)
        {
        }

        [Option(
            "-rdid|--release-definition-id",
            "Release definition id to filter the resutl",
            CommandOptionType.SingleValue)]
        public int ReleaseDefinitionId { get; set; }

        [Option(
            "--top",
            "Number of releases to get. Default is 50.",
            CommandOptionType.SingleValue)]
        public int Top { get; set; }

        [Option(
            "--all-properties",
            "If present each item in the output list of releases will contain all the release properties",
            CommandOptionType.NoValue)]
        public bool IncludeAllProperties { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            base.OnExecute(app);

            var releaseListRequest = new ReleaseListRequest
            {
                ReleaseDefinitionId = this.ReleaseDefinitionId,
                Top = this.Top,
            };

            IEnumerable<Release> releases = this.DevOpsClient.Release.GetAllAsync(this.ProjectName, releaseListRequest).Result;

            if (this.IncludeAllProperties)
            {
                this.PrintOrExport(releases.OrderByDescending(r => r.Id));
            }
            else
            {
                this.PrintOrExport(releases.Select(r => new { r.Id, r.Name })
                                           .OrderByDescending(r => r.Id));
            }

            return ExitCodes.Ok;
        }
    }
}
