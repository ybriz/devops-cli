namespace Jmelosegui.DevOpsCLI
{
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("agent-queue", Description = "Commands for managing Agent Queues.")]
    [Subcommand(typeof(AgentQueueListCommand))]
    [Subcommand(typeof(AgentQueueExportCommand))]
    public class AgentQueueCommand : CommandBase
    {
        public AgentQueueCommand(ILogger<AgentQueueCommand> logger)
            : base(logger)
        {
        }
    }
}
