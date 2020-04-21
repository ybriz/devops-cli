// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("variable-group", Description = "Commands for managing Variable Groups.")]
    [Subcommand(typeof(VariableGroupListCommand))]
    [Subcommand(typeof(VariableGroupExportCommand))]
    [Subcommand(typeof(VariableGroupImportCommand))]
    public class VariableGroupCommand : CommandBase
    {
        public VariableGroupCommand(ILogger<VariableGroupCommand> logger)
            : base(logger)
        {
        }
    }
}