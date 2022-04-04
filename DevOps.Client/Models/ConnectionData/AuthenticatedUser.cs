// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client
{
    public class AuthenticatedUser
    {
        public string Id { get; set; }

        public string Descriptor { get; set; }

        public string SubjectDescriptor { get; set; }

        public string ProviderDisplayName { get; set; }

        public string CustomDisplayName { get; set; }

        public bool IsActive { get; set; }

        public int ResourceVersion { get; set; }

        public int MetaTypeId { get; set; }
    }
}
