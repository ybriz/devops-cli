// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using System;
    using System.Collections.Generic;
    using Jmelosegui.DevOpsCLI.Models;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("list", Description = "Get a list of release approvals.")]
    public class ReleaseApprovalListCommand : CommandBase
    {
        public ReleaseApprovalListCommand(ILogger<CommandBase> logger)
            : base(logger)
        {
        }

        [Option(
            "-s|--status",
            "Filter approvals with this status. Default is 'pending'. (valid values here https://docs.microsoft.com/en-us/rest/api/azure/devops/release/approvals/list?view=azure-devops-rest-5.1#approvalstatus)",
            CommandOptionType.SingleValue)]
        public string StatusFilter { get; set; }

        [Option(
            "-rid|--release-id",
            "Release id. You can specify this multiple times to include multiple releases in the filter",
            CommandOptionType.MultipleValue)]
        public IEnumerable<int> ReleaseIds { get; set; }

        [Option(
            "--output-file",
            "File to export the approval details. If this value is not provided the output will be the console.",
            CommandOptionType.SingleValue)]
        public string OutputFile { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            base.OnExecute(app);

            ApprovalStatus status = ApprovalStatus.Pending;

            if (!string.IsNullOrEmpty(this.StatusFilter))
            {
                Enum.TryParse(value: this.StatusFilter, ignoreCase: true, out status);

                while (status <= 0)
                {
                    Enum.TryParse(Prompt.GetString("> Approval Status:", null, ConsoleColor.DarkGray), ignoreCase: true, out status);
                }
            }

            var approvalList = this.DevOpsClient.Release.GetApprovalsAsync(this.ProjectName, this.ReleaseIds, status).Result;

            this.PrintOrExport(approvalList, this.OutputFile);

            return ExitCodes.Ok;
        }
    }
}
