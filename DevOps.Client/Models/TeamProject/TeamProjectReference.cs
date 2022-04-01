// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client
{
    public class TeamProjectReference
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Abbreviation { get; set; }

        public string Description { get; set; }

        public int Revision { get; set; }

        public string Url { get; set; }

        public ProjectState State { get; set; }

        public ProjectVisibility Visibility { get; set; }
    }
}
