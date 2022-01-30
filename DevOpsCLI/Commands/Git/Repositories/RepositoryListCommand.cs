// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using System;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("list", Description = "Retrieve a list of git repositories.")]
    public sealed class RepositoryListCommand : CommandBase
    {
        public RepositoryListCommand(ILogger<RepositoryListCommand> logger)
           : base(logger)
        {
        }

        [Option(
        "--include-all-urls",
        "[optional] True to include all remote URLs. The default value is false.",
        CommandOptionType.NoValue)]
        public bool IncludeAllUrls { get; set; }

        [Option(
        "--include-hidden",
        "[optional] True to include hidden repositories. The default value is false.",
        CommandOptionType.NoValue)]
        public bool IncludeHiddenRepositories{ get; set; }

        [Option(
        "--include-links",
        "[optional] True to include reference links. The default value is false.",
        CommandOptionType.NoValue)]
        public bool IncludeLinks { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            base.OnExecute(app);

            var result = this.DevOpsClient.Git.ListRepositoriesAsync(this.ProjectName, this.IncludeAllUrls, this.IncludeHiddenRepositories, this.IncludeLinks).GetAwaiter().GetResult();

            this.PrintOrExport(result);

            return ExitCodes.Ok;
        }
    }
}
