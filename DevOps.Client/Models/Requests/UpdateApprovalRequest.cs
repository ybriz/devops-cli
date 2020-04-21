// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public class UpdateApprovalRequest
    {
        public int Id { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ApprovalStatus Status { get; set; }

        public string Comments { get; set; }
    }
}
