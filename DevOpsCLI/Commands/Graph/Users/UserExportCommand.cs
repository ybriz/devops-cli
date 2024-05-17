// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands.Graph.Users
{
    using System;
    using System.Linq;
    using Jmelosegui.DevOps;
    using Jmelosegui.DevOps.Client;
    using Jmelosegui.DevOps.Client.Models.Requests;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("export", Description = "Get a User by its descriptor.")]
    public sealed class UserExportCommand : CommandBase
    {
        public UserExportCommand(ApplicationConfiguration settings, ILogger<UserExportCommand> logger)
            : base(settings, logger)
        {
        }

        [Option(
          "--user-descriptor",
          "The descriptor of the desired graph User.",
          CommandOptionType.SingleValue)]
        public string UserDescriptor { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            base.OnExecute(app);

            GraphUser graphUser = null;

            if (!string.IsNullOrEmpty(this.UserDescriptor))
            {
                graphUser = this.DevOpsClient.Graph.UserGetAsync(this.UserDescriptor).GetAwaiter().GetResult();
            }

            if (graphUser == null)
            {
                throw new Exception("User not found with the provided parameters");
            }

            this.PrintOrExport(graphUser);

            return ExitCodes.Ok;
        }
    }
}
