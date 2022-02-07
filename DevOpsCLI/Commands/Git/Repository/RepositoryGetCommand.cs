// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("export", Description = "Get a list of git repositories.")]
    public sealed class RepositoryGetCommand : CommandBase
    {
        public RepositoryGetCommand(ILogger<RepositoryGetCommand> logger)
            : base(logger)
        {
        }

        [Option(
        "--repositoryId",
        "The name or ID of the repository.",
        CommandOptionType.SingleValue)]
        public string RepositoryId { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            base.OnExecute(app);

            var result = this.DevOpsClient.Git.RepositoryGetAsync(this.ProjectName, this.RepositoryId).GetAwaiter().GetResult();

            this.PrintOrExport(result);

            return ExitCodes.Ok;
        }
    }
}
