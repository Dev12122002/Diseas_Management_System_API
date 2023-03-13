using Diseas_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Diseas_Management_System.Data
{
    public class AuthRepo : IAuthRepo
    {
        private readonly DiseasContext _context;
        private readonly IConfiguration _configuration;

        public AuthRepo(DiseasContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<int> Register(Admin admin, string password)
        {
            if (await AdminExists(admin.Email))
            {
                return 0;
            }
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            admin.PasswordHash = passwordHash;
            admin.PasswordSalt = passwordSalt;

            _context.Admin.Add(admin);
            await _context.SaveChangesAsync();
            return admin.AdminId;
        }

        public async Task<bool> AdminExists(string email)
        {
            if (await _context.Admin.AnyAsync(u => u.Email.ToLower() == email.ToLower()))
            {
                return true;
            }
            return false;
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<string?> Login(string email, string password)
        {
            var admin = await _context.Admin.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
            if (admin == null)
            {
                return null;
            }
            else if (!VerifyPasswordHash(password, admin.PasswordHash, admin.PasswordSalt))
            {

                return null;
            }
            else
                return CreateToken(admin);
        }
        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }

        public string CreateToken(Admin admin)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, admin.AdminId.ToString()),
                new Claim(ClaimTypes.Email, admin.Email)
            };

            var appSettingsToken = _configuration.GetSection("AppSettings:Token").Value;
            if (appSettingsToken == null)
            {
                throw new Exception("AppSettings Token is null");
            }
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(appSettingsToken));
            SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = signingCredentials
            };
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);

        }
    }
}
