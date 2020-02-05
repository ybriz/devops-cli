// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using System;
    using System.Collections.Generic;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;

    [Command("list", Description = "Get a list of release definitions.")]
    public class ReleaseDefinitionListCommand : CommandBase
    {
        public ReleaseDefinitionListCommand(ILogger<ReleaseDefinitionListCommand> logger)
            : base(logger)
        {
        }

        protected override int OnExecute(CommandLineApplication app)
        {
            base.OnExecute(app);

            IEnumerable<Models.ReleaseDefinition> releaseDefinitions = this.DevOpsClient.ReleaseDefinition.GetAllAsync(this.ProjectName).Result;

            Console.WriteLine();

            Console.WriteLine(JsonConvert.SerializeObject(releaseDefinitions, Formatting.Indented));

            return ExitCodes.Ok;
        }
    }
}
