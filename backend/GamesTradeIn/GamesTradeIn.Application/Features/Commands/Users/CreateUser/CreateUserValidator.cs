using FluentValidation;

namespace GamesTradeIn.Application.Features.Commands.Users.CreateUser;

public class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        RuleSet(nameof(CreateUserCommand), () =>
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("The Name is required")
                .MaximumLength(150).WithMessage("The Name must be less than 150 characters");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("The Email is required")
                .EmailAddress().WithMessage("The Email is not valid");

            RuleFor(x => x.Balance)
                .GreaterThan(0).WithMessage("The Balance must be greater than 0");

            RuleForEach(x => x.Wishlist)
                .ChildRules(item =>
                {
                    item.RuleFor(i => i.Title).NotEmpty().WithMessage("The Wishlist item title is required");
                    item.RuleFor(i => i.Platform).NotEmpty().WithMessage("The Wishlist item platform is required");
                });
        });
    }
}


