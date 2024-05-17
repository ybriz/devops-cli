// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client
{
    public sealed class GraphMembershipListRequest
    {
        /// <summary>
        /// Gets or sets the ScopeDecriptor.
        /// Specify a non-default scope (collection, project) to search for groups.
        /// </summary>
        public string SubjectDescriptor { get; set; }

        public string Direction { get; set; }
    }
}
