// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("agent-queue", Description = "Commands for managing Agent Queues.")]
    [Subcommand(typeof(AgentQueueListCommand))]
    [Subcommand(typeof(AgentQueueExportCommand))]
    public class AgentQueueCommand : ProjectCommandBase
    {
        public AgentQueueCommand(ILogger<AgentQueueCommand> logger)
            : base(logger)
        {
        }
    }
}
