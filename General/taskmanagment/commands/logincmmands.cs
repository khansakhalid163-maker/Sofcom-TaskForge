using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskForge.Models;
using TaskForge.Repositories.Interface;

namespace TaskForge.General.Auth.Commands
{
    public class LoginCommand : IRequest<LoginResponse>
    {
        public LoginRequest Request { get; set; }
        public LoginCommand(LoginRequest request) => Request = request;
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public LoginCommandHandler(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.Request.Email);
            if (user == null)
                throw new Exception("Invalid email or password.");

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Request.Password, user.PasswordHash);
            if (!isPasswordValid)
                throw new Exception("Invalid email or password.");

            var jwtSettings = _configuration.GetSection("JwtSettings");
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];
            var expiryMinutes = int.Parse(jwtSettings["ExpiryMinutes"]);
            var secretKey = $"{issuer}{audience}SuperSecretKeyForTaskForge";

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Name, user.FullName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
                signingCredentials: credentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new LoginResponse
            {
                Token = tokenString,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role
            };
        }
    }
}