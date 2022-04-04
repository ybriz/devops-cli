// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("commit", Description = "Retrieve git commits for a project")]
    [Subcommand(typeof(CommitListCommand))]
    public sealed class CommitCommand : ProjectCommandBase
    {
        public CommitCommand(ApplicationConfiguration settings, ILogger<CommitCommand> logger)
            : base(settings, logger)
        {
        }
    }
}
