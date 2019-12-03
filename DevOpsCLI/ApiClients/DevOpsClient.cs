// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.ApiClients
{
    using System;
    using Jmelosegui.DevOpsCLI.Helpers;
    using Jmelosegui.DevOpsCLI.Http;

    public class DevOpsClient
    {
        public DevOpsClient(Uri collectionUri)
            : this(collectionUri, Credentials.Anonymous)
        {
        }

        public DevOpsClient(Uri serviceUrl, Credentials credentials)
            : this(new Connection(serviceUrl, credentials))
        {
        }

        public DevOpsClient(IConnection connection)
        {
            Ensure.ArgumentNotNull(connection, nameof(connection));

            this.Connection = connection;

            this.BuildDefinition = new BuildDefinitionApiClient(connection);
            this.VariableGroup = new VariableGroupApiClient(connection);
        }

        public IConnection Connection { get; }

        public IBuildDefinitionApiClient BuildDefinition { get; }

        public IVariableGroupApiClient VariableGroup { get; }
    }
}