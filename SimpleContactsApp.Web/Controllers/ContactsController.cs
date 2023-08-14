using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleContactsApp.Application.Interfaces;
using SimpleContactsApp.Domain.Entities;
using System.Security.Claims;

namespace SimpleContactsApp.Web.Controllers
{
    [Authorize]
    public class ContactsController : Controller
    {
        private readonly IContactService _contactService;

        public ContactsController(IContactService contactService)
        {
            _contactService = contactService ?? throw new ArgumentNullException(nameof(contactService));
        }
        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetString("UserId");
            var contacts = await _contactService.GetContactsAsync(userId!);
            return View(contacts);
        }

        /*
        * Retrieve UserId from a session and use the searchTerm to look for contacts
        */
        [HttpGet]
        public async Task<IActionResult> Index(string searchTerm)
        {
            var userId = HttpContext.Session.GetString("UserId");
            var contacts = await _contactService.SearchContactsAsync(userId!, searchTerm);
            ViewData["SearchTerm"] = searchTerm;
            return View(contacts);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        /*
        * Retrieve UserId from a session and get create a new Contact.
        */
        [HttpPost]
        public async Task<IActionResult> Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                var userId = HttpContext.Session.GetString("UserId");
                contact.UserId = userId!;
                await _contactService.CreateContactAsync(contact);
                return RedirectToAction(nameof(Index));
            }
            return View(contact);
        }

        /*
        * Retrieve UserId from a session and get a contact. 
        */
        public async Task<IActionResult> Edit(int id)
        {
            var userId = HttpContext.Session.GetString("UserId");
            var contact = await _contactService.GetContactAsync(id, userId!);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }

        /*
        * Retrieve a contact from db. 
        */
        [HttpPost]
        public async Task<IActionResult> Edit(Contact contact)
        {
            if (ModelState.IsValid)
            {
                await _contactService.UpdateContactAsync(contact);
                return RedirectToAction(nameof(Index));
            }
            return View(contact);
        }

        /*
        * Retrieve UserId from a session and get a contact. 
        */
        public async Task<IActionResult> Details(int id)
        {
            var userId = HttpContext.Session.GetString("UserId");

            var contact = await _contactService.GetContactAsync(id, userId!);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }

        /*
        * Retrieve UserId from a session and get contact that will be removed. 
        */
        public async Task<IActionResult> Delete(int id)
        {
            var userId = HttpContext.Session.GetString("UserId");

            var contact = await _contactService.GetContactAsync(id, userId!);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }

        /*
         * Retrieve UserId from a session and remove contact from the database. 
         */
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = HttpContext.Session.GetString("UserId");

            await _contactService.DeleteContactAsync(id, userId!);
            return RedirectToAction(nameof(Index));
        }
    }
}
