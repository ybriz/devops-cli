// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Services
{
    public interface ICredentialStore
    {
        void ClearCredential();

        string GetCredential(string username);

        void SetCredential(string username, string password);
    }
}
