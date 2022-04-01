// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using System;
    using System.Linq;
    using Jmelosegui.DevOps.Client;
    using Jmelosegui.DevOps.Client.Models.Requests;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("export", Description = "Get project with the specified id or name, optionally including capabilities.")]
    public class ProjectExportCommand : CommandBase
    {
        public ProjectExportCommand(ILogger<ProjectExportCommand> logger)
            : base(logger)
        {
        }

        [Option(
            "--project-id",
            "Project id",
            CommandOptionType.SingleValue)]
        public Guid ProjectId { get; set; }

        [Option(
        "--project-name",
        "Project name",
        CommandOptionType.SingleValue)]
        public string ProjectName { get; set; }

        [Option(
            "--output-file",
            "File to export the project details. If this value is not provided the output will be the console.",
            CommandOptionType.SingleValue)]
        public string OutputFile { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            base.OnExecute(app);

            while (this.ProjectId == Guid.Empty && string.IsNullOrEmpty(this.ProjectName))
            {
                string value = Prompt.GetString("> ProjectId:", null, ConsoleColor.DarkGray);
                if (Guid.TryParse(value, out Guid projectId))
                {
                    this.ProjectId = projectId;
                }
                else
                {
                    this.ProjectName = value;
                }
            }

            if (this.ProjectId == Guid.Empty)
            {
                var project = this.GetProjectByName(this.ProjectName);

                if (project != null)
                {
                    this.ProjectId = Guid.Parse(project.Id);
                }
                else
                {
                    Console.Error.WriteLine($"Cannot find a project named: {this.ProjectName}");
                    return ExitCodes.ResourceNotFound;
                }
            }

            try
            {
                string project = this.DevOpsClient.Project.GetAsync(this.ProjectId).GetAwaiter().GetResult();

                this.PrintOrExport(project, this.OutputFile);

                return ExitCodes.Ok;
            }
            catch (NotFoundException ex)
            {
                Console.Error.WriteLine(ex.Message);
                return ExitCodes.ResourceNotFound;
            }
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
