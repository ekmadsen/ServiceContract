using System;
using System.Collections.Generic;
using System.Security.Claims;
using ErikTheCoder.Utilities;
using JetBrains.Annotations;


namespace ErikTheCoder.ServiceContract
{
    [UsedImplicitly]
    public class User : IUser
    {
        // ReSharper disable MemberCanBePrivate.Global
        // ReSharper disable UnusedMember.Global
        public int Id { get; set; }
        public string Username { get; set; }
        public int PasswordManagerVersion { get; set; }
        public string Salt { get; set; }
        public string PasswordHash { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName => $"{FirstName} {LastName}";
        public HashSet<string> Roles { get; set; }
        public Dictionary<string, HashSet<string>> Claims { get; set; }
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
