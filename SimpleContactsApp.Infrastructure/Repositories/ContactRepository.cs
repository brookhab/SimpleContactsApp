using Microsoft.EntityFrameworkCore;
using SimpleContactsApp.Domain.Entities;
using SimpleContactsApp.Infrastructure.Database;
using SimpleContactsApp.Infrastructure.Interfaces;

namespace SimpleContactsApp.Infrastructure.Repositories
{
    /*
     * This class implements the basic CRUD operations for the api.
     * It uses new db context for each operation to prevent concurrency issues since entity framework dbcontext is not thread safe.
     */
    public class ContactRepository : IContactRepository
    {
        private readonly IDbContextFactory<ContactDbContext> _dbContextFactory;

        public ContactRepository(IDbContextFactory<ContactDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory)) ;
        }

        public async Task<IEnumerable<Contact>> GetAllContactsAsync(string userId)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync(default);

            return await context.Contacts
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }

        public async Task<Contact> GetContactByIdAsync(int id, string userId)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync(default);
            return await context.Contacts
                .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);
        }

        public async Task<int> CreateContactAsync(Contact contact)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync(default);
            context.Contacts.Add(contact);
            await context.SaveChangesAsync();
            return contact.Id;
        }

        public async Task UpdateContactAsync(Contact contact)
        {
            await using var _context = await _dbContextFactory.CreateDbContextAsync(default);
            _context.Contacts.Update(contact);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteContactAsync(int id, string userId)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync(default);
            var contact = await context.Contacts
                .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

            if (contact != null)
            {
                context.Contacts.Remove(contact);
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Contact>> SearchContactsAsync(string userId, string searchTerm)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync(default);
            
            if (searchTerm == null) 
                return  await context.Contacts.Where(c=>c.UserId==userId).ToListAsync();
            
            return await context.Contacts
                .Where(c => c.UserId == userId && (c.Name.Contains(searchTerm) || c.Email.Contains(searchTerm) || c.Phone.Contains(searchTerm)))
                .ToListAsync();
        }
    }
}

