using System.Collections.Generic;
using JetBrains.Annotations;


namespace ErikTheCoder.ServiceContract
{
    public interface IUser
    {
        [UsedImplicitly] int Id { get; set; }
        [UsedImplicitly] string Username { get; set; }
        [UsedImplicitly] int PasswordManagerVersion { get; set; }
        [UsedImplicitly] string Salt { get; set; }
        [UsedImplicitly] string PasswordHash { get; set; }
        [UsedImplicitly] string EmailAddress { get; set; }
        [UsedImplicitly] string FirstName { get; set; }
        [UsedImplicitly] string LastName { get; set; }
        [UsedImplicitly] string DisplayName { get; }
        [UsedImplicitly] HashSet<string> Roles { get; set; }
        [UsedImplicitly] Dictionary<string, HashSet<string>> Claims { get; set; }
        [UsedImplicitly] string SecurityToken { get; set; }
    }
}
