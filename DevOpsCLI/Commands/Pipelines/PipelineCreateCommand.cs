// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using System;
    using Jmelosegui.DevOps.Client.Models;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("create", Description = "Create a pipeline.")]
    internal class PipelineCreateCommand : ProjectCommandBase
    {
        public PipelineCreateCommand(ILogger<PipelineCreateCommand> logger)
            : base(logger)
        {
        }

        [Option(
            "-n|--name",
            "Pipeline Name",
            CommandOptionType.SingleValue)]
        public string Name { get; set; }

        [Option(
        "--repositoryId",
        "Id or Nameof the repository.",
        CommandOptionType.SingleValue)]
        public string ReposirotyId { get; set; }

        [Option(
            "-f|--folder-name",
            "Pipeline Folder Name",
            CommandOptionType.SingleValue)]
        public string Folder { get; set; }

        [Option(
            "--yaml",
            "Path to the yaml file within the repository.",
            CommandOptionType.SingleValue)]
        public string YamlContent { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            base.OnExecute(app);

            while (string.IsNullOrEmpty(this.Name))
            {
                this.Name = Prompt.GetString("> Pipeline Name:", null, ConsoleColor.DarkGray);
            }

            while (string.IsNullOrEmpty(this.ReposirotyId))
            {
                this.ReposirotyId = Prompt.GetString("> Repository Name or Id:", null, ConsoleColor.DarkGray);
            }

            var repository = this.DevOpsClient.Git.RepositoryGetAsync(this.ProjectName, this.ReposirotyId).GetAwaiter().GetResult();

            var configuration = new PipelineConfiguration
            {
                Path = "azure-pipeline.yml",
                Repository = new Repository
                {
                    Id = Guid.Parse(repository.Id),
                    Type = "azureReposGit",
                },
                Type = "yaml",
            };

            var request = new PipelineCreateRequest
            {
                Name = this.Name,
                Configuration = configuration,
            };

            Pipeline pipeline = this.DevOpsClient.Pipeline.CreateAsync(this.ProjectName, request).GetAwaiter().GetResult();

            this.PrintOrExport(pipeline);

            return ExitCodes.Ok;
        }
    }
}
