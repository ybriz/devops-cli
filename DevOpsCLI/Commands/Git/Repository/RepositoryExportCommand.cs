// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;
    using System;

    [Command("export", Description = "Retrieve a git repository.")]
    public sealed class RepositoryExportCommand : ProjectCommandBase
    {
        public RepositoryExportCommand(ILogger<RepositoryExportCommand> logger)
            : base(logger)
        {
        }

        [Option(
        "--repositoryId",
        "The name or ID of the repository.",
        CommandOptionType.SingleValue)]
        public string RepositoryId { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            base.OnExecute(app);

            while (!string.IsNullOrEmpty(this.RepositoryId))
            {
                this.RepositoryId = Prompt.GetString("> Pipeline Id", null, ConsoleColor.DarkGray);
            }

            var result = this.DevOpsClient.Git.RepositoryGetAsync(this.ProjectName, this.RepositoryId).GetAwaiter().GetResult();

            this.PrintOrExport(result);

            return ExitCodes.Ok;
        }
    }
}
