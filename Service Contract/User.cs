﻿using System;
using System.Collections.Generic;
using System.Security.Claims;
using ErikTheCoder.Utilities;
using JetBrains.Annotations;


namespace ErikTheCoder.ServiceContract
{
    [UsedImplicitly]
    public class User
    {
        // ReSharper disable MemberCanBePrivate.Global
        // ReSharper disable UnusedMember.Global
        public int Id { get; [UsedImplicitly] set; }
        public string Username { get; [UsedImplicitly] set; }
        public int PasswordManagerVersion { get; [UsedImplicitly] set; }
        public string Salt { get; [UsedImplicitly] set; }
        public string PasswordHash { get; [UsedImplicitly] set; }
        public string EmailAddress { get; [UsedImplicitly] set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName => $"{FirstName} {LastName}";
        public HashSet<string> Roles { get; [UsedImplicitly] set; }
        public Dictionary<string, HashSet<string>> Claims { get; [UsedImplicitly] set; }
        public string SecurityToken { get; set; }
        // ReSharper restore UnusedMember.Global
        // ReSharper restore MemberCanBePrivate.Global


        public User()
        {
            Roles = new HashSet<string>(StringComparer.CurrentCultureIgnoreCase);
            Claims = new Dictionary<string, HashSet<string>>(StringComparer.CurrentCultureIgnoreCase);
        }


        [UsedImplicitly]
        public List<Claim> GetClaims()
        {
            var claims = new List<Claim>
            {
                // Include user properties.
                new Claim(ClaimTypes.Name, Username),
                new Claim(ClaimTypes.Email, EmailAddress),
                new Claim(CustomClaimType.FirstName, FirstName),
                new Claim(CustomClaimType.LastName, LastName),
                new Claim(CustomClaimType.SecurityToken, SecurityToken ?? string.Empty)
            };
            // Include roles and claims.
            foreach (var role in Roles) claims.Add(new Claim(ClaimTypes.Role, role));
            foreach (var (claimType, claimValues) in Claims) foreach (var claimValue in claimValues) claims.Add(new Claim(claimType, claimValue));
            return claims;
        }


        [UsedImplicitly]
        public static User ParseClaims(IEnumerable<Claim> Claims)
        {
            var user = new User();
            foreach (var claim in Claims)
            {
                // ReSharper disable once SwitchStatementMissingSomeCases
                switch (claim.Type)
                {
                    case ClaimTypes.Name:
                        user.Username = claim.Value;
                        break;
                    case ClaimTypes.Email:
                        user.EmailAddress = claim.Value;
                        break;
                    case CustomClaimType.FirstName:
                        user.FirstName = claim.Value;
                        break;
                    case CustomClaimType.LastName:
                        user.LastName = claim.Value;
                        break;
                    case CustomClaimType.SecurityToken:
                        user.SecurityToken = claim.Value;
                        break;
                    case ClaimTypes.Role:
                        user.Roles.Add(claim.Value);
                        break;
                    default:
                        if (!user.Claims.ContainsKey(claim.Type)) user.Claims.Add(claim.Type, new HashSet<string>(StringComparer.CurrentCultureIgnoreCase));
                        user.Claims[claim.Type].Add(claim.Value);
                        break;
                }
            }
            return user;
        }
    }
}
