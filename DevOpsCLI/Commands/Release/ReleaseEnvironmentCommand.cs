// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("environment", Description = "Update the status of a release environment.")]
    [Subcommand(typeof(ReleaseEnvironmentUpdateCommand))]
    public class ReleaseEnvironmentCommand : CommandBase
    {
        public ReleaseEnvironmentCommand(ILogger<CommandBase> logger)
            : base(logger)
        {
        }
    }
}
