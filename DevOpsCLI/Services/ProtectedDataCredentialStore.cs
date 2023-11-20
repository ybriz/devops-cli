// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Services
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;
    using Newtonsoft.Json;

    internal class ProtectedDataCredentialStore : ICredentialStore
    {
        private readonly string tokenFilePath;

        public ProtectedDataCredentialStore()
        {
            this.tokenFilePath = this.GetTokenFile();
        }

        public void ClearCredential()
        {
            if (File.Exists(this.tokenFilePath))
            {
                File.Delete(this.tokenFilePath);
            }
        }

        public string GetCredential(string username)
        {
            string result = null;

            try
            {
                if (File.Exists(this.tokenFilePath))
                {
                    var protectedContentBytesBase64 = File.ReadAllText(this.tokenFilePath);
                    var protetedContentBytes = Convert.FromBase64String(protectedContentBytesBase64);
                    var contentBytes = ProtectedData.Unprotect(protetedContentBytes, null, DataProtectionScope.CurrentUser);
                    var jsonContent = Encoding.UTF8.GetString(contentBytes);
                    var credentials = JsonConvert.DeserializeObject<Credentials>(jsonContent);
                    result = credentials.Password;
                }
            }
            catch
            {
                this.ClearCredential();
            }

            return result;
        }

        public void SetCredential(string username, string password)
        {
            try
            {
                string jsonContent = JsonConvert.SerializeObject(new Credentials { UserName = username, Password = password });
                var contentBytes = Encoding.UTF8.GetBytes(jsonContent);

                var protectedContentBytes = ProtectedData.Protect(contentBytes, null, DataProtectionScope.CurrentUser);
                var protectedContentBytesBase64 = Convert.ToBase64String(protectedContentBytes);
                File.WriteAllText(this.tokenFilePath, protectedContentBytesBase64);
            }
            catch (PlatformNotSupportedException)
            {
                Debug.WriteLine("Could not store credentials");
            }
        }

        private string GetTokenFile()
        {
            string homeUserProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            return Path.Join(homeUserProfile, ".devops", "token.bin");
        }

        private class Credentials
        {
            public string UserName { get; set; }

            public string Password { get; set; }
        }
    }
}
