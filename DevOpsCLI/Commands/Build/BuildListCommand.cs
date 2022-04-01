// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using System.Collections.Generic;
    using System.Linq;
    using Jmelosegui.DevOps.Client;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("list", Description = "Get a list of builds.")]
    public class BuildListCommand : ProjectCommandBase
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

        [Option(
            "-bn|--build-number",
            "Buil Number to filter response",
            CommandOptionType.SingleValue)]
        public string BuildNumber { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            base.OnExecute(app);

            var buildListRequest = new BuildListRequest
            {
                BuildDefinitionId = this.BuildDefinitionId,
                Top = this.Top,
                BuildNumber = this.BuildNumber,
            };

            IEnumerable<Build> builds = this.DevOpsClient.Build.GetAllAsync(this.ProjectName, buildListRequest).GetAwaiter().GetResult();

            this.PrintOrExport(builds.OrderByDescending(b => b.Id));

            return ExitCodes.Ok;
        }
    }
}
