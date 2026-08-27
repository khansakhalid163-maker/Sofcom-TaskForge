using MediatR;
using TaskForge.Models;
using TaskForge.Repositories.Interface;

namespace TaskForge.General.Auth.Commands
{
    public class RegisterCommand : IRequest<int>
    {
        public RegisterRequest Request { get; set; }
        public RegisterCommand(RegisterRequest request) => Request = request;
    }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, int>
    {
        private readonly IUserRepository _userRepository;

        public RegisterCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<int> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var existing = await _userRepository.GetUserByEmailAsync(request.Request.Email);
            if (existing != null)
                throw new Exception("Email already exists.");

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Request.Password);

            var user = new User
            {
                FullName = request.Request.FullName,
                Email = request.Request.Email,
                PasswordHash = hashedPassword,
                Role = request.Request.Role
            };

            var newId = await _userRepository.AddUserAsync(user);
            return newId;
        }
    }
}