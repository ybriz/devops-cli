// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client
{
    public class ConnectionData
    {
        public AuthenticatedUser AuthenticatedUser { get; set; }

        public AuthorizedUser AuthorizedUser { get; set; }

        public string InstanceId { get; set; }

        public string DeploymentId { get; set; }

        public string DeploymentType { get; set; }

        public LocationServiceData LocationServiceData { get; set; }
    }
}
