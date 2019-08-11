using System.Security.Claims;
using JetBrains.Annotations;


namespace ErikTheCoder.ServiceContract
{
    [UsedImplicitly]
    public static class ExtensionMethods
    {
        [UsedImplicitly]
        public static User ToAppUser(this ClaimsPrincipal User) => ServiceContract.User.ParseClaims(User.Claims);
    }
}
