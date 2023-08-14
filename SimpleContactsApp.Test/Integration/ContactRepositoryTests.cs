/*
using Microsoft.EntityFrameworkCore;
using Moq;
using SimpleContactsApp.Domain.Entities;
using SimpleContactsApp.Infrastructure.Database;
using SimpleContactsApp.Infrastructure.Interfaces;
using SimpleContactsApp.Infrastructure.Repositories;
using Xunit;

namespace SimpleContactsApp.Test.Integration
{
    public class TestDbContextFactory : IDbContextFactory<ContactDbContext>
    {
        public ContactDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<ContactDbContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            return new ContactDbContext(options);
        }
    }

    public class ContactRepositoryTests : IDisposable
    {
        private readonly ContactDbContext _dbContext;
        private readonly IContactRepository _contactRepository;

        public ContactRepositoryTests()
        {
            var dbContextFactory = new TestDbContextFactory();
            _dbContext = dbContextFactory.CreateDbContext();

            _contactRepository = new ContactRepository(dbContextFactory);
        }

        [Fact]
        public async Task GetAllContactsAsync_ReturnsContacts()
        {
            // Arrange
            var userId = "user123";
            var contacts = new[]
            {
            new Contact { UserId = userId, Name = "Iron Man" },
            new Contact { UserId = userId, Name = "Black Widow" }
        };

            await _dbContext.Contacts.AddRangeAsync(contacts);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _contactRepository.GetAllContactsAsync(userId);

            // Assert
            Assert.Equal(contacts.Length, result.Count());
        }

        [Fact]
        public async Task GetContactByIdAsync_ExistingContact_ReturnsContact()
        {
            // Arrange
            var userId = "user123";
            var contactId = 1;
            var contact = new Contact { Id = contactId, UserId = userId, Name = "Iron Man" };

            await _dbContext.Contacts.AddAsync(contact);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _contactRepository.GetContactByIdAsync(contactId, userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(contact.Id, result.Id);
        }

        [Fact]
        public async Task GetContactByIdAsync_NonExistingContact_ReturnsNull()
        {
            // Arrange
            var userId = "user123";
            var contactId = 1;

            // Act
            var result = await _contactRepository.GetContactByIdAsync(contactId, userId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateContactAsync_AddsContactToDatabase()
        {
            // Arrange
            var contact = new Contact { Name = "Spider Man" };

            // Act
            var contactId = await _contactRepository.CreateContactAsync(contact);

            // Assert
            var savedContact = await _dbContext.Contacts.FindAsync(contactId);
            Assert.NotNull(savedContact);
            Assert.Equal(contact.Name, savedContact.Name);
        }

        [Fact]
        public async Task UpdateContactAsync_UpdatesContactInDatabase()
        {
            // Arrange
            var contact = new Contact { Name = "Spider Man" };
            await _dbContext.Contacts.AddAsync(contact);
            await _dbContext.SaveChangesAsync();

            // Act
            contact.Name = "Hulk";
            await _contactRepository.UpdateContactAsync(contact);

            // Assert
            var updatedContact = await _dbContext.Contacts.FindAsync(contact.Id);
            Assert.NotNull(updatedContact);
            Assert.Equal(contact.Name, updatedContact.Name);
        }

        [Fact]
        public async Task DeleteContactAsync_DeletesContactFromDatabase()
        {
            // Arrange
            var userId = "user123";
            var contactId = 1;
            var contact = new Contact { Id = contactId, UserId = userId, Name = "Iron Man" };

            await _dbContext.Contacts.AddAsync(contact);
            await _dbContext.SaveChangesAsync();

            // Act
            await _contactRepository.DeleteContactAsync(contactId, userId);

            // Assert
            var deletedContact = await _dbContext.Contacts.FindAsync(contactId);
            Assert.Null(deletedContact);
        }

        [Fact]
        public async Task SearchContactsAsync_ReturnsMatchingContacts()
        {
            // Arrange
            var userId = "user123";
            var contacts = new[]
            {
            new Contact { UserId = userId, Name = "Iron Man", Email = "IronMan@Avengers.com", Phone = "123" },
            new Contact { UserId = userId, Name = "Black Widow", Email = "blackwidow@Avengers.com", Phone = "456" }
        };

            await _dbContext.Contacts.AddRangeAsync(contacts);
            await _dbContext.SaveChangesAsync();

            // Act
            var searchTerm = "Iron";
            var result = await _contactRepository.SearchContactsAsync(userId, searchTerm);

            // Assert
            Assert.Single(result);
            Assert.Equal("Iron Man", result.First().Name);
        }

        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }
    }
}*/
