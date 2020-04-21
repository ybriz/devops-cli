// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using System;
    using Jmelosegui.DevOps.Client;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("update", Description = "Update status of an approval.")]
    public class ReleaseApprovalUpdateCommand : CommandBase
    {
        public ReleaseApprovalUpdateCommand(ILogger<CommandBase> logger)
            : base(logger)
        {
        }

        [Option(
            "-aid|--approval-id",
            "Id of the approval to update",
            CommandOptionType.SingleValue)]
        public int ApprovalId { get; set; }

        [Option(
            "-s|--status",
            "Set the approval status. (valid values here https://docs.microsoft.com/en-us/rest/api/azure/devops/release/approvals/list?view=azure-devops-rest-5.1#approvalstatus)",
            CommandOptionType.SingleValue)]
        public string Status { get; set; }

        [Option(
            "-c|--comments",
            "Sets comments for approval.",
            CommandOptionType.SingleValue)]
        public string Comments { get; set; }

        public string OutputFile { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            base.OnExecute(app);

            ApprovalStatus status = ApprovalStatus.Undefined;

            while (this.ApprovalId <= 0)
            {
                int.TryParse(Prompt.GetString("> Approval Id:", null, ConsoleColor.DarkGray), out int approvalId);
                this.ApprovalId = approvalId;
            }

            Enum.TryParse(value: this.Status, ignoreCase: true, out status);

            while (status <= 0)
            {
                Enum.TryParse(Prompt.GetString("> Approval Status:", null, ConsoleColor.DarkGray), ignoreCase: true, out status);
            }

            while (string.IsNullOrEmpty(this.Comments))
            {
                this.Token = Prompt.GetString("> Comments:", null, ConsoleColor.DarkGray);
            }

            var request = new UpdateApprovalRequest
            {
                Id = this.ApprovalId,
                Status = status,
                Comments = this.Comments,
            };

            ReleaseApproval approval = this.DevOpsClient.Release.UpdateApprovalsAsync(this.ProjectName, request).Result;

            this.PrintOrExport(approval, this.OutputFile);

            return ExitCodes.Ok;
        }
    }
}
