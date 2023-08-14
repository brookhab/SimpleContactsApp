using SimpleContactsApp.Domain.Entities;

namespace SimpleContactsApp.Infrastructure.Interfaces
{
    public  interface IContactRepository
    {
        Task<IEnumerable<Contact>> GetAllContactsAsync(string userId);
        Task<Contact> GetContactByIdAsync(int id, string userId);
        Task<int> CreateContactAsync(Contact contact);
        Task UpdateContactAsync(Contact contact);
        Task DeleteContactAsync(int id, string userId);
        Task<IEnumerable<Contact>> SearchContactsAsync(string searchTerm, string userId);

    }
}
