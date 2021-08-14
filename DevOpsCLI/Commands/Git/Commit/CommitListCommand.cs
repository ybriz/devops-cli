// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using System;
    using Jmelosegui.DevOps.Client.Models.Requests;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("list", Description = "Get a list of commits in a git repository.")]
    public sealed class CommitListCommand : CommandBase
    {
        public CommitListCommand(ILogger<CommitListCommand> logger)
            : base(logger)
        {
        }

        [Option(
        "--repository",
        "The id or friendly name of the repository.",
        CommandOptionType.SingleValue)]
        public string RepositoryId { get; set; }

        [Option(
        "-scv|--searchCriteria-version",
        "Version string identifier (name of tag/branch, SHA1 of commit)",
        CommandOptionType.SingleValue)]
        public string SearchCriteriaVersion { get; set; }

        [Option(
        "--top",
        "Number of commits to get. Default is 50.",
        CommandOptionType.SingleValue)]
        public int Top { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            base.OnExecute(app);

            var request = new CommitListRequest();

            while (string.IsNullOrEmpty(this.RepositoryId))
            {
                this.RepositoryId = Prompt.GetString("> RepositoryId:", null, ConsoleColor.DarkGray);
            }

            request.RepositoryId = this.RepositoryId;
            request.SearchCriteriaVersion = this.SearchCriteriaVersion;
            request.Top = this.Top;

            var result = this.DevOpsClient.Git.GetCommits(this.ProjectName, request).GetAwaiter().GetResult();

            this.PrintOrExport(result);

            return ExitCodes.Ok;
        }
    }
}
