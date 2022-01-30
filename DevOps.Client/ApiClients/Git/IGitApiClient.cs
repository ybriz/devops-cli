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
        /// <returns>Returns an IEnumerable of <see cref="CommitRef"/>.</returns>
        Task<IEnumerable<CommitRef>> GetCommitsAsync(string projectName, CommitListRequest commitListRequest = null);

        /// <summary>
        /// Retrieve git repositories.
        /// </summary>
        /// <param name="projectName">Project ID or project name.</param>
        /// <param name="includeAllUrls">[optional] True to include all remote URLs. The default value is false.</param>
        /// <param name="includeHiddenRepositories">[optional] True to include hidden repositories. The default value is false.</param>
        /// <param name="includeLinks">[optional] True to include reference links. The default value is false.</param>
        /// <returns>A list of <see cref="GitRepository"/>.</returns>
        Task<IEnumerable<GitRepository>> ListRepositoriesAsync(string projectName, bool includeAllUrls = false, bool includeHiddenRepositories = false, bool includeLinks = false);
    }
}
