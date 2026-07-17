using GamesTradeIn.Domain.Aggregates.User;
using GamesTradeIn.Domain.Repositories;
using MediatR;

namespace GamesTradeIn.Application.Features.Commands.Users.CreateUser;

public class CreateUserCommandHandler(IUserRepository userRepository) : IRequestHandler<CreateUserCommand, Guid>
{
    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User(Guid.NewGuid(), request.Name, request.Email, request.Balance);
        foreach (var item in request.Wishlist)
        {
            user.AddToWishlist(item.Title, item.Platform);
        }

        await userRepository.AddAsync(user);
        await userRepository.SaveChangesAsync();
        
        return user.Id;
    }
}