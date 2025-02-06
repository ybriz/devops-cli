// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client.Models
{
    public sealed class GitRepository
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public bool IsFork { get; set; }

        public long Size { get; set; }

        public string Url { get; set; }

        public string WebUrl { get; set; }

        public string RemoteUrl { get; set; }

        public string SshUrl { get; set; }

        public string DefaultBranch { get; set; }
    }
}
