// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("approval", Description = "Operation related with release approval process.")]
    [Subcommand(typeof(ReleaseApprovalListCommand))]
    [Subcommand(typeof(ReleaseApprovalUpdateCommand))]
    public class ReleaseApprovalCommand : ProjectCommandBase
    {
        public ReleaseApprovalCommand(ApplicationConfiguration settings, ILogger<ReleaseApprovalCommand> logger)
            : base(settings, logger)
        {
        }
    }
}
