// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI
{
    using System;
    using Jmelosegui.DevOpsCLI.Commands;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    internal class Program
    {
        private static int Main(string[] args)
        {
            var servicesProvider = new ServiceCollection()
                .AddLogging(configure =>
                {
                    configure.AddConsole();
                    configure.SetMinimumLevel(LogLevel.Debug);
                })
                .BuildServiceProvider();

            var logger = servicesProvider.GetRequiredService<ILogger<Program>>();

            try
            {
                var app = new CommandLineApplication<CommandRunner>() { ThrowOnUnexpectedArgument = false };
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
