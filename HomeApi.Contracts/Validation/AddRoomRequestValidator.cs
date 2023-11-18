using FluentValidation;
using FluentValidation.Validators;
using HomeApi.Contracts.Models.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeApi.Contracts.Validation
{
    public class AddRoomRequestValidator: AbstractValidator<AddRoomRequest>
    {
        public AddRoomRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty();    
            RuleFor(x => x.Area).NotEmpty();    
            RuleFor(x => x.GasConnected).NotEmpty();    
            RuleFor(x => x.Voltage).NotEmpty();    
        }
    }
}
