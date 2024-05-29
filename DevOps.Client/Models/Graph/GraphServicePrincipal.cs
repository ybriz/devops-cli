// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client
{
    public sealed class GraphServicePrincipal
    {
        public string Descriptor { get; set; }

        public string ApplicationId { get; set; }

        public string DisplayName { get; set; }

        public string Domain { get; set; }

        public string Origin { get; set; }

        public string OriginId { get; set; }

        public string PrincipalName { get; set; }

        public string SubjectKind { get; set; }

        public string Url { get; set; }

        public string MailAddress { get; set; }
    }
}
