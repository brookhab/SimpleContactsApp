using System.ComponentModel.DataAnnotations;

namespace SimpleContactsApp.Domain.Entities
{
    public class Contact
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; }

        // Foreign key to link contact with user
        public string UserId { get; set; }

        public User? User { get; set; }
    }
}
