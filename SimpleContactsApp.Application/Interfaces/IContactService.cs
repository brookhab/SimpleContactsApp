using SimpleContactsApp.Domain.Entities;

namespace SimpleContactsApp.Application.Interfaces
{
    public interface IContactService
    {
        Task<IEnumerable<Contact>> GetContactsAsync(string userId);
        Task<Contact> GetContactAsync(int contactId, string userId);
        Task<int> CreateContactAsync(Contact contact);
        Task UpdateContactAsync(Contact contact);
        Task DeleteContactAsync(int contactId, string userId);
        Task<IEnumerable<Contact>> SearchContactsAsync(string userId, string searchTerm);

    }
}
