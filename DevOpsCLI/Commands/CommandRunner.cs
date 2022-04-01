// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using System;
    using McMaster.Extensions.CommandLineUtils;

    [Command("devops")]
    [Subcommand(typeof(BuildCommand))]
    [Subcommand(typeof(BuildDefinitionCommand))]
    [Subcommand(typeof(GitCommand))]
    [Subcommand(typeof(PipelineCommand))]
    [Subcommand(typeof(ReleaseCommand))]
    [Subcommand(typeof(ReleaseDefinitionCommand))]
    [Subcommand(typeof(VariableGroupCommand))]
    [Subcommand(typeof(TaskGroupCommand))]
    [Subcommand(typeof(AgentQueueCommand))]
    [Subcommand(typeof(GraphCommand))]
    [Subcommand(typeof(IdentityCommand))]
    [Subcommand(typeof(ProjectCommand))]
    [HelpOption("-h|--help")]
    public sealed class CommandRunner
    {
        [Option(
            "-v|--version",
            "Gets the version of the devops cli tool.",
            CommandOptionType.NoValue)]
        public bool ShowVersion { get; set; }

        private int OnExecute(CommandLineApplication app)
        {
            if (this.ShowVersion)
            {
                Console.WriteLine(ThisAssembly.AssemblyInformationalVersion);
            }
            else
            {
                app.ShowHint();
            }

            return ExitCodes.Ok;
        }
    }
}