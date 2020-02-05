// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Models
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public class ReleaseApproval
    {
        public int Id { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ApprovalStatus Status { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ApprovalType Type { get; set; }

        public Release Release { get; set; }

        public ReleaseDefinition ReleaseDefinition { get; set; }

        public ReleaseEnvironment ReleaseEnvironment { get; set; }

        public int Attempt { get; set; }

        public IdentityRef Approver { get; set; }
    }
}
