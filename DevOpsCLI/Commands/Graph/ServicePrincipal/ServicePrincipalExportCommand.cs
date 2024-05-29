// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands.Graph.ServicePrincipal
{
    using System;
    using System.Linq;
    using Jmelosegui.DevOps.Client;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("export", Description = "Get a service principal by its descriptor.")]
    public sealed class ServicePrincipalExportCommand : CommandBase
    {
        public ServicePrincipalExportCommand(ApplicationConfiguration settings, ILogger<ServicePrincipalExportCommand> logger)
            : base(settings, logger)
        {
        }

        [Option(
          "--service-principal-descriptor",
          "The descriptor of the desired graph service principal.",
          CommandOptionType.SingleValue)]
        public string ServicePrincipalDescriptor { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            base.OnExecute(app);

            GraphServicePrincipal graphServicePrincipal = null;

            if (!string.IsNullOrEmpty(this.ServicePrincipalDescriptor))
            {
                graphServicePrincipal = this.DevOpsClient.Graph.ServicePrincipalGetAsync(this.ServicePrincipalDescriptor).GetAwaiter().GetResult();
            }

            if (graphServicePrincipal == null)
            {
                throw new Exception("Service Principal not found with the provided parameters");
            }

            this.PrintOrExport(graphServicePrincipal);

            return ExitCodes.Ok;
        }
    }
}
