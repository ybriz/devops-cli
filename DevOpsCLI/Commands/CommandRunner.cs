// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using McMaster.Extensions.CommandLineUtils;

    [Command("devops")]
    [Subcommand(typeof(BuildCommand))]
    [Subcommand(typeof(ReleaseCommand))]
    [Subcommand(typeof(ReleaseDefinitionCommand))]
    [Subcommand(typeof(VariableGroupCommand))]
    [HelpOption("-h|--help")]
    public sealed class CommandRunner
    {
        private int OnExecute(CommandLineApplication app)
        {
            app.ShowHint();
            return ExitCodes.UnknownError;
        }
    }
}