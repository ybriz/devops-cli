// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using Jmelosegui.DevOpsCLI.Commands.Graph.GraphServicePrincipal;
    using Jmelosegui.DevOpsCLI.Commands.Graph.ServicePrincipal;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Subcommand(typeof(ServicePrincipalExportCommand))]
    [Subcommand(typeof(GraphServicePrincipalListCommand))]
    [Command("service-principal", Description = "Commands for managing Service Principals.")]
    public class GraphServicePrincipalCommand : CommandBase
    {
        public GraphServicePrincipalCommand(ApplicationConfiguration settings, ILogger<GraphServicePrincipalCommand> logger)
            : base(settings, logger)
        {
        }
    }
}
