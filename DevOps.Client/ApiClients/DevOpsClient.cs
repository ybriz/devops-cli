// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client
{
    using System;

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

            this.Build = new BuildApiClient(connection);

            this.BuildDefinition = new BuildDefinitionApiClient(connection);

            this.Release = new ReleaseApiClient(connection);

            this.ReleaseDefinition = new ReleaseDefinitionApiClient(connection);

            this.VariableGroup = new VariableGroupApiClient(connection);
        }

        public IConnection Connection { get; }

        public IBuildApiClient Build { get; }

        public IBuildDefinitionApiClient BuildDefinition { get; }

        public IReleaseApiClient Release { get; }

        public IReleaseDefinitionApiClient ReleaseDefinition { get; }

        public IVariableGroupApiClient VariableGroup { get; }
    }
}