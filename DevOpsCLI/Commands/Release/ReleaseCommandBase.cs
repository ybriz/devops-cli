// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI
{
    using Microsoft.Extensions.Logging;

    public abstract class ReleaseCommandBase : CommandBase
    {
        protected ReleaseCommandBase(ILogger<ReleaseCommandBase> logger)
            : base(logger)
        {
        }

        protected override string HostPrefix => "vsrm.";
    }
}
