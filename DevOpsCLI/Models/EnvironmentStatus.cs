// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Models
{
    using System;

    [Flags]
    public enum EnvironmentStatus
    {
        Undefined = 0,
        NotStarted = 1,
        InProgress = 2,
        Succeeded = 4,
        Canceled = 8,
        Rejected = 16,
        Queued = 32,
        Scheduled = 64,
        PartiallySucceeded = 128,
    }
}
