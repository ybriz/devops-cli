// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client
{
    public class Identity
    {
        public string Id { get; set; }

        public int UniqueUserId { get; set; }

        public int ResourceVersion { get; set; }

        public string CustomDisplayName { get; set; }

        public string SocialDescriptor { get; set; }

        public string SubjectDescriptor { get; set; }

        public bool IsActive { get; set; }

        public bool IsContainer { get; set; }
    }
}
