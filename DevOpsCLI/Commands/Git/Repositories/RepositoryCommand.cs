// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("repository", Description = "Command group to work with git repostories")]
    [Subcommand(typeof(RepositoryListCommand))]
    [Subcommand(typeof(RepositoryCreateCommand))]
    public sealed class RepositoryCommand : CommandBase
    {
        public RepositoryCommand(ILogger<RepositoryCommand> logger)
            : base(logger)
        {
        }
    }
}
