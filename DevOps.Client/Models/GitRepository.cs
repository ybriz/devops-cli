// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Provides properties that describe a GitRepository commit and associated metadata.
    /// </summary>
    public class GitRepository
    {
        /// <summary>
        /// Gets or sets the Id of the repository.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the Name of the repository.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets a value indicating whether the repository was created as a fork;
        /// True if the repository was created as a fork.
        /// </summary>
        public bool IsFork { get; set; }

        /// <summary>
        /// Gets or sets the name of the default branch in the repository.
        /// </summary>
        public string DefaultBranch { get; set; }

        /// <summary>
        /// Gets or sets the Url of the repository.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets a collection of valid remote Urls for the repository.
        /// </summary>
        public IEnumerable<string> ValidReponUrls { get; set; }
    }
}
