
using InforsturctiorLayer.DbContextFolder;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ----------------------------
// 🔹 Configure DbContext
// ----------------------------
// ربط EF Core بقاعدة البيانات باستخدام SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ----------------------------
// 🔹 Repository & Service Dependency Injection
// ----------------------------
// تسجيل جميع Repositories و Services في DI Container
builder.Services.AddInfrastructureServices(builder.Configuration);

// ----------------------------
// 🔹 Controllers
// ----------------------------
builder.Services.AddControllers();

// ----------------------------
// 🔹 Swagger Setup
// ----------------------------
// إضافة Swagger لتوليد واجهة API تلقائية
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Task Mangmnt API", Version = "v1" });

    // ----------------------------
    // 🔹 Configure JWT Bearer Authentication in Swagger
    // ----------------------------
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",               // اسم الهيدر
        Type = SecuritySchemeType.Http,       // نوع المصادقة
        Scheme = "Bearer",                     // Bearer
        BearerFormat = "JWT",                  // صيغة الـ Token
        In = ParameterLocation.Header,         // مكان إرسال التوكن
        Description = "أدخل JWT Token هنا (Bearer <Token>)" // وصف للمستخدم
    });

    // إضافة Security Requirement حتى تستخدم الـ Token تلقائيًا
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {} // لا حاجة لتحديد Scopes هنا
        }
    });
});

// ----------------------------
// 🔹 Authentication (JWT)
// ----------------------------
// إعداد المصادقة باستخدام JWT Bearer Tokens
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true, // التحقق من الـ Issuer
        ValidateAudience = true, // التحقق من الـ Audience
        ValidateLifetime = true, // التحقق من مدة صلاحية التوكن
        ValidateIssuerSigningKey = true, // التحقق من صحة التوقيع
        ValidIssuer = builder.Configuration["Jwt:Issuer"], // قراءة Issuer من appsettings.json
        ValidAudience = builder.Configuration["Jwt:Audience"], // قراءة Audience
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]) // مفتاح التشفير
        )
    };
});

// ----------------------------
// 🔹 Authorization
// ----------------------------
// تمكين سياسات الصلاحيات ([Authorize] / Roles)
builder.Services.AddAuthorization();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

builder.Services.AddControllers();


var app = builder.Build();
app.UseCors("AllowAll");

// ----------------------------
// 🔹 Swagger Middleware
// ----------------------------
// تمكين Swagger UI مع Endpoint للمشروع
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hotel API V1");
    c.RoutePrefix = string.Empty; // Swagger UI يظهر مباشرة على /
});

// ----------------------------
// 🔹 HTTPS
// ----------------------------
app.UseHttpsRedirection();

// ----------------------------
// 🔹 Authentication & Authorization Middleware
// ----------------------------
// Authentication يجب أن يكون قبل Authorization
app.UseAuthentication();
app.UseAuthorization();

// ----------------------------
// 🔹 Map Controllers
// ----------------------------
app.MapControllers();

// ----------------------------
// 🔹 Run Application
// ----------------------------
app.Run();
