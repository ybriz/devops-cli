// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json;
    using Jmelosegui.DevOps.Client;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("list", Description = "Resolve legacy identity information for use with older APIs such as the Security APIs.")]
    public class IdentityListCommand : CommandBase
    {
        public IdentityListCommand(ILogger<IdentityListCommand> logger)
            : base(logger)
        {
        }

        [Option(
          "--identity-ids",
          "Specify a non-default scope (collection, project) to search for groups.",
          CommandOptionType.SingleValue)]
        public string IdentityIds { get; set; }

        [Option(
          "--query-membership",
          "The membership information to include with the identities. Values can be None for no membership data or Direct to include the groups that the identity is a member of and the identities that are a member of this identity (groups only).",
          CommandOptionType.SingleValue)]
        public QueryMembership QueryMembership { get; set; }

        [Option(
          "--search-filter",
          "The type of search to perform. Values can be AccountName (domain\alias), DisplayName, MailAddress, General (display name, account name, or unique name), or LocalGroupName (only search Azure Devops groups).",
          CommandOptionType.SingleValue)]
        public string SearchFilter { get; set; }

        [Option(
          "--filter-value",
          "The search value, as specified by the searchFilter.",
          CommandOptionType.SingleValue)]
        public string FilterValue { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            base.OnExecute(app);

            IdentityListRequest request = new IdentityListRequest
            {
                IdentityIds = this.IdentityIds,
                QueryMembership = this.QueryMembership,
                SearchFilter = this.SearchFilter,
                FilterValue = this.FilterValue,
            };

            var list = this.DevOpsClient.Identity
                                                          .GetAllAsync(request)
                                                          .GetAwaiter()
                                                          .GetResult()
                                                          .Select(i => new
                                                          {
                                                              i.Id,
                                                              i.IsContainer,
                                                              i.UniqueUserId,
                                                              i.SocialDescriptor,
                                                              i.SubjectDescriptor,
                                                              i.CustomDisplayName,
                                                              i.IsActive,
                                                              i.ResourceVersion,
                                                              Mail = i.Properties["Mail"]?["$value"]?.Value,
                                                          }).ToList();



            Console.WriteLine();

            string json = JsonSerializer.Serialize(list);

            Console.WriteLine(json);

            return ExitCodes.Ok;
        }
    }
}
