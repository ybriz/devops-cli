// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands.Graph.Groups
{
    using Jmelosegui.DevOps;
    using Jmelosegui.DevOps.Client;
    using Jmelosegui.DevOps.Client.Models.Requests;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Linq;

    [Command("export", Description = "Get a group by its descriptor.")]
    public sealed class GroupExportCommand : CommandBase
    {
        public GroupExportCommand(ILogger<GroupExportCommand> logger)
            : base(logger)
        {
        }

        [Option(
          "--group-descriptor",
          "The descriptor of the desired graph group.",
          CommandOptionType.SingleValue)]
        public string GroupDescriptor { get; set; }

        [Option(
          "--group-name",
          "The name of the group to export.",
          CommandOptionType.SingleValue)]
        public string GroupName { get; set; }

        [Option(
          "--project-name",
          "Name of the project to reduce the search of the group when --group-name is used.",
          CommandOptionType.SingleValue)]
        public string ProjectName { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            base.OnExecute(app);

            while (string.IsNullOrEmpty(this.GroupDescriptor) && string.IsNullOrEmpty(this.GroupName))
            {
                throw new InvalidOperationException("Provide either --group-name or --group-descriptor");
            }

            GraphGroup graphGroup = null;

            if (!string.IsNullOrEmpty(this.GroupDescriptor))
            {
                graphGroup = this.DevOpsClient.Graph.GroupGetAsync(this.GroupDescriptor).GetAwaiter().GetResult();
            }
            else
            {
                TeamProjectReference project = null;
                if (!string.IsNullOrEmpty(this.ProjectName))
                {
                    project = this.GetProjectByName(this.ProjectName);
                }

                GraphGroupListRequest request = null;
                if (project != null)
                {
                    var projectDescriptor = this.DevOpsClient.Graph.DescriptorGetAsync(project.Id.ToString()).GetAwaiter().GetResult();
                    request = new GraphGroupListRequest
                    {
                        ScopeDescriptor = projectDescriptor.Value,
                    };
                }

                var list = this.DevOpsClient.Graph.GroupGetAllAsync(request)
                                                    .GetAwaiter()
                                                    .GetResult();

                graphGroup = list.FirstOrDefault(rd => rd.PrincipalName.Equals(this.GroupName, StringComparison.OrdinalIgnoreCase));
            }

            if (graphGroup == null)
            {
                throw new Exception("Group not found with the provided parameters");
            }

            this.PrintOrExport(graphGroup);

            return ExitCodes.Ok;
        }

        private TeamProjectReference GetProjectByName(string projectName)
        {
            TeamProjectReference project = this.DevOpsClient
                                                    .Project
                                                    .GetAllAsync(new TeamProjectListRequest())
                                                    .GetAwaiter()
                                                    .GetResult()
                                                    .FirstOrDefault(rd => rd.Name.Equals(projectName, StringComparison.OrdinalIgnoreCase));

            return project;
        }
    }
}
