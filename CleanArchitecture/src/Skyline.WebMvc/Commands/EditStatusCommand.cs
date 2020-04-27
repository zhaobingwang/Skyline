using MediatR;
using Skyline.WebMvc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skyline.WebMvc.Commands
{
    public class EditStatusCommand : IRequest<bool>
    {
        public EditStatusViewModel EditStatusViewModel { get; set; }
        public EditStatusCommand(EditStatusViewModel vm)
        {
            EditStatusViewModel = vm;
        }
    }
}
