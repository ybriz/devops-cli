// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client.Models
{
    using System;

    /// <summary>
    /// User info and date for Git operations.
    /// </summary>
    public class GitUserDate
    {
        /// <summary>
        /// Gets or sets name of the user performing the Git operation.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets email address of the user performing the Git operation.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets date of the Git operation.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets url for the user's avatar.
        /// </summary>
        public string ImageUrl { get; set; }
    }
}
