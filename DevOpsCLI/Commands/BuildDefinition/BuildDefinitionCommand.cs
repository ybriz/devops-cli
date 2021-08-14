// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("build-definition", Description = "Commands for managing builds definitnions.")]
    [Subcommand(typeof(BuildDefinitionListCommand))]
    [Subcommand(typeof(BuildDefinitionExportCommand))]
    [Subcommand(typeof(BuildDefinitionImportCommand))]
    public class BuildDefinitionCommand : CommandBase
    {
        public BuildDefinitionCommand(ILogger<BuildDefinitionCommand> logger)
            : base(logger)
        {
        }
    }
}