using ApplicationLayer.DTOs.Auth;
using ApplicationLayer.DTOs.UserDtos;
using ApplicationLayer.Interfaces.Reposetory.Auth;
using DomenLayer.UserEntity;
using InforsturctiorLayer.DbContextFolder;
using InforsturctiorLayer.Interfaces.Reposetory.BaseRepo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InforsturctiorLayer.Interfaces.Reposetory.AuthRepo
{

    public class AuthReposetory : BaseReposetory<Users>, IAuthReposetory
    {

        private readonly IConfiguration _configuration;

        public AuthReposetory(ApplicationDbContext context, IConfiguration configuration) : base(context)
        {
            this._configuration = configuration;
        }

        public async Task<ResponseLogin?> LoginAsync(LoginUserDto login)
        {
            // 1️⃣ الحصول على المستخدم من قاعدة البيانات حسب البريد الإلكتروني
            var user = await GetUserByEmail(login.Email);
            if (user == null)
                return null; // أو رمي استثناء حسب التصميم

            // 2️⃣ التحقق من كلمة المرور
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(login.Password, user.PasswordHash);
            if (!isPasswordValid)
                return null; // أو رمي استثناء لكلمة مرور خاطئة
            var token = GenerateJwtToken(user.Pk_Id,user.Name, user.Role.ToString(), _configuration);

            var response = new ResponseLogin
            {
                UserId =user.Pk_Id,
                Tokin = token, // هنا نعطي العميل التوكن
                UserName = user.Name,
                Role = user.Role.ToString()
            };


            return response;
        }

        public string GenerateJwtToken(int Pk_Id,string userName, string role, IConfiguration configuration)
        {

            var jwtSettings = configuration.GetSection("JwT");
            var key = jwtSettings["Key"];
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];
            var durationInDays = int.Parse(jwtSettings["DurationInDays"] ?? "30"); // قيمة افتراضية 30 يوم
            var expiration = DateTime.Now.AddDays(durationInDays);

            // 2️⃣ تعريف الـ Claims (المعلومات التي سيحملها التوكن)
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userName), // الاسم
                new Claim(ClaimTypes.Role, role),                 // الدور
                new Claim("id", Pk_Id.ToString()),          // <-- أضفنا الـ userId
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
           };


            // 3️⃣ توليد الـ Signing Key
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                                      issuer: issuer,
                                      audience: audience,
                                      claims: claims,
                                      expires: expiration,
                                      signingCredentials: credentials);


            // 5️⃣ تحويل التوكن إلى string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<Users> ChingedPasswordAsync(ChengedPasswordDto chenged)
        {
            // 1️⃣ جلب المستخدم
            var user = await GetUserByEmail(chenged.Email);
            if (user == null)throw new Exception();
            // 2️⃣ التحقق من كلمة المرور القديمة


            // 3️⃣ تشفير كلمة المرور الجديدة
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(chenged.NewPassword);
            // 4️⃣ حفظ التغييرات

            // 5️⃣ نجاح العملية
            return user;
        }

        public async Task<Users?> GetUserByEmail(string email)
        {
             var Data=await _context.users
                .FirstOrDefaultAsync(u => u.Email == email);

            return Data;
        }


    }
}
