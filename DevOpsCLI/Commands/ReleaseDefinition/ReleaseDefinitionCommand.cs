// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("release-definition", Description = "Commands for managing Release Definitions.")]
    [Subcommand(typeof(ReleaseDefinitionListCommand))]
    [Subcommand(typeof(ReleaseDefinitionExportCommand))]
    public class ReleaseDefinitionCommand : CommandBase
    {
        public ReleaseDefinitionCommand(ILogger<ReleaseCommand> logger)
            : base(logger)
        {
        }
    }
}