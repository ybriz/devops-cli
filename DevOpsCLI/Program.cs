// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI
{
    using System;
    using Jmelosegui.DevOpsCLI.Commands;
    using Jmelosegui.DevOpsCLI.Services;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    internal class Program
    {
        private static int Main(string[] args)
        {
            var settings = new ApplicationConfiguration();
            var configurationFile = settings.GetConfigurationFile();
            var builder = new ConfigurationBuilder()
                .AddJsonFile(configurationFile, optional: true, reloadOnChange: true);

            var configuration = builder.Build();

            configuration.Bind(settings);

            var servicesProvider = new ServiceCollection()
                .AddSingleton<ICredentialStore, ProtectedDataCredentialStore>()
                .AddSingleton(settings)
                .AddLogging(configure =>
                {
                    configure.AddConsole();
                    configure.SetMinimumLevel(LogLevel.Debug);
                })
                            .BuildServiceProvider();

            var logger = servicesProvider.GetRequiredService<ILogger<Program>>();

            try
            {
                var app = new CommandLineApplication<CommandRunner>();
                app.Conventions
                   .UseDefaultConventions()
                   .UseConstructorInjection(servicesProvider);

                return app.Execute(args);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return ExitCodes.UnknownError;
            }
        }
    }
}
