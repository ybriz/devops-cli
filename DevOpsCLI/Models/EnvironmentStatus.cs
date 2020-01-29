// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Models
{
    public enum EnvironmentStatus
    {
        Undefined = 0,
        Queued,
        Scheduled,
        NotStarted,
        InProgress,
        Canceled,
        Succeeded,
        PartiallySucceeded,
        Rejected,
    }
}
