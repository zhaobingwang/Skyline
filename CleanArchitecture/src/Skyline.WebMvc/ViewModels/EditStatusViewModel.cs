using Skyline.ApplicationCore.Entities.ContactAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skyline.WebMvc.ViewModels
{
    public class EditStatusViewModel
    {
        public int Id { get; set; }
        public ContactStatus Status { get; set; }
    }
}
