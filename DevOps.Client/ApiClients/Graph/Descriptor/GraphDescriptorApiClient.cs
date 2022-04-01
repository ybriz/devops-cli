// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public partial class GraphApiClient
    {
        private const string DescriptorEndPoint = "_apis/graph/descriptors";

        public async Task<GraphDescriptor> DescriptorGetAsync(string storageKey)
        {
            var parameters = new Dictionary<string, object>();

            FluentDictionary.For(parameters)
                            .Add("api-version", "6.0-preview.1");

            var response = await this.Connection.Get<GraphDescriptor>(new Uri($"{DescriptorEndPoint}/{storageKey}", UriKind.Relative), parameters, null, CancellationToken.None, this.BaseUrl)
                                                .ConfigureAwait(false);

            return response.Body;
        }
    }
}
