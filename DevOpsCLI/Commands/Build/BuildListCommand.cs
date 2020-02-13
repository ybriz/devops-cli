// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using System.Collections.Generic;
    using System.Linq;
    using Jmelosegui.DevOpsCLI.Models;
    using Jmelosegui.DevOpsCLI.Models.Requests;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("list", Description = "Get a list of builds.")]
    public class BuildListCommand : CommandBase
    {
        public BuildListCommand(ILogger<BuildListCommand> logger)
            : base(logger)
        {
        }

        [Option(
            "-bdid|--build-definition-id",
            "Release definition id to filter the resutl",
            CommandOptionType.SingleValue)]
        public int BuildDefinitionId { get; set; }

        [Option(
            "--top",
            "Number of releases to get. Default is 50.",
            CommandOptionType.SingleValue)]
        public int Top { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            base.OnExecute(app);

            var buildListRequest = new BuildListRequest
            {
                BuildDefinitionId = this.BuildDefinitionId,
                Top = this.Top,
            };

            IEnumerable<Build> builds = this.DevOpsClient.Build.GetAllAsync(this.ProjectName, buildListRequest).Result;

            this.PrintOrExport(builds.OrderByDescending(b => b.Id));

            return ExitCodes.Ok;
        }
    }
}
