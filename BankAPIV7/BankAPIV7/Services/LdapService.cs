﻿using BankAPIV7.Models;
using Microsoft.Extensions.Options;
using System.DirectoryServices;
using System.Runtime.Versioning;
using System.Text.RegularExpressions;


namespace BankAPIV7.Services
{
    [SupportedOSPlatform("windows")]
    public class LdapService:ILdapService
    {
        private const string EmailAttribute = "mail";
        private const string UserNameAttribute = "uid";

        private readonly LdapConfig config;

        public LdapService(IOptions<LdapConfig> config)
        {
            this.config = config.Value;
        }
        public User Search(string userName)
        {
            //whitelist validation for vulnerability test
            if (Regex.IsMatch(userName, "^[a-zA-Z][a-zA-Z0-9]*$"))
            {
                using (DirectoryEntry entry = new DirectoryEntry(config.Path))
                {
                    entry.AuthenticationType = AuthenticationTypes.Anonymous;
                    using (DirectorySearcher searcher = new DirectorySearcher(entry))
                    {
                        searcher.Filter = "(&(" + UserNameAttribute + "=" + userName + "))";
                        searcher.PropertiesToLoad.Add(EmailAttribute);
                        searcher.PropertiesToLoad.Add(UserNameAttribute);
                        var result = searcher.FindOne();
                        if (result != null)
                        {
                            var email = result.Properties[EmailAttribute];
                            var uid = result.Properties[UserNameAttribute];

                            return new User
                            {
                                Email = email == null || email.Count <= 0 ? null : email[0].ToString(),
                                UserName = uid == null || uid.Count <= 0 ? null : uid[0].ToString()
                            };
                        }
                    }
                }
            }
            return null;
        }


    }
}
