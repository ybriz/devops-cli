// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("pull-request", Description = "Retrieve git pull requests for a project")]
    [Subcommand(typeof(PullRequestListCommand))]
    public sealed class PullRequestCommand : ProjectCommandBase
    {
        public PullRequestCommand(ApplicationConfiguration settings, ILogger<CommitCommand> logger)
            : base(settings, logger)
        {
        }
    }
}
