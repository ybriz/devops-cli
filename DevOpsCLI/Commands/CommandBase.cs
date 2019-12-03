// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI
{
    using System;
    using Jmelosegui.DevOpsCLI.ApiClients;
    using Jmelosegui.DevOpsCLI.Http;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [HelpOption("-h| --help")]
    public abstract class CommandBase
    {
        protected CommandBase(ILogger<CommandBase> logger)
        {
            this.Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [Option(
            "-u|--service-url",
            "URL to the service you will connect to, e.g. https://youraccount.visualstudio.com/DefaultCollection.",
            CommandOptionType.SingleValue)]
        public string ServiceUrl { get; set; }

        [Option(
            "-t|--token",
            "Personal access token.",
            CommandOptionType.SingleValue)]
        public string Token { get; set; }

        protected ILogger Logger { get; }

        protected DevOpsClient DevOpsClient { get; private set; }

        protected virtual int OnExecute(CommandLineApplication app)
        {
            while (string.IsNullOrEmpty(this.ServiceUrl))
            {
                this.ServiceUrl = Prompt.GetString("> ServiceURL:", null, ConsoleColor.DarkGray);
            }

            while (string.IsNullOrEmpty(this.Token))
            {
                this.Token = Prompt.GetString("> Token:", null, ConsoleColor.DarkGray);
            }

            this.DevOpsClient = new DevOpsClient(new Uri(this.ServiceUrl), new Credentials(string.Empty, this.Token));

            return ExitCodes.Ok;
        }
    }
}