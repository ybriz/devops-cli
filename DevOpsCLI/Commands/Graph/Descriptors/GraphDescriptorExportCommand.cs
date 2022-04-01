// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("export", Description = "Resolve a storage key to a descriptor.")]
    internal class GraphDescriptorExportCommand : CommandBase
    {
        public GraphDescriptorExportCommand(ILogger<GraphDescriptorExportCommand> logger)
            : base(logger)
        {
        }

        [Option(
          "--storage-key",
          "Storage key of the subject (user, group, scope, etc.) to resolve.",
          CommandOptionType.SingleValue)]
        public string StorageKey { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            base.OnExecute(app);

            while (string.IsNullOrEmpty(this.StorageKey))
            {
                this.StorageKey = Prompt.GetString("> Storage Key", null, System.ConsoleColor.DarkGray);
            }

            var result = this.DevOpsClient.Graph.DescriptorGetAsync(this.StorageKey).GetAwaiter().GetResult();

            this.PrintOrExport(result);

            return ExitCodes.Ok;
        }
    }
}
