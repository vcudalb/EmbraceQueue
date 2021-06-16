using Microsoft.AspNetCore.Identity;

namespace EmbraceQueue.Infrastructure.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsSubscribed { get; set; }
    }
}
