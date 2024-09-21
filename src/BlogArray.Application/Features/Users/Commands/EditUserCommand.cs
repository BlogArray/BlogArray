using BlogArray.Domain.DTOs;
using BlogArray.Domain.Interfaces;
using FluentValidation;
using MediatR;

namespace BlogArray.Application.Features.Users.Commands;

public class EditUserValidator : AbstractValidator<EditUserInfo>
{
    public EditUserValidator()
    {
        RuleFor(x => x.UserName).NotNull().NotEmpty();
        RuleFor(x => x.Password).NotNull().NotEmpty();
    }
}

public class EditUserCommand(EditUserInfo model, int loggedInUser) : IRequest<ReturnResult<int>>
{
    public EditUserInfo Model { get; set; } = model;

    public int LoggedInUserId { get; set; } = loggedInUser;
}

internal class EditUserCommandHandler(IAccountRepository accountRepository) : IRequestHandler<EditUserCommand, ReturnResult<int>>
{
    public async Task<ReturnResult<int>> Handle(EditUserCommand request, CancellationToken cancellationToken)
    {
        return await accountRepository.EditUser(request.Model, request.LoggedInUserId);
    }
}
