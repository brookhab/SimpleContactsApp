using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SimpleContactsApp.Application.Interfaces;
using SimpleContactsApp.Domain.Entities;
using SimpleContactsApp.Infrastructure.Interfaces;
using SimpleContactsApp.Web.Controllers;
using Xunit;

namespace SimpleContactsApp.Test.Unit.Web
{
    public class ContactsControllerTests
    {
        private readonly Mock<IContactService> _mockedContactService;

        public ContactsControllerTests()
        {
            _mockedContactService = new Mock<IContactService>();
        }

        private ContactsController SetupControllerWithSessionData(string userId, Dictionary<string, string> sessionData)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId)
            };

            var user = new ClaimsPrincipal(new ClaimsIdentity(claims));
            var httpContext = new DefaultHttpContext { User = user, Session = CreateMockSession(sessionData) };

            return new ContactsController(_mockedContactService.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = httpContext
                }
            };
        }

        private ISession CreateMockSession(Dictionary<string, string> sessionData)
        {
            var session = new Mock<ISession>();

            foreach (var kvp in sessionData)
            {
                var key = kvp.Key;
                var value = kvp.Value;

                byte[] byteArray = null;
                if (value != null)
                {
                    byteArray = Encoding.UTF8.GetBytes(value);
                }

                session.Setup(s => s.TryGetValue(key, out byteArray))
                    .Returns(byteArray != null);
               
                session.Setup(s => s.Set(key, It.IsAny<byte[]>()))
                    .Callback<string, byte[]>((k, v) => byteArray = v);
            }

            return session.Object;
        }


        [Fact]
        public void Controller_WithValidService_ShouldNotThrowException()
        {
            Assert.NotNull(new ContactsController(_mockedContactService.Object));
        }

        [Fact]
        public void Constructor_WithNullService_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new ContactsController(null));
        }

        [Fact]
        public async Task Index_ReturnsViewWithContacts()
        {
            // Arrange
            var userId = "user123";
            var sessionData = new Dictionary<string, string>
            {
                { "UserId", userId }
            };

            var controller = SetupControllerWithSessionData(userId, sessionData);

            var fakeContacts = new List<Contact>
            {
                new Contact { Id = 1, Name = "Iron Man" },
                new Contact { Id = 2, Name = "Black Widow" }
            };
            _mockedContactService.Setup(service => service.GetContactsAsync(userId))
                .ReturnsAsync(fakeContacts);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Contact>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count()); // Ensure the correct number of contacts
        }

        [Fact]
        public async Task Index_ReturnsViewWithSearchedContacts()
        {
            // Arrange
            var userId = "user123";
            var searchTerm = "Iron";
            var sessionData = new Dictionary<string, string>
            {
                { "UserId", userId }
            };

            var controller = SetupControllerWithSessionData(userId, sessionData);

            var fakeContacts = new List<Contact>
            {
                new Contact { Id = 1, Name = "Iron Man" },
                new Contact { Id = 2, Name = "Black Widow" }
            };
            _mockedContactService.Setup(service => service.SearchContactsAsync(userId, searchTerm))
                .ReturnsAsync(fakeContacts);

            // Act
            var result = await controller.Index(searchTerm);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Contact>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count()); // Ensure the correct number of contacts
            Assert.Equal(searchTerm, viewResult.ViewData["SearchTerm"]);
        }

        [Fact]
        public async Task Create_ValidModel_RedirectsToIndex()
        {
            // Arrange
            var userId = "user123";
            var sessionData = new Dictionary<string, string>
            {
                { "UserId", userId }
            };

            var controller = SetupControllerWithSessionData(userId, sessionData);

            var validContact = new Contact { Name = "Iron Man" };

            // Act
            var result = await controller.Create(validContact);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(ContactsController.Index), redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Create_InvalidModel_ReturnsView()
        {
            // Arrange
            var controller = new ContactsController(_mockedContactService.Object);
            controller.ModelState.AddModelError("Name", "Name is required");

            var invalidContact = new Contact();

            // Act
            var result = await controller.Create(invalidContact);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(invalidContact, viewResult.Model);
        }

        [Fact]
        public async Task Edit_ExistingContact_ReturnsView()
        {
            // Arrange
            var userId = "user123";
            var sessionData = new Dictionary<string, string>
            {
                { "UserId", userId }
            };

            var controller = SetupControllerWithSessionData(userId, sessionData);

            var existingContact = new Contact { Id = 1, Name = "Iron Man" };
            _mockedContactService.Setup(service => service.GetContactAsync(existingContact.Id, userId))
                .ReturnsAsync(existingContact);

            // Act
            var result = await controller.Edit(existingContact.Id);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Contact>(viewResult.Model);
            Assert.Equal(existingContact, model);
        }

        [Fact]
        public async Task Edit_NonExistingContact_ReturnsNotFound()
        {
            // Arrange
            var userId = "user123";
            var sessionData = new Dictionary<string, string>
            {
                { "UserId", userId }
            };

            var controller = SetupControllerWithSessionData(userId, sessionData);

            _mockedContactService.Setup(service => service.GetContactAsync(It.IsAny<int>(), userId))
                .ReturnsAsync((Contact)null);

            // Act
            var result = await controller.Edit(123); // Non-existing contact ID

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_ExistingContact_ReturnsView()
        {
            // Arrange
            var userId = "user123";
            var sessionData = new Dictionary<string, string>
            {
                { "UserId", userId }
            };

            var controller = SetupControllerWithSessionData(userId, sessionData);

            var existingContact = new Contact { Id = 1, Name = "Iron Man" };
            _mockedContactService.Setup(service => service.GetContactAsync(existingContact.Id, userId))
                .ReturnsAsync(existingContact);

            // Act
            var result = await controller.Details(existingContact.Id);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Contact>(viewResult.Model);
            Assert.Equal(existingContact, model);
        }

        [Fact]
        public async Task Details_NonExistingContact_ReturnsNotFound()
        {
            // Arrange
            var userId = "user123";
            var sessionData = new Dictionary<string, string>
            {
                { "UserId", userId }
            };

            var controller = SetupControllerWithSessionData(userId, sessionData);

            _mockedContactService.Setup(service => service.GetContactAsync(It.IsAny<int>(), userId))
                .ReturnsAsync((Contact)null);

            // Act
            var result = await controller.Details(123); // Non-existing contact ID

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ExistingContact_ReturnsView()
        {
            // Arrange
            var userId = "user123";
            var sessionData = new Dictionary<string, string>
            {
                { "UserId", userId }
            };

            var controller = SetupControllerWithSessionData(userId, sessionData);

            var existingContact = new Contact { Id = 1, Name = "Iron Man" };
            _mockedContactService.Setup(service => service.GetContactAsync(existingContact.Id, userId))
                .ReturnsAsync(existingContact);

            // Act
            var result = await controller.Delete(existingContact.Id);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Contact>(viewResult.Model);
            Assert.Equal(existingContact, model);
        }

        [Fact]
        public async Task Delete_NonExistingContact_ReturnsNotFound()
        {
            // Arrange
            var userId = "user123";
            var sessionData = new Dictionary<string, string>
            {
                { "UserId", userId }
            };

            var controller = SetupControllerWithSessionData(userId, sessionData);

            _mockedContactService.Setup(service => service.GetContactAsync(It.IsAny<int>(), userId))
                .ReturnsAsync((Contact)null);

            // Act
            var result = await controller.Delete(123); // Non-existing contact ID

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteConfirmed_ExistingContact_RedirectsToIndex()
        {
            // Arrange
            var userId = "user123";
            var sessionData = new Dictionary<string, string>
            {
                { "UserId", userId }
            };

            var controller = SetupControllerWithSessionData(userId, sessionData);

            var existingContact = new Contact { Id = 1, Name = "Iron Man" };
            _mockedContactService.Setup(service => service.GetContactAsync(existingContact.Id, userId))
                .ReturnsAsync(existingContact);

            // Act
            var result = await controller.DeleteConfirmed(existingContact.Id);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(ContactsController.Index), redirectToActionResult.ActionName);
        }
    }
}
