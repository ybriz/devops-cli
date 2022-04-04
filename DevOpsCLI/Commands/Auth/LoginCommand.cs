// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using System;
    using System.Threading.Tasks;
    using Jmelosegui.DevOps.Client;
    using Jmelosegui.DevOpsCLI.Services;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("login", Description = "Set the credential (PAT) to use for a particular organization")]
    public class LoginCommand
    {
        private readonly ICredentialStore credentialStore;
        private readonly ILogger<LoginCommand> logger;
        private readonly ApplicationConfiguration settings;

        public LoginCommand(
            ApplicationConfiguration settings,
            ICredentialStore credentialStore,
            ILogger<LoginCommand> logger)
        {
            this.settings = settings ?? throw new ArgumentNullException(nameof(settings));
            this.credentialStore = credentialStore ?? throw new ArgumentNullException(nameof(credentialStore));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [Option(
        "-u|--service-url",
        "URL to the service you will connect to, e.g. https://dev.azure.com/organization.",
        CommandOptionType.SingleValue)]
        public string ServiceUrl { get; set; }

        [Option(
        "--non-interactive",
        "Personal access token.",
        CommandOptionType.NoValue)]
        public bool NonInteractive { get; set; }

        public int OnExecute(CommandLineApplication app)
        {
            while (this.NonInteractive == false && string.IsNullOrEmpty(this.ServiceUrl))
            {
                this.ServiceUrl = Prompt.GetString("> ServiceURL:", null, ConsoleColor.DarkGray);
            }

            var token = Prompt.GetPassword("> Token:", null, ConsoleColor.DarkGray);

            ConnectionData connectionData = this.AssertCredentialsAsync(this.ServiceUrl, token).GetAwaiter().GetResult();

            if (connectionData == null)
            {
                throw new Exception("Token is invalid or expired");
            }

            this.credentialStore.SetCredential(this.ServiceUrl, token);

            if (this.settings.Defaults.Organization != this.ServiceUrl)
            {
                this.settings.Defaults.Project = null;
            }

            this.settings.Defaults.Organization = this.ServiceUrl;

            this.settings.Save();

            return ExitCodes.Ok;
        }

        private async Task<ConnectionData> AssertCredentialsAsync(string serviceUrl, string token)
        {
            if (string.IsNullOrEmpty(serviceUrl))
            {
                throw new ArgumentException("The service URL cannot be null or empty.", nameof(serviceUrl));
            }

            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException("The token cannot be null or empty.", nameof(token));
            }

            if (!Uri.TryCreate(serviceUrl, UriKind.Absolute, out var uri))
            {
                throw new ArgumentException("The service URL is not valid.", nameof(serviceUrl));
            }

            if (!uri.IsWellFormedOriginalString())
            {
                throw new ArgumentException("The service URL is not valid.", nameof(serviceUrl));
            }

            var devOpsClient = new DevOpsClient(new Uri(serviceUrl), new Credentials(string.Empty, token));

            return await devOpsClient.Identity.GetConnectionData();
        }
    }
}
