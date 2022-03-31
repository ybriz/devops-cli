// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI
{
    using System;
    using System.Linq;
    using Jmelosegui.DevOps.Client;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("export", Description = "Show agent queue details.")]
    public sealed class AgentQueueExportCommand : CommandBase
    {
        public AgentQueueExportCommand(ILogger<CommandBase> logger)
            : base(logger)
        {
        }

        [Option(
            "--agent-queue-id",
            "Agent queue id",
            CommandOptionType.SingleValue)]
        public int AgentQueueId { get; set; }

        [Option(
        "--agent-queue-name",
        "Agent Queue Name",
        CommandOptionType.SingleValue)]
        public string AgentQueueName { get; set; }

        [Option(
            "--output-file",
            "File to export the agent queue details. If this value is not provided the output will be the console.",
            CommandOptionType.SingleValue)]
        public string OutputFile { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            base.OnExecute(app);

            while (this.AgentQueueId <= 0 && string.IsNullOrEmpty(this.AgentQueueName))
            {
                string value = Prompt.GetString("> AgentQueueName Id or Name:", null, ConsoleColor.DarkGray);

                if (int.TryParse(value, out int agentQueueId))
                {
                    this.AgentQueueId = agentQueueId;
                }
                else
                {
                    this.AgentQueueName = value;
                }
            }

            if (this.AgentQueueId == 0)
            {
                var agentQueue = this.GetAgentQueueByName(this.AgentQueueName);

                if (agentQueue != null)
                {
                    this.AgentQueueId = agentQueue.Id;
                }
                else
                {
                    Console.Error.WriteLine($"Cannot find an agent queue named: {this.AgentQueueName}");
                    return ExitCodes.ResourceNotFound;
                }
            }

            try
            {
                string variableGroup = this.DevOpsClient.AgentQueue.GetAsync(this.ProjectName, this.AgentQueueId).GetAwaiter().GetResult();

                this.PrintOrExport(variableGroup, this.OutputFile);

                return ExitCodes.Ok;
            }
            catch (NotFoundException ex)
            {
                Console.Error.WriteLine(ex.Message);
                return ExitCodes.ResourceNotFound;
            }
        }

        private AgentQueue GetAgentQueueByName(string value)
        {
            var request = new AgentQueueListRequest
            {
                SearchText = value,
            };

            AgentQueue agentQueue = this.DevOpsClient
                                                    .AgentQueue
                                                    .GetAllAsync(this.ProjectName, request)
                                                    .GetAwaiter()
                                                    .GetResult()
                                                    .FirstOrDefault(rd => rd.Name.Equals(value, StringComparison.OrdinalIgnoreCase));

            return agentQueue;
        }
    }
}
