// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("git", Description = "Commands for work with git repositories is Azure DevOps.")]
    [Subcommand(typeof(CommitCommand))]
    [Subcommand(typeof(RepositoryCommand))]
    [Subcommand(typeof(PullRequestCommand))]
    public sealed class GitCommand : ProjectCommandBase
    {
        public GitCommand(ApplicationConfiguration settings, ILogger<GitCommand> logger)
            : base(settings, logger)
        {
        }
    }
}
