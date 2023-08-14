using SimpleContactsApp.Application.Interfaces;
using SimpleContactsApp.Domain.Entities;
using SimpleContactsApp.Infrastructure.Interfaces;

namespace SimpleContactsApp.Application.Services
{
    /*
     * Contact service for the application layer This exposes the repository CRUD operations for the api. 
     */
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository ?? throw new ArgumentNullException(nameof(contactRepository));
        }

        public async Task<IEnumerable<Contact>> GetContactsAsync(string userId)
        {
            return await _contactRepository.GetAllContactsAsync(userId);
        }

        public async Task<Contact> GetContactAsync(int contactId, string userId)
        {
            return await _contactRepository.GetContactByIdAsync(contactId, userId);
        }

        public async Task<int> CreateContactAsync(Contact contact)
        {
            return await _contactRepository.CreateContactAsync(contact);
        }

        public async Task UpdateContactAsync(Contact contact)
        {
            await _contactRepository.UpdateContactAsync(contact);
        }

        public async Task DeleteContactAsync(int contactId, string userId)
        {
            await _contactRepository.DeleteContactAsync(contactId, userId);
        }

        public async Task<IEnumerable<Contact>> SearchContactsAsync(string userId, string searchTerm)
        {
            return await _contactRepository.SearchContactsAsync(userId, searchTerm);
        }
    }
}
