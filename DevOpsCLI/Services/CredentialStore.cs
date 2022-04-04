// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Services
{
    using Windows.Security.Credentials;

    internal class CredentialStore : ICredentialStore
    {
        public const string DefaultKey = "Jmelosegui.DevOpsCLI.Credentials";
        private readonly PasswordVault vault;

        public CredentialStore()
        {
            this.vault = new PasswordVault();
        }

        public void ClearCredential()
        {
            try
            {
                foreach (var item in this.vault.FindAllByResource(DefaultKey))
                {
                    this.vault.Remove(item);
                }
            }
            catch (System.Exception ex)
            {
                if (!ex.Message.Contains("Element not found"))
                {
                    throw;
                }

                // ignore
            }
        }

        public string GetCredential(string username)
        {
            string result = null;

            try
            {
                result = this.vault.Retrieve(DefaultKey, username).Password;
            }
            catch (System.Exception ex)
            {
                if (!ex.Message.Contains("Element not found"))
                {
                    throw;
                }

                // ignore
            }

            return result;
        }

        public void SetCredential(string username, string password)
        {
            this.vault.Add(new PasswordCredential(DefaultKey, username, password));
        }
    }
}
