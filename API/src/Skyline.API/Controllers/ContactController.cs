using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skyline.ApplicationCore.Entities.ContactAggregate;
using Skyline.ApplicationCore.Interfaces;

namespace Skyline.API.Controllers
{
    //[Route("[controller]")]
    [Route("contacts")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactRepository _contactRepository;
        public ContactController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            var contacts = await _contactRepository.ListAllAsync();
            return new JsonResult(contacts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContacts(int id)
        {
            var contacts = await _contactRepository.GetByIdAsync(id);
            return new JsonResult(contacts);
        }

        [HttpPost]
        public async Task<IActionResult> AddContacts()
        {
            var contact = await _contactRepository.AddAsync(new Contact
            {
                Name = $"测试{Guid.NewGuid().ToString().Substring(0, 8)}",
                Address = "文一路",
                City = "杭州市",
                Province = "浙江省",
                Zip = "310000",
                Email = "test@example.com",
                MobileNumber = "",
                Status = ContactStatus.Approved,
                OwnerId = "349ecef2-db74-4ae6-bae0-c88a89ce8f2b"
            });
            return new JsonResult(contact);
        }
    }
}