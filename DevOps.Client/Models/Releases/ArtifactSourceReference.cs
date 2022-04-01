// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client
{
    public class ArtifactSourceReference
    {
        public IdName Definition { get; set; }

        public IdName Version { get; set; }

        public IdName RequestedFor { get; set; }

        public IdName Project { get; set; }

        public IdName Branch { get; set; }

        public IdName SourceVersion { get; set; }
    }
}
