using FluentValidation;

namespace WebApi.BookOperations;

public class GetBookDetailQueryValidator : AbstractValidator<GetBookDetailQuery>
{
    public GetBookDetailQueryValidator() 
    {
        RuleFor(query => query.BookId).GreaterThan(0);
    }
}
