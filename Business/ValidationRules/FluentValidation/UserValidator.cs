using Core.Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.FirstName).NotEmpty();
            RuleFor(u => u.FirstName).MaximumLength(100);
            RuleFor(u => u.LastName).NotEmpty();
            RuleFor(u => u.LastName).MaximumLength(100);
            RuleFor(u => u.Email).NotEmpty();
            RuleFor(u => u.Email).MaximumLength(100);
            RuleFor(u => u.Email).EmailAddress();
        }
    }
}
