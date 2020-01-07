// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Extensions for working with Uris.
    /// </summary>
    public static class UriExtensions
    {
        /// <summary>
        /// Merge a dictionary of values with an existing <see cref="Uri"/>.
        /// </summary>
        /// <param name="uri">Original request Uri.</param>
        /// <param name="parameters">Collection of key-value pairs.</param>
        /// <returns>Updated request Uri.</returns>
        public static Uri ApplyParameters(this Uri uri, IDictionary<string, object> parameters)
        {
            Ensure.ArgumentNotNull(uri, "uri");

            if (parameters == null || !parameters.Any())
            {
                return uri;
            }

            // to prevent values being persisted across requests
            // use a temporary dictionary which combines new and existing parameters
            IDictionary<string, object> p = new Dictionary<string, object>(parameters);

            string queryString;
            if (uri.IsAbsoluteUri)
            {
                queryString = uri.Query;
            }
            else
            {
                var hasQueryString = uri.OriginalString.IndexOf("?", StringComparison.Ordinal);
                queryString = hasQueryString == -1
                    ? string.Empty
                    : uri.OriginalString.Substring(hasQueryString);
            }

            var values = queryString.Replace("?", string.Empty)
                                    .Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries);

            var existingParameters = values.ToDictionary(
                        key => key.Substring(0, key.IndexOf('=')),
                        value => value.Substring(value.IndexOf('=') + 1));

            foreach (var existing in existingParameters)
            {
                if (!p.ContainsKey(existing.Key))
                {
                    p.Add(existing.Key, existing.Value);
                }
            }

            Func<string, string, string> mapValueFunc = (key, value) => key == "q" ? value : Uri.EscapeDataString(value);

            string query = string.Join("&", p.Select(kvp => kvp.Key + "=" + mapValueFunc(kvp.Key, GetFormattedValue(kvp.Value))));
            if (uri.IsAbsoluteUri)
            {
                var uriBuilder = new UriBuilder(uri)
                {
                    Query = query,
                };
                return uriBuilder.Uri;
            }

            return new Uri(uri + "?" + query, UriKind.Relative);
        }

        private static string GetFormattedValue(object value)
        {
            string result;

            switch (value)
            {
                case null:
                    result = null;
                    break;
                case DateTime d:
                    result = d.ToString("yyyy-MM-ddTHH:mm:ssZ");
                    break;
                default:
                    result = value.ToString();
                    break;
            }

            return result;
        }
    }
}