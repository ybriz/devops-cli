// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client
{
    public class IdentityListRequest
    {
        /// <summary>
        /// Gets or sets descriptors.
        /// A comma separated list of identity descriptors to resolve.
        /// </summary>
        public string Descriptors { get; set; }

        /// <summary>
        /// Gets or sets QueryMembership.
        /// The membership information to include with the identities.
        /// Values can be None for no membership data or Direct to include the groups that the identity is a
        /// member of and the identities that are a member of this identity (groups only).
        /// </summary>
        public QueryMembership QueryMembership { get; set; }

        /// <summary>
        /// Gets or sets FilterValue.
        /// The search value, as specified by the searchFilter.
        /// </summary>
        public string FilterValue { get; set; }

        /// <summary>
        /// Gets or sets IdentityIds.
        /// A comma seperated list of storage keys to resolve.
        /// </summary>
        public string IdentityIds { get; set; }

        /// <summary>
        /// Gets or sets the type of search to perform.
        /// Values can be AccountName (domain\alias), DisplayName, MailAddress,
        /// General (display name, account name, or unique name), or LocalGroupName (only search Azure Devops groups).
        /// </summary>
        public string SearchFilter { get; set; }

        /// <summary>
        /// Gets or sets SubjectDescriptors
        /// A comma seperated list of subject descriptors to resolve.
        /// </summary>
        public string SubjectDescriptors { get; set; }
    }
}
