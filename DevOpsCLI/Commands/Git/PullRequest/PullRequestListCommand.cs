// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using System;
    using System.IO;
    using System.Linq;
    using Jmelosegui.DevOps.Client.Models.Requests;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    [Command("list", Description = "Get a list of pull requests in a git repository.")]
    public sealed class PullRequestListCommand : ProjectCommandBase
    {
        public PullRequestListCommand(ApplicationConfiguration settings, ILogger<PullRequestListCommand> logger)
            : base(settings, logger)
        {
        }

        [Option(
        "--repository",
        "The id or friendly name of the repository.",
        CommandOptionType.SingleValue)]
        public string RepositoryId { get; set; }

        [Option(
        "--top",
        "Number of pull requests to get.",
        CommandOptionType.SingleValue)]
        public int Top { get; set; } = 100;

        [Option(
        "--skip",
        "Number of pull requests to skip.",
        CommandOptionType.SingleValue)]
        public int Skip { get; set; }

        [Option(
        "--timerange-type",
        "The type of time range on which the pull requests are filtered.",
        CommandOptionType.SingleValue)]
        public PullRequestTimeRangeType TimeRangeType { get; set; }

        [Option(
        "--min-time",
        "If specified, filters pull requests that created/closed after this date based on the queryTimeRangeType specified.",
        CommandOptionType.SingleValue)]
        public DateTime? MinTime { get; set; }

        [Option(
        "--max-time",
        "If specified, filters pull requests that created/closed before this date based on the queryTimeRangeType specified.",
        CommandOptionType.SingleValue)]
        public DateTime? MaxTime { get; set; }

        [Option(
        "--status",
        "The status of the pull request.",
        CommandOptionType.SingleValue)]
        public PullRequestStatus Status { get; set; }

        [Option(
        "--export-all",
        "If specified, retrieves and exports all available pull requests.",
        CommandOptionType.NoValue)]
        public bool ExportAll { get; set; }

        [Option(
        "--output-file",
        "File to export the pullrequest list. If this value is not provided the output will be the console. If --export-all is set this --output-file is required.",
        CommandOptionType.SingleValue)]
        public string OutputFile { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            base.OnExecute(app);

            var request = new PullRequestListRequest();

            request.RepositoryId = this.RepositoryId;
            request.MinTime = this.MinTime;
            request.MaxTime = this.MaxTime;
            request.TimeRageType = this.TimeRangeType;
            request.Status = this.Status;

            if (this.ExportAll)
            {
                if (string.IsNullOrEmpty(this.OutputFile))
                {
                    throw new ApplicationException("Output file must be specified when using --export-all.");
                }

                using (StreamWriter writer = new StreamWriter(this.OutputFile))
                {
                    int skip = 0;
                    int top = 500;
                    int iteration = 0;
                    int totalPr = 0;

                    var settings = new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    };

                    while (true)
                    {
                        request.Top = top;
                        request.Skip = skip;

                        var result = this.DevOpsClient.Git.GetPullRequestsAsync(this.ProjectName, request).GetAwaiter().GetResult();
                        var count = result.Count();
                        if (count == 0)
                        {
                            break;
                        }

                        if (skip == 0)
                        {
                            Console.WriteLine($"Exporting pull requests to {this.OutputFile}");
                        }

                        var outPutContent = JsonConvert.SerializeObject(result, settings);
                        writer.Write(outPutContent);

                        totalPr += count;
                        Console.WriteLine($"Iteration {++iteration}. Total pull request exported: {totalPr}.");

                        skip += top;
                    }
                }
            }
            else
            {
                request.Top = this.Top;
                request.Skip = this.Skip;

                var result = this.DevOpsClient.Git.GetPullRequestsAsync(this.ProjectName, request).GetAwaiter().GetResult();

                this.PrintOrExport(result);
            }

            this.DevOpsClient?.Dispose();

            return ExitCodes.Ok;
        }
    }
}
