// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;
    using System.Linq;

    [Command("git", Description = "Commands for managing git repositories is Azure DevOps.")]
    [Subcommand(typeof(CommitCommand))]
    public sealed class GitCommand : CommandBase
    {
        public GitCommand(ILogger<GitCommand> logger)
            : base(logger)
        {
        }
    }
}
