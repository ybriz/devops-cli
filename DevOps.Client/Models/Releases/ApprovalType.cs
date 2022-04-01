// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client
{
    using System;

    [Flags]
    public enum ApprovalType
    {
        Undefined = 0,
        Predeploy = 1,
        PostDeploy = 2,
        All = Predeploy | PostDeploy,
    }
}
