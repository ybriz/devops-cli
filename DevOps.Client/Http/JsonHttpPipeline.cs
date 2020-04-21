// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Net.Http;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    ///     Responsible for serializing the request and response as JSON and
    ///     adding the proper JSON response header.
    /// </summary>
    public class JsonHttpPipeline
    {
        private readonly JsonSerializerSettings settings;

        public JsonHttpPipeline()
            : this(new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() })
        {
        }

        public JsonHttpPipeline(JsonSerializerSettings settings)
        {
            this.settings = settings;
        }

        public void SerializeRequest(IRequest request)
        {
            Ensure.ArgumentNotNull(request, "request");

            if (request.Method == HttpMethod.Get || request.Body == null)
            {
                return;
            }

            if (request.Body is string || request.Body is Stream || request.Body is HttpContent)
            {
                return;
            }

            request.Body = JsonConvert.SerializeObject(request.Body, this.settings);
        }

        public IApiResponse<T> DeserializeResponse<T>(IResponse response)
        {
            Ensure.ArgumentNotNull(response, "response");

            if (response.ContentType != null && response.ContentType.Equals("application/json", StringComparison.Ordinal))
            {
                var body = response.Body as string;

                if (typeof(T) != typeof(string) && !string.IsNullOrEmpty(body) && body != "{}")
                {
                    var typeIsDictionary = typeof(IDictionary).IsAssignableFrom(typeof(T));
                    var typeIsEnumerable = typeof(IEnumerable).IsAssignableFrom(typeof(T));
                    var responseIsObject = body.StartsWith("{", StringComparison.Ordinal);

                    // If we're expecting an array, but we get a single object, just wrap it.
                    // This supports an api that dynamically changes the return type based on the content.
                    if (!typeIsDictionary && typeIsEnumerable && responseIsObject)
                    {
                        body = "[" + body + "]";
                    }

                    var json = JsonConvert.DeserializeObject<T>(body, this.settings);
                    return new ApiResponse<T>(response, json);
                }
            }

            return new ApiResponse<T>(response);
        }
    }
}
