using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleContactsApp.Application.Interfaces;
using SimpleContactsApp.Domain.Entities;
using System.Security.Claims;

namespace SimpleContactsApp.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            HttpContext.Request.Query.TryGetValue("userId",out var userId);
            var contacts = await _contactService.GetContactsAsync(userId);
            return Ok(contacts);
        }

        /*
        * Retrieve UserId from a session and use the searchTerm to look for contacts
        */
        [HttpGet()]
        public async Task<IActionResult> Search(string searchTerm)
        {
            HttpContext.Request.Query.TryGetValue("userId", out var userId);
            var contacts = await _contactService.SearchContactsAsync(userId, searchTerm);
            return Ok(contacts);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                HttpContext.Request.Query.TryGetValue("userId", out var userId);
                await _contactService.CreateContactAsync(contact);
                return CreatedAtAction(nameof(Get), new { id = contact.Id }, contact);
            }
            return BadRequest(ModelState);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            HttpContext.Request.Query.TryGetValue("userId", out var userId);
            var contact = await _contactService.GetContactAsync(id, userId);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Contact contact)
        {
            if (ModelState.IsValid)
            {
                await _contactService.UpdateContactAsync(contact);
                return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            HttpContext.Request.Query.TryGetValue("userId", out var userId);
            var contact = await _contactService.GetContactAsync(id, userId);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            HttpContext.Request.Query.TryGetValue("userId", out var userId);
            var contact = await _contactService.GetContactAsync(id, userId);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            HttpContext.Request.Query.TryGetValue("userId", out var userId);
            await _contactService.DeleteContactAsync(id, userId);
            return NoContent();
        }
    }
}
