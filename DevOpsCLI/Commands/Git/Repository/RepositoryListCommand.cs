// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using Jmelosegui.DevOps.Client.Models.Requests;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("list", Description = "Get a list of git repositories.")]
    public sealed class RepositoryListCommand : CommandBase
    {
        public RepositoryListCommand(ILogger<RepositoryListCommand> logger)
            : base(logger)
        {
        }

        [Option(
        "--include-hidden",
        "True to include hidden repositories.",
        CommandOptionType.SingleValue)]
        public bool IncludeHidden { get; set; }

        [Option(
        "--include-all-urls",
        "True to include all remote URLs.",
        CommandOptionType.SingleValue)]
        public bool IncludeAllUrls { get; set; }

        [Option(
        "--include-links",
        "True to include reference links.",
        CommandOptionType.SingleValue)]
        public bool IncludeLinks { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            base.OnExecute(app);

            var request = new RepositoryListRequest
            {
                IncludeHidden = this.IncludeHidden,
                IncludeAllUrls = this.IncludeAllUrls,
                IncludeLinks = this.IncludeLinks,
            };

            var result = this.DevOpsClient.Git.RepositoryGetAllAsync(this.ProjectName, request).GetAwaiter().GetResult();

            this.PrintOrExport(result);

            return ExitCodes.Ok;
        }
    }
}
