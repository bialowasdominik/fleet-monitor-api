using FleetMonitorAPI.Entities;
using FleetMonitorAPI.Exceptions;
using FluentValidation;

namespace FleetMonitorAPI.Models.Validators
{
    public class RegisterUserDtoValidator: AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(FleetMonitorDbContext dbContext)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .Custom((value, context) =>
                {
                    var emailInUse = dbContext.Users.Any(p => p.Email == value);
                    if (emailInUse)
                        context.AddFailure("Email", ExceptionDictionary.TakenEmail);
                });

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(10);

            RuleFor(x => x.ConfirmPassword)
                .Equal(e => e.Password);

            RuleFor(x => x.FirstName)
                .NotEmpty();

            RuleFor(x => x.LastName)
                .NotEmpty();
                
        }
    }
}
