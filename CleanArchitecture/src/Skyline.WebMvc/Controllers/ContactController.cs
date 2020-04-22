using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Skyline.WebMvc.Commands;

namespace Skyline.WebMvc.Controllers
{
    public class ContactController : Controller
    {
        private readonly IMediator _mediator;
        public ContactController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<IActionResult> Index()
        {
            var vm = await _mediator.Send(new ContactList());
            return View(vm);
        }
    }
}