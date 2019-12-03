// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Exceptions
{
    using System;
    using System.Net;
    using System.Text.RegularExpressions;
    using Jmelosegui.DevOpsCLI.Http;
    using Newtonsoft.Json;

    [Serializable]
    public class ApiException : Exception
    {
        public ApiException(IResponse response)
            : this(response, null)
        {
        }

        public ApiException(IResponse response, Exception innerException)
            : base(null, innerException)
        {
            this.StatusCode = response.StatusCode;
            this.ApiError = GetApiErrorFromExceptionMessage(response);
            this.HttpResponse = response;
        }

        /// <summary>
        /// Gets the HTTP status code associated with the repsonse.
        /// </summary>
        public HttpStatusCode StatusCode { get; private set; }

        public IResponse HttpResponse { get; private set; }

        public override string Message
        {
            get { return this.ApiErrorMessageSafe ?? "An error occurred with this API request"; }
        }

        /// <summary>
        /// Gets the raw exception payload from the response.
        /// </summary>
        public ApiError ApiError { get; private set; }

        /// <summary>
        /// Gets get the inner error message from the API response.
        /// </summary>
        /// <remarks>
        /// Returns null if ApiError is not populated.
        /// </remarks>
        protected string ApiErrorMessageSafe
        {
            get
            {
                if (this.ApiError != null && !string.IsNullOrWhiteSpace(this.ApiError.Message))
                {
                    return this.ApiError.Message;
                }

                return null;
            }
        }

        private static ApiError GetApiErrorFromExceptionMessage(IResponse response)
        {
            string responseBody = response != null ? response.Body as string : null;
            try
            {
                if (!string.IsNullOrEmpty(responseBody))
                {
                    return JsonConvert.DeserializeObject<ApiError>(responseBody);
                }
            }
            catch (Exception)
            {
                string title = Regex.Match(responseBody, @"\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>", RegexOptions.IgnoreCase).Groups["Title"].Value;
                return new ApiError(title);
            }

            return new ApiError(responseBody);
        }
    }
}