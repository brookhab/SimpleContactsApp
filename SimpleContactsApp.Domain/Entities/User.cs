using Microsoft.AspNetCore.Identity;

namespace SimpleContactsApp.Domain.Entities
{
    public class User : IdentityUser
    {
        public ICollection<Contact> Contacts { get; set; }
    }
}
