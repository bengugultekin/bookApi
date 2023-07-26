using FluentValidation;

namespace WebApi.Application.AuthorOperations.Commands;

public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
{
    public CreateAuthorCommandValidator()
    {
        RuleFor(command => command.model.FirstName).MinimumLength(4);
        RuleFor(command => command.model.LastName).MinimumLength(4);
        RuleFor(command => command.model.DateOfBirth).NotEmpty().LessThan(DateTime.Now.Date);
    }
}
