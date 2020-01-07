// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using System;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("release", Description = "Commands for managing Releases.")]
    [Subcommand(typeof(ReleaseCreateCommand))]
    [Subcommand(typeof(ReleaseListCommand))]
    public class ReleaseCommand : CommandBase
    {
        public ReleaseCommand(ILogger<ReleaseCommand> logger)
            : base(logger)
        {
        }
    }
}