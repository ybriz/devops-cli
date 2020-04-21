// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client
{
    using System.Collections.Generic;
    using System.Linq;

    public class ReleaseListRequest
    {
        private List<string> expandPropterties;

        public ReleaseListRequest()
            : this(null)
        {
        }

        public ReleaseListRequest(IEnumerable<string> expandPropterties)
        {
            if (expandPropterties == null)
            {
                this.expandPropterties = new List<string>();
            }
            else
            {
                this.expandPropterties = new List<string>(expandPropterties);
            }
        }

        public int ReleaseDefinitionId { get; set; }

        public int DefinitionEnvironmentId { get; set; }

        public EnvironmentStatus EnvironmentStatusFilter { get; set; }

        public int Top { get; set; }

        public IEnumerable<string> ExpandPropterties
        {
            get
            {
                return this.expandPropterties;
            }
        }
    }
}