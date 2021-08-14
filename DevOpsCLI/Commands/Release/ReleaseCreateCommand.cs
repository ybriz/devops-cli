// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using System;
    using System.Collections.Generic;
    using Jmelosegui.DevOps.Client;
    using Jmelosegui.DevOpsCLI.Model;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    [Command("create", Description = "Request (create) a release given a release definition.")]
    public class ReleaseCreateCommand : CommandBase
    {
        public ReleaseCreateCommand(ILogger<ReleaseCreateCommand> logger)
            : base(logger)
        {
        }

        [Option(
            "-rdid|--release-definition-id",
            "Release definition id",
            CommandOptionType.SingleValue)]
        public int ReleaseDefinitionId { get; set; }

        [Option(
            "-desc|--description",
            "Description",
            CommandOptionType.SingleValue)]
        public string Description { get; set; }

        [Option(
            "-r|--reason",
            "Reason",
            CommandOptionType.SingleValue)]
        public string Reason { get; set; }

        [Option(
            "--is-draft",
            "IsDraft",
            CommandOptionType.NoValue)]
        public bool IsDraft { get; set; }

        [Option(
            "-arts|--artifacts",
            "Json string that represent the list of ArtifactsMetadata that will be use in the release(i.e [{'Name': 'artifact-name', 'Alias': 'artifact-alias', 'Id': 'artifact-id'}])",
            CommandOptionType.SingleValue)]
        public string Artifacts { get; private set; }

        [Option(
            "-e|--environment",
            "Manual Environment",
            CommandOptionType.MultipleValue)]
        public IEnumerable<string> ManualEnvironments { get; private set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            base.OnExecute(app);

            while (this.ReleaseDefinitionId <= 0)
            {
                int.TryParse(Prompt.GetString("> ReleaseDefinitionId:", null, ConsoleColor.DarkGray), out int releaseDefinitionId);
                this.ReleaseDefinitionId = releaseDefinitionId;
            }

            CreateReleaseRequest request = new CreateReleaseRequest
            {
                DefinitionId = this.ReleaseDefinitionId,
                Description = this.Description,
                IsDraft = this.IsDraft,
                ManualEnvironments = this.ManualEnvironments,
                Reason = this.Reason,
            };

            if (!string.IsNullOrEmpty(this.Artifacts))
            {
                try
                {
                    JsonSerializerSettings settings = new JsonSerializerSettings();
                    settings.MissingMemberHandling = MissingMemberHandling.Error;

                    List<ArtifactDTO> artifactsMetadata = JsonConvert.DeserializeObject<List<ArtifactDTO>>(this.Artifacts, settings);

                    List<ReleaseArtifactMetadata> releaseArtifactMetadataList = new List<ReleaseArtifactMetadata>();
                    foreach (var artifact in artifactsMetadata)
                    {
                        releaseArtifactMetadataList.Add(
                            new ReleaseArtifactMetadata
                            {
                                Alias = artifact.Alias,
                                InstanceReference = new BuildVersion
                                {
                                    Id = artifact.Id,
                                    Name = artifact.Name,
                                },
                            });
                    }

                    request.Artifacts = releaseArtifactMetadataList;
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine("There was an error parsing the parameter 'artifacts'. Error: {0}", ex.Message);
                    return ExitCodes.UnknownError;
                }
            }

            var result = this.DevOpsClient.Release.CreateAsync(this.ProjectName, request).GetAwaiter().GetResult();

            Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));

            return ExitCodes.Ok;
        }
    }
}
