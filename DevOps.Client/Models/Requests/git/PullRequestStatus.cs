// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client.Models.Requests
{
    /// <summary>
    /// Defines the status of the pull request.
    /// </summary>
    public enum PullRequestStatus
    {
        NotSet,
        Active,
        Completed,
        Abandoned,
        All,
    }
}
