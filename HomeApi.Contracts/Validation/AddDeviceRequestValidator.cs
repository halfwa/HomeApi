using FluentValidation;
using HomeApi.Contracts.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeApi.Contracts.Validators
{
    /// <summary>
    /// Класс-валидатор запросов подключения
    /// </summary>
    public class AddDeviceRequestValidator : AbstractValidator<AddDeviceRequest>
    {
        private string[] _validLocations;
        /// <summary>
        /// Метод, конструктор, устанавливающий правила
        /// </summary>
        public AddDeviceRequestValidator()
        {
            _validLocations = new[]
            {
                   "Kitchen",
                   "Bathroom",
                   "Livingroom",
                   "Toilet"
            };

            /* Зададим правила валидации */
            RuleFor(x => x.Name).NotEmpty(); // Проверим на null и на пустое свойство
            RuleFor(x => x.Manufacturer).NotEmpty();
            RuleFor(x => x.Model).NotEmpty();
            RuleFor(x => x.SerialNumber).NotEmpty();
            RuleFor(x => x.CurrentVolts).InclusiveBetween(120, 220)
                .WithMessage("Устройства с напряжением меньше 120 и больше 180 вольт не поддерживаются");
            RuleFor(x => x.GasUsage).NotNull();
            RuleFor(x => x.Location).NotEmpty()
                .Must(BeSupported)
                .WithMessage($"Please choose one of the following locations: {string.Join(", ", _validLocations)}");

        }

        /// <summary>
        ///  Метод кастомной валидации для свойства location
        /// </summary>
        private bool BeSupported(string location)
        {
            return _validLocations.Any(e => e == location);
        }
    }
}
