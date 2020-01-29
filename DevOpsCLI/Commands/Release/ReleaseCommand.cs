// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("release", Description = "Commands for managing Releases.")]
    [Subcommand(typeof(ReleaseListCommand))]
    [Subcommand(typeof(ReleaseCreateCommand))]
    [Subcommand(typeof(ReleaseExportCommand))]
    [Subcommand(typeof(ReleaseEnvironmentCommand))]
    public class ReleaseCommand : CommandBase
    {
        public ReleaseCommand(ILogger<ReleaseCommand> logger)
            : base(logger)
        {
        }
    }
}