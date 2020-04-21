// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;

    public interface IRequest
    {
        object Body { get; set; }

        Dictionary<string, string> Headers { get; }

        HttpMethod Method { get; }

        Dictionary<string, string> Parameters { get; }

        Uri BaseAddress { get; }

        Uri Endpoint { get; }

        string ContentType { get; }

        HttpRequestMessage ToHttpMessage();
    }
}
