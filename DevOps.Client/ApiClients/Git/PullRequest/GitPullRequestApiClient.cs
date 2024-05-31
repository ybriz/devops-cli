// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Jmelosegui.DevOps.Client.Models;
    using Jmelosegui.DevOps.Client.Models.Requests;

    public sealed partial class GitApiClient
    {
        public async Task<IEnumerable<GitPullRequest>> GetPullRequestsAsync(string projectName, PullRequestListRequest pullRequestListRequest)
        {
            var parameters = new Dictionary<string, object>();

            FluentDictionary.For(parameters)
                            .Add("api-version", "7.1-preview.1")
                            .Add("searchCriteria.repositoryId", pullRequestListRequest.RepositoryId, () => !string.IsNullOrWhiteSpace(pullRequestListRequest.RepositoryId))
                            .Add("searchCriteria.minTime", pullRequestListRequest.MinTime, () => pullRequestListRequest.MinTime.HasValue)
                            .Add("searchCriteria.maxTime", pullRequestListRequest.MaxTime, () => pullRequestListRequest.MaxTime.HasValue)
                            .Add("searchCriteria.queryTimeRangeType", pullRequestListRequest.TimeRageType, () => pullRequestListRequest.TimeRageType != PullRequestTimeRangeType.Created)
                            .Add("searchCriteria.status", pullRequestListRequest.Status, () => pullRequestListRequest.Status != PullRequestStatus.NotSet)
                            .Add("$top", pullRequestListRequest.Top, () => pullRequestListRequest.Top > 0)
                            .Add("$skip", pullRequestListRequest.Skip, () => pullRequestListRequest.Skip > 0);

            var endPoint = new Uri($"{projectName}/_apis/git/pullrequests", UriKind.Relative);

            var response = await this.Connection.Get<GenericCollectionResponse<GitPullRequest>>(endPoint, parameters, null)
                                .ConfigureAwait(false);

            return response.Body.Values;
        }
    }
}
