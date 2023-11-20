// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Jmelosegui.DevOps.Client;
    using Jmelosegui.DevOpsCLI.Services;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    [HelpOption("-h| --help")]
    public abstract class CommandBase
    {
        protected CommandBase(ApplicationConfiguration settings, ILogger<CommandBase> logger)
        {
            this.Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.Settings = settings ?? throw new ArgumentNullException(nameof(logger));
        }

        [Option(
            "-u|--service-url",
            "URL to the service you will connect to, e.g. https://dev.azure.com/organization.",
            CommandOptionType.SingleValue)]
        public string ServiceUrl { get; set; }

        [Option(
            "-t|--token",
            "Personal access token.",
            CommandOptionType.SingleValue)]
        public string Token { get; set; }

        [Option(
        "--non-interactive",
        "When set, the tool will run in non interactive mode.",
        CommandOptionType.NoValue)]
        public bool NonInteractive { get; set; }

        public bool IsCommandGroup
        {
            get
            {
                return this.GetType()
                           .GetCustomAttributes<SubcommandAttribute>()
                           .Any();
            }
        }

        protected ILogger Logger { get; }

        protected ApplicationConfiguration Settings { get; }

        protected DevOpsClient DevOpsClient { get; private set; }

        protected virtual int OnExecute(CommandLineApplication app)
        {
            if (this.IsCommandGroup)
            {
                app.ShowHelp(true);
            }
            else
            {
                if (string.IsNullOrEmpty(this.ServiceUrl) && !string.IsNullOrEmpty(this.Settings?.Defaults?.Organization))
                {
                    this.ServiceUrl = this.Settings.Defaults.Organization;
                }

                while (this.NonInteractive == false && string.IsNullOrEmpty(this.ServiceUrl))
                {
                    this.ServiceUrl = Prompt.GetString("> ServiceURL:", null, ConsoleColor.DarkGray);
                }

                string storedToken = null;
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                {
                    var credentialStore = new ProtectedDataCredentialStore();
                    storedToken = credentialStore.GetCredential(this.ServiceUrl);
                }

                if (string.IsNullOrEmpty(this.Token) && !string.IsNullOrEmpty(storedToken))
                {
                    this.Token = storedToken;
                }

                while (this.NonInteractive == false && string.IsNullOrEmpty(this.Token))
                {
                    this.Token = Prompt.GetPassword("> Token:", null, ConsoleColor.DarkGray);
                }

                if (string.IsNullOrEmpty(this.Token))
                {
                    throw new InvalidOperationException("An Azure DevOps token must be provided. You can run the login command to avoid passing the credential every time.");
                }

                this.DevOpsClient = new DevOpsClient(new Uri(this.ServiceUrl), new Credentials(string.Empty, this.Token));
            }

            return ExitCodes.Ok;
        }

        protected virtual void PrintOrExport<T>(T content, string outputFile = null)
        {
            string outPutContent;

            if (typeof(T) == typeof(string))
            {
                outPutContent = content as string;
            }
            else
            {
                var settings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                };

                outPutContent = JsonConvert.SerializeObject(content, settings);
            }

            if (string.IsNullOrEmpty(outputFile))
            {
                Console.Write(outPutContent);
            }
            else
            {
                string outputDirectory = Path.GetDirectoryName(outputFile);

                if (!string.IsNullOrEmpty(outputDirectory) && !Directory.Exists(outputDirectory))
                {
                    Directory.CreateDirectory(outputDirectory);
                }

                File.WriteAllText(outputFile, outPutContent);
            }
        }
    }
}