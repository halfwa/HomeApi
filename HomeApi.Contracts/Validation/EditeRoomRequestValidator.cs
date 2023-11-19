using FluentValidation;
using HomeApi.Contracts.Models.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeApi.Contracts.Validation
{
    public class EditeRoomRequestValidator: AbstractValidator<EditeRoomRequest>
    {
        public EditeRoomRequestValidator()
        {
            RuleFor(x =>  x.Name).NotEmpty().Must(BeSupported)
                .WithMessage($"Please choose one of the following locations: {string.Join(", ", Values.ValidRooms)}");
            RuleFor(x =>  x.Area).NotEmpty();
            RuleFor(x =>  x.GasConnected).NotEmpty();
            RuleFor(x =>  x.Voltage).NotEmpty().InclusiveBetween(120, 220 )
                .WithMessage("Устройства с напряжением меньше 120 и больше 180 вольт не поддерживаются"); ;
        }
        private bool BeSupported(string location)
        {
            return Values.ValidRooms.Any(e => e == location);
        }
    }
}
