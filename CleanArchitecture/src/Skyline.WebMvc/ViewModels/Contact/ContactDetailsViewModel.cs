﻿using Skyline.ApplicationCore.Entities.ContactAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skyline.WebMvc.ViewModels
{
    public class ContactDetailsViewModel : ContactBaseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string Zip { get; set; }
        public ContactStatus Status { get; set; }
    }
}
