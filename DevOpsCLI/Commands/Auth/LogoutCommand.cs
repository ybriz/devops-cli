// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using System;
    using Jmelosegui.DevOpsCLI.Services;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("logout", Description = "Clear the credential for all or a particular organization")]
    public class LogoutCommand
    {
        private readonly ICredentialStore credentialStore;
        private readonly ILogger<LogoutCommand> logger;

        public LogoutCommand(
            ICredentialStore credentialStore,
            ILogger<LogoutCommand> logger)
        {
            this.credentialStore = credentialStore ?? throw new ArgumentNullException(nameof(credentialStore));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public int OnExecute(CommandLineApplication app)
        {
            this.credentialStore.ClearCredential();
            return ExitCodes.Ok;
        }
    }
}
