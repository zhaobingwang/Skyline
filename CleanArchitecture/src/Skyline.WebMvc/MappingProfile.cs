using AutoMapper;
using Skyline.ApplicationCore.Entities.ContactAggregate;
using Skyline.WebMvc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skyline.WebMvc
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Contact Mapping
            // ENTITY => VM
            CreateMap<Contact, ContactViewModel>();
            CreateMap<Contact, ContactEditViewModel>();
            CreateMap<Contact, ContactDetailsViewModel>();

            // VM => ENTITY
            CreateMap<ContactCreateViewModel, Contact>(); 
            #endregion
        }
    }
}
