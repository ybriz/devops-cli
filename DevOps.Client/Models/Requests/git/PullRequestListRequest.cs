// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client.Models.Requests
{
    using System;

    public sealed class PullRequestListRequest
    {
        /// <summary>
        /// Gets or sets the id or friendly name of the repository.
        /// </summary>
        public string RepositoryId { get; set; }

        /// <summary>
        /// Gets or sets the status of the pull request.
        /// </summary>
        public PullRequestStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the type of time range which should be used for minTime and maxTime. Defaults to Created if unset.
        /// </summary>
        public PullRequestTimeRangeType TimeRageType { get; set; }

        /// <summary>
        /// Gets or sets the request min datetime. If specified, filters pull requests that created/closed after this date based on the queryTimeRangeType specified.
        /// </summary>
        public DateTime? MinTime { get; set; }

        /// <summary>
        /// Gets or sets the request max datetime. If specified, filters pull requests that created/closed before this date based on the queryTimeRangeType specified.
        /// </summary>
        public DateTime? MaxTime { get; set; }

        /// <summary>
        /// Gets or sets the number of pull requests to get.
        /// </summary>
        public int Top { get; set; }

        /// <summary>
        /// Gets or sets the number of pull requests to skip.
        /// </summary>
        public int Skip { get; set; }
    }
}
