// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Models
{
    using System.Collections.Generic;
    using System.Linq;

    public class ReleaseListRequest
    {
        private IEnumerable<string> expandPropterties;

        public int ReleaseDefinitionId { get; set; }

        public int DefinitionEnvironmentId { get; set; }

        public EnvironmentStatus EnvironmentStatusFilter { get; set; }

        public int Top { get; set; }

        public IEnumerable<string> ExpandPropterties
        {
            get
            {
                if (this.expandPropterties == null)
                {
                    this.expandPropterties = Enumerable.Empty<string>();
                }

                return this.expandPropterties;
            }

            internal set => this.expandPropterties = value;
        }
    }
}