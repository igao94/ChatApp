using Application.Messages.Queries.SearchChat;
using FluentValidation;

namespace Application.Messages.Validators;

public sealed class SearchChatValidator : AbstractValidator<SearchChatQuery>
{
    public SearchChatValidator()
    {
        RuleFor(sc => sc.MessageParams.SearchTerm).NotEmpty().WithMessage("SearchTerm is required.");
    }
}
