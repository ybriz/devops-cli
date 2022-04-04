// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json;
    using Jmelosegui.DevOps.Client;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("list", Description = "Get a list of agent queues.")]
    public class AgentQueueListCommand : ProjectCommandBase
    {
        public AgentQueueListCommand(ApplicationConfiguration settings, ILogger<AgentQueueListCommand> logger)
            : base(settings, logger)
        {
        }

        [Option(
        "--search-text",
        "Get Agent Queues with names starting with searchText.",
        CommandOptionType.SingleValue)]
        public string SearchText { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            base.OnExecute(app);

            AgentQueueListRequest request = null;
            if (!string.IsNullOrEmpty(this.SearchText))
            {
                request = new AgentQueueListRequest
                {
                    SearchText = this.SearchText,
                };
            }

            IEnumerable<AgentQueue> list = this.DevOpsClient.AgentQueue.GetAllAsync(this.ProjectName, request).GetAwaiter().GetResult();

            string json = JsonSerializer.Serialize(list);

            Console.WriteLine(json);

            return ExitCodes.Ok;
        }
    }
}
