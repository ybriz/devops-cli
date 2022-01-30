// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("create", Description = "Create a new repository.")]
    public sealed class RepositoryCreateCommand : CommandBase
    {
        public RepositoryCreateCommand(ILogger<RepositoryCreateCommand> logger)
           : base(logger)
        {
        }

        [Option(
        "-n|--name",
        "Name of the repository.",
        CommandOptionType.SingleValue)]
        [Required]
        public string Name { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            base.OnExecute(app);

            var result = this.DevOpsClient.Git.CreateRepositoryAsync(this.ProjectName, this.Name).GetAwaiter().GetResult();

            this.PrintOrExport(result);

            return ExitCodes.Ok;
        }
    }
}
