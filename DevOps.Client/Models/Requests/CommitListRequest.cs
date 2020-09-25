// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client.Models.Requests
{
    public sealed class CommitListRequest
    {
        /// <summary>
        /// Gets or sets the id or friendly name of the repository.
        /// </summary>
        public string RepositoryId { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of entries to retrieve.
        /// </summary>
        public int Top { get; set; }

        /// <summary>
        /// Gets or sets the version string identifier (name of tag/branch, SHA1 of commit).
        /// </summary>
        public string SearchCriteriaVersion { get; set; }
    }
}
