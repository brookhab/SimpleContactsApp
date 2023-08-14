using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using SimpleContactsApp.Application.Interfaces;
using SimpleContactsApp.Application.Services;
using SimpleContactsApp.Domain.Entities;
using SimpleContactsApp.Infrastructure.Interfaces;
using Xunit;

namespace SimpleContactsApp.Test.Unit.Application
{
    public class ContactsServiceTests
    {
        private readonly Mock<IContactRepository> _mockContactRepository;
        private readonly IContactService _contactService;

        public ContactsServiceTests()
        {
            _mockContactRepository = new Mock<IContactRepository>();
            _contactService = new ContactService(_mockContactRepository.Object);
        }

        [Fact]
        public async Task GetContactsAsync_ReturnsContacts()
        {
            // Arrange
            var userId = "user123";
            var fakeContacts = new List<Contact>
            {
                new Contact { Id = 1, Name = "Spider Man" },
                new Contact { Id = 2, Name = "Cat Woman" }
            };
            _mockContactRepository.Setup(repo => repo.GetAllContactsAsync(userId))
                .ReturnsAsync(fakeContacts);

            // Act
            var result = await _contactService.GetContactsAsync(userId);

            // Assert
            Assert.Equal(fakeContacts, result);
        }

        [Fact]
        public async Task GetContactAsync_ReturnsContact()
        {
            // Arrange
            var userId = "user123";
            var contactId = 1;
            var fakeContact = new Contact { Id = contactId, Name = "Spider Man" };
            _mockContactRepository.Setup(repo => repo.GetContactByIdAsync(contactId, userId))
                .ReturnsAsync(fakeContact);

            // Act
            var result = await _contactService.GetContactAsync(contactId, userId);

            // Assert
            Assert.Equal(fakeContact, result);
        }

        [Fact]
        public async Task CreateContactAsync_ReturnsContactId()
        {
            // Arrange
            var newContact = new Contact { Name = "Spider Man" };
            var newContactId = 1;
            _mockContactRepository.Setup(repo => repo.CreateContactAsync(newContact))
                                  .ReturnsAsync(newContactId);

            // Act
            var result = await _contactService.CreateContactAsync(newContact);

            // Assert
            Assert.Equal(newContactId, result);
        }

        [Fact]
        public async Task UpdateContactAsync_CallsRepositoryMethod()
        {
            // Arrange
            var contactToUpdate = new Contact { Id = 1, Name = "Wonder Woman" };

            // Act
            await _contactService.UpdateContactAsync(contactToUpdate);

            // Assert
            _mockContactRepository.Verify(repo => repo.UpdateContactAsync(contactToUpdate), Times.Once);
        }

        [Fact]
        public async Task DeleteContactAsync_CallsRepositoryMethod()
        {
            // Arrange
            var contactId = 1;
            var userId = "user123";

            // Act
            await _contactService.DeleteContactAsync(contactId, userId);

            // Assert
            _mockContactRepository.Verify(repo => repo.DeleteContactAsync(contactId, userId), Times.Once);
        }

        [Fact]
        public async Task SearchContactsAsync_ReturnsSearchedContacts()
        {
            // Arrange
            var userId = "user123";
            var searchTerm = "Spider";
            var fakeContacts = new List<Contact>
            {
                new Contact { Id = 1, Name = "Spider Man" }
            };
            _mockContactRepository.Setup(repo => repo.SearchContactsAsync(userId, searchTerm))
                                  .ReturnsAsync(fakeContacts);

            // Act
            var result = await _contactService.SearchContactsAsync(userId, searchTerm);

            // Assert
            Assert.Equal(fakeContacts, result);
        }
    }
}
