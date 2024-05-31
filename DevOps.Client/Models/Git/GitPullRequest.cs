// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides properties that describe a Git pull request.
    /// </summary>
    public sealed class GitPullRequest
    {
        /// <summary>
        /// Gets or sets the repository that contains the target branch of the pull request.
        /// </summary>
        public GitRepository Repository { get; set; }

        /// <summary>
        /// Gets or sets the id of the pull request.
        /// </summary>
        public int PullRequestId { get; set; }

        /// <summary>
        /// Gets or sets the id of the code review.
        /// </summary>
        public int CodeReviewId { get; set; }

        /// <summary>
        /// Gets or sets the status of the pull request.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the title of the pull request.
        /// </summary>
        public IdentityRef CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the creation date of the pull request.
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the title of the pull request.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description of the pull request.
        /// </summary>
        /// <remarks>
        /// Please note that description field will be truncated up to 400 symbols in the result.
        /// </remarks>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the source branch of the pull request.
        /// </summary>
        public string SourceRefName { get; set; }

        /// <summary>
        /// Gets or sets the target branch of the pull request.
        /// </summary>
        public string TargetRefName { get; set; }

        /// <summary>
        /// Gets or sets the merge status of the pull request.
        /// </summary>
        public string MergeStatus { get; set; }

        /// <summary>
        /// Gets or sets the merge id of the pull request.
        /// </summary>
        public string MergeId { get; set; }

        /// <summary>
        /// Gets or sets the merge commit id of the pull request.
        /// </summary>
        public GitCommitRef LastMergeSourceCommit { get; set; }

        /// <summary>
        /// Gets or sets the merge commit id of the pull request.
        /// </summary>
        public GitCommitRef LastMergeTargetCommit { get; set; }

        /// <summary>
        /// Gets or sets the merge commit id of the pull request.
        /// </summary>
        public GitCommitRef LastMergeCommit { get; set; }

        /// <summary>
        /// Gets or sets the reviewers of the pull request.
        /// </summary>
        public IEnumerable<IdentityRef> Reviewers { get; set; }

        /// <summary>
        /// Gets or sets the url of the pull request.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the pull request support iterations.
        /// </summary>
        /// <remarks>
        /// If true, this pull request supports multiple iterations.
        /// Iteration support means individual pushes to the source branch of the pull request can
        /// be reviewed and comments left in one iteration will be tracked across future iterations.
        /// </remarks>
        public bool SupportsIterations { get; set; }
    }
}
