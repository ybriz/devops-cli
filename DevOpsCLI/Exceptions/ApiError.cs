// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Exceptions
{
    using System.Collections.Generic;

    public class ApiError
    {
        public ApiError(string message)
        {
            this.Message = message;
        }

        public int Code { get; set; }

        public string Message { get; set; }

        public IReadOnlyList<string> Errors { get; set; }
    }
}
