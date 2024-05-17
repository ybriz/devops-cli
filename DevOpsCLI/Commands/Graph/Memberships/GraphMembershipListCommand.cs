// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands.Graph.Memberships
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json;
    using Jmelosegui.DevOps;
    using Jmelosegui.DevOps.Client;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("list", Description = "Gets a list of all Memberships in the current scope (usually organization or account).")]
    public sealed class GraphMembershipListCommand : CommandBase
    {
        public GraphMembershipListCommand(ApplicationConfiguration settings, ILogger<GraphMembershipListCommand> logger)
            : base(settings, logger)
        {
        }

        [Option(
          "--subject-descriptor",
          "Specify a non-default scope (collection, project) to search for Memberships.",
          CommandOptionType.SingleValue)]
        public string SubjectDescriptor { get; set; }

        [Option(
          "--direction",
          "Specify a direction (up, down) to search for Memberships.",
          CommandOptionType.SingleValue)]
        public string Direction { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            base.OnExecute(app);

            var request = new GraphMembershipListRequest
            {
                SubjectDescriptor = this.SubjectDescriptor,
                Direction = this.Direction,
            };

            IEnumerable<GraphMembership> list = this.DevOpsClient.Graph.MembershipGetAllAsync(request).GetAwaiter().GetResult();

            Console.WriteLine();

            string json = JsonSerializer.Serialize(list);

            Console.WriteLine(json);

            return ExitCodes.Ok;
        }
    }
}
