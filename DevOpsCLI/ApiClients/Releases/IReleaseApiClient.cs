// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.ApiClients
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Jmelosegui.DevOpsCLI.Models;

    public interface IReleaseApiClient
    {
        Task<Release> CreateAsync(string projectName, CreateReleaseRequest request);

        Task<IEnumerable<Release>> GetAllAsync(string projectName);

        Task<string> GetAsync(string projectName, int releaseId);

        Task<string> UpdateEnvironmentAsync(string projectName, int releaseId, int environmentId, EnvironmentStatus status, string comments);

        Task<string> GetEnvironmentAsync(string projectName, int releaseId, int environmentId);

        Task<GenericCollectionResponse<ReleaseApproval>> GetApprovalsAsync(string projectName, IEnumerable<int> releaseIds, ApprovalStatus status = ApprovalStatus.Undefined, ApprovalType approvalType = ApprovalType.Undefined);

        Task<ReleaseApproval> UpdateApprovalsAsync(string projectName, UpdateApprovalRequest request);
    }
}