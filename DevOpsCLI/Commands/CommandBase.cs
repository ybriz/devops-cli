// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Jmelosegui.DevOps.Client;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    [HelpOption("-h| --help")]
    public abstract class CommandBase
    {
        protected CommandBase(ILogger<CommandBase> logger)
        {
            this.Logger = logger ?? throw new ArgumentNullException(nameof(logger));
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
            "-p|--project",
            "Tfs project name",
            CommandOptionType.SingleValue)]
        public string ProjectName { get; set; }

        public bool IsCommandGroup
        {
            get
            {
                return this.GetType()
                           .GetCustomAttributes<SubcommandAttribute>()
                           .Any();
            }
        }

        /// <summary>
        /// Gets the HostPrefix value.
        /// </summary>
        /// <remarks>
        /// There are some endpoints that add a prefix to the default azure devops domain name.
        /// For instance the end point url to manage release is "vsrm.dev.azure.com" instead of dev.azure.com.
        /// On those scenarios, use this property to add that extra prefix to the default hostname.
        /// </remarks>
        protected virtual string HostPrefix => string.Empty;

        protected ILogger Logger { get; }

        protected DevOpsClient DevOpsClient { get; private set; }

        protected virtual int OnExecute(CommandLineApplication app)
        {
            if (this.IsCommandGroup)
            {
                app.ShowHelp(true);
            }
            else
            {
                while (string.IsNullOrEmpty(this.ServiceUrl))
                {
                    this.ServiceUrl = Prompt.GetString("> ServiceURL:", null, ConsoleColor.DarkGray);
                }

                while (string.IsNullOrEmpty(this.Token))
                {
                    this.Token = Prompt.GetPassword("> Token:", null, ConsoleColor.DarkGray);
                }

                while (string.IsNullOrEmpty(this.ProjectName))
                {
                    this.ProjectName = Prompt.GetString("> ProjectName:", null, ConsoleColor.DarkGray);
                }

                var serviceUri = this.GetServiceUri(this.ServiceUrl);

                this.DevOpsClient = new DevOpsClient(serviceUri, new Credentials(string.Empty, this.Token));
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

        private Uri GetServiceUri(string serviceUrl)
        {
            var uri = new Uri(serviceUrl);

            if (uri.Host == "dev.azure.com")
            {
                return new Uri($"{uri.Scheme}://{this.HostPrefix}dev.azure.com{uri.AbsolutePath}");
            }

            return uri;
        }
    }
}