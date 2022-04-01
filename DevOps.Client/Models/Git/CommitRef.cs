// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client.Models
{
    /// <summary>
    /// Provides properties that describe a Git commit and associated metadata.
    /// </summary>
    public class CommitRef
    {
        /// <summary>
        /// Gets or sets iD(SHA-1) of the commit.
        /// </summary>
        public string CommitId { get; set; }

        /// <summary>
        /// Gets or sets author of the commit.
        /// </summary>
        public GitUserDate Author { get; set; }

        /// <summary>
        /// Gets or sets comment or message of the commit.
        /// </summary>
        public string Comment { get; set; }
    }
}
