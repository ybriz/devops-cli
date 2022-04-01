// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands.Graph.Groups
{
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("export", Description = "Get a group by its descriptor.")]
    public sealed class GroupExportCommand : CommandBase
    {
        public GroupExportCommand(ILogger<GroupExportCommand> logger)
            : base(logger)
        {
        }

    }
}
