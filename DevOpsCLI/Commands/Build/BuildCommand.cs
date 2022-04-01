// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("build", Description = "Commands for managing Builds.")]
    [Subcommand(typeof(BuildListCommand))]
    public class BuildCommand : ProjectCommandBase
    {
        public BuildCommand(ILogger<BuildCommand> logger)
            : base(logger)
        {
        }
    }
}