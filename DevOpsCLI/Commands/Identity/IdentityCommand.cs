// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Subcommand(typeof(IdentityListCommand))]
    [Command("identity", Description = "Resolve legacy identity information for use with older APIs such as the Security APIs.")]
    public class IdentityCommand : CommandBase
    {
        public IdentityCommand(ApplicationConfiguration settings, ILogger<IdentityCommand> logger)
            : base(settings, logger)
        {
        }
    }
}
