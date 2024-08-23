using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserAuthService.Models;

namespace UserAuthService.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

       

        private async Task<int> GetNextUserIdAsync()
        {
            // Rastgele bir kullanıcı ID'si üretmek için uygun bir yöntem kullanabilirsiniz
            // Örneğin, GUID veya bir rastgele sayı üreteci gibi
            Random rnd = new Random();
            return rnd.Next(1000, 9999); // Örnek olarak 1000 ile 9999 arasında bir rastgele sayı
        }
        public async Task<bool> RegisterUserAsync(User user, string password)
        {

            user.kullaniciId = await GetNextUserIdAsync();
            _context.Users.Add(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<string> LoginUserAsync(string email, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.kullaniciMail == email && u.kullaniciSifre == password);
            if (user != null)
            {
                // Token oluşturma işlemi burada yapılacak
                return "generated_token";
            }
            return null;
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            if (!int.TryParse(userId, out int intUserId))
            {
                return false; // Geçersiz kullanıcı ID'si
            }

            var user = await _context.Users.FindAsync(intUserId);
            if (user != null)
            {
                _context.Users.Remove(user);
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            return false;
        }

        public async Task<bool> ResetPasswordAsync(string email, string newPassword)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.kullaniciMail == email);
            if (user != null)
            {
                user.kullaniciSifre = newPassword;
                _context.Users.Update(user);
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            return false;
        }
    }
}
