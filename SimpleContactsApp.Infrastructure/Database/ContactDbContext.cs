using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimpleContactsApp.Domain.Entities;

namespace SimpleContactsApp.Infrastructure.Database
{
    public class ContactDbContext : IdentityDbContext<User>
    {
        /*
         *This constructor creates DbContext after checking if there are any pending migrations, if there are new
         * if new migrations are found , it will apply them and updates the database.
         */
        public ContactDbContext(DbContextOptions<ContactDbContext> options) : base(options)
        {
            if (Database.GetPendingMigrations().Any())
                 Database.Migrate();
        }

        public DbSet<Contact> Contacts { get; set; } = null!;
    }
}
