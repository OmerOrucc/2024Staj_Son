using System.Threading.Tasks;
using UserAuthService.Models;

namespace UserAuthService.Services
{
    public interface IUserService
    {
        Task<bool> DeleteUserAsync(string id);
        Task<bool> RegisterUserAsync(User kullaniciAdi, string kullaniciSifre);
        Task<string> LoginUserAsync(string kullaniciMail, string kullaniciSifre);
        Task<bool> ResetPasswordAsync(string kullaniciMail, string kullaniciYeniSifre);
    }
}
