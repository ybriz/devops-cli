// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class GenericCollectionResponse<T>
        where T : class
    {
        public GenericCollectionResponse()
        {
            this.Values = new List<T>();
        }

        public int Count { get; set; }

        [JsonProperty("value")]
        public List<T> Values { get; set; }
    }
}