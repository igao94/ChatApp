using Application.Users.Queries.SearchUsers;
using FluentValidation;

namespace Application.Users.Validators;

public sealed class SearchUsersValidator : AbstractValidator<SearchUsersQuery>
{
    public SearchUsersValidator()
    {
        RuleFor(sq => sq.UserParams.SearchTerm)
            .NotEmpty().WithMessage("SearchTerm is required.");
    }
}
