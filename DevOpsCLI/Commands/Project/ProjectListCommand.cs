// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using Jmelosegui.DevOps.Client.Models.Requests;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("list", Description = "Get all projects in the organization that the authenticated user has access to.")]
    public class ProjectListCommand : CommandBase
    {
        public ProjectListCommand(ILogger<ProjectExportCommand> logger)
            : base(logger)
        {
        }

        protected override int OnExecute(CommandLineApplication app)
        {
            base.OnExecute(app);

            var request = new TeamProjectListRequest();

            var list = this.DevOpsClient.Project.GetAllAsync(request).GetAwaiter().GetResult();

            this.PrintOrExport(list);

            return ExitCodes.Ok;
        }
    }
}
