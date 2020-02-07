// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.ApiClients
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Jmelosegui.DevOpsCLI.Helpers;
    using Jmelosegui.DevOpsCLI.Http;
    using Jmelosegui.DevOpsCLI.Models;

    public sealed class ReleaseApiClient : IReleaseApiClient
    {
        private const string EndPoint = "_apis/release/releases";

        public ReleaseApiClient(IConnection connection)
        {
            this.Connection = connection;
        }

        /// <summary>
        /// Gets the underlying connection.
        /// </summary>
        public IConnection Connection { get; private set; }

        public async Task<Release> CreateAsync(string projectName, CreateReleaseRequest request)
        {
            var parameters = new Dictionary<string, object>();

            FluentDictionary.For(parameters)
                            .Add("api-version", "5.0");

            IApiResponse<Release> result = await this.Connection
                                                     .Post<Release>(new Uri($"{projectName}/{EndPoint}", UriKind.Relative), request, parameters, null)
                                                     .ConfigureAwait(false);

            return result.Body;
        }

        public async Task<IEnumerable<Release>> GetAllAsync(string projectName, ReleaseListRequest releaseListRequest = null)
        {
            var parameters = new Dictionary<string, object>
            {
                { "api-version", "5.0" },
            };

            if (releaseListRequest != null)
            {
                if (releaseListRequest.ReleaseDefinitionId > 0)
                {
                    parameters["definitionId"] = releaseListRequest.ReleaseDefinitionId;
                }

                if (releaseListRequest.Top > 0)
                {
                    parameters["top"] = releaseListRequest.Top;
                }
            }

            var response = await this.Connection.Get<GenericCollectionResponse<Release>>(new Uri($"{projectName}/{EndPoint}", UriKind.Relative), parameters, null)
                                                .ConfigureAwait(false);

            return response.Body.Values;
        }

        public async Task<string> GetAsync(string projectName, int releaseId)
        {
            var parameters = new Dictionary<string, object>
            {
                { "api-version", "5.0" },
            };

            var endPointUrl = new Uri($"{projectName}/{EndPoint}/{releaseId}", UriKind.Relative);

            var response = await this.Connection
                         .Get<string>(endPointUrl, parameters, null)
                         .ConfigureAwait(false);

            return response.Body;
        }

        public async Task<string> UpdateEnvironmentAsync(string projectName, int releaseId, int environmentId, EnvironmentStatus status, string comments)
        {
            var parameters = new Dictionary<string, object>
            {
                { "api-version", "5.0-preview.6" },
            };

            var endPointUrl = new Uri($"{projectName}/{EndPoint}/{releaseId}/environments/{environmentId}", UriKind.Relative);

            var body = new UpdateEnvironmentRequest
            {
                Status = status,
                Comments = comments,
            };

            var response = await this.Connection
                      .Patch<string>(endPointUrl, body, parameters, null)
                      .ConfigureAwait(false);

            return response.Body;
        }

        public async Task<string> GetEnvironmentAsync(string projectName, int releaseId, int environmentId)
        {
            var parameters = new Dictionary<string, object>
            {
                { "api-version", "5.1-preview.6" },
            };

            var endPointUrl = new Uri($"{projectName}/{EndPoint}/{releaseId}/environments/{environmentId}", UriKind.Relative);

            var response = await this.Connection
                      .Get<string>(endPointUrl, parameters, null)
                      .ConfigureAwait(false);

            return response.Body;
        }

        public async Task<GenericCollectionResponse<ReleaseApproval>> GetApprovalsAsync(string projectName, IEnumerable<int> releaseIds, ApprovalStatus status = ApprovalStatus.Undefined, ApprovalType approvalType = ApprovalType.Undefined)
        {
            var parameters = new Dictionary<string, object>
            {
                { "api-version", "5.0" },
            };

            if (releaseIds?.Count() > 0)
            {
                parameters["releaseIdsFilter"] = string.Join(',', releaseIds);
            }

            if (status != ApprovalStatus.Undefined)
            {
                parameters["statusFilter"] = status;
            }

            if (approvalType != ApprovalType.Undefined)
            {
                parameters["typeFilter"] = approvalType;
            }

            var endPointUrl = new Uri($"{projectName}/_apis/release/approvals", UriKind.Relative);

            var response = await this.Connection.Get<GenericCollectionResponse<ReleaseApproval>>(endPointUrl, parameters, null)
                                    .ConfigureAwait(false);

            return response.Body;
        }

        public async Task<ReleaseApproval> UpdateApprovalsAsync(string projectName, UpdateApprovalRequest request)
        {
            var parameters = new Dictionary<string, object>
            {
                { "api-version", "5.0" },
            };

            var endPointUrl = new Uri($"{projectName}/_apis/release/approvals/{request.Id}", UriKind.Relative);

            var body = new UpdateApprovalRequest
            {
                Status = request.Status,
                Comments = request.Comments,
            };

            var response = await this.Connection
                      .Patch<ReleaseApproval>(endPointUrl, body, parameters, null)
                      .ConfigureAwait(false);

            return response.Body;
        }
    }
}