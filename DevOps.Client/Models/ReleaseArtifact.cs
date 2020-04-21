// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client
{
    public class ReleaseArtifact
    {
        public string SourceId { get; set; }

        public string Type { get; set; }

        public bool IsPrimary { get; set; }

        public bool IsRetained { get; set; }

        public ArtifactSourceReference DefinitionReference { get; set; }
    }
}
