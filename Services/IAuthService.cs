using Library.API.Dtos;

namespace Library.API.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(UserRegisterDto request);
        Task<AuthResponseDto> LoginAsync(UserLoginDto request);
    }
}
