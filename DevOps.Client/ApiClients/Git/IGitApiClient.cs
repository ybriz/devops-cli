// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Jmelosegui.DevOps.Client.Models;
    using Jmelosegui.DevOps.Client.Models.Requests;

    public interface IGitApiClient
    {
        /// <summary>
        /// Retrieve git commits for a project.
        /// </summary>
        /// <param name="projectName">Project ID or project name.</param>
        /// <param name="commitListRequest">Payload used in the request.</param>
        /// <returns>Returns an IEnumerable of <see cref="GitCommitRef"/>.</returns>
        Task<IEnumerable<GitCommitRef>> GetCommits(string projectName, CommitListRequest commitListRequest = null);

        /// <summary>
        /// Retrieve a git reposiroty by id or name.
        /// </summary>
        /// <param name="projectName">Project ID or project name.</param>
        /// <param name="repositoryId">The name or ID of the repository.</param>
        /// <returns><see cref="GitRepository"/>.</returns>
        Task<GitRepository> RepositoryGetAsync(string projectName, string repositoryId);

        /// <summary>
        /// Retrieve git repositories.
        /// </summary>
        /// <param name="projectName">Project Name.</param>
        /// <param name="request">Payload used in the request <see cref="RepositoryListRequest"/>.</param>
        /// <returns>IEnumerable of <see cref="GitRepository"/>.</returns>
        Task<IEnumerable<GitRepository>> RepositoryGetAllAsync(string projectName, RepositoryListRequest request);

        /// <summary>
        /// Retrieve pull requests for a project.
        /// </summary>
        /// <param name="projectName">Project Name.</param>
        /// <param name="request">Payload used in the request <see cref="PullRequestListRequest"/>.</param>
        /// <returns>IEnumerable of <see cref="GitPullRequest"/>.</returns>
        Task<IEnumerable<GitPullRequest>> GetPullRequestsAsync(string projectName, PullRequestListRequest request);
    }
}
