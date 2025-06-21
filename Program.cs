using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BudgetZ.API.Data;
using BudgetZ.API.Services;
using DotNetEnv;
using Microsoft.AspNetCore.Routing;

var builder = WebApplication.CreateBuilder(args);

// .env dosyasından environment değişkenlerini yükle
Env.Load();

// Environment değişkeninden connection string'i ekle
builder.Configuration["ConnectionStrings:DefaultConnection"] = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

// Environment değişkenlerinden JWT yapılandırmasını ekle
builder.Configuration["Jwt:Key"] = Environment.GetEnvironmentVariable("JWT_KEY");
builder.Configuration["Jwt:Issuer"] = Environment.GetEnvironmentVariable("JWT_ISSUER");
builder.Configuration["Jwt:Audience"] = Environment.GetEnvironmentVariable("JWT_AUDIENCE");

// Servisleri container'a ekle
// OpenAPI yapılandırması hakkında daha fazla bilgi için: https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();

// Controller'ları ekle
builder.Services.AddControllers();

// URL'lerin küçük harfli olmasını zorla
builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
});

// Swagger/OpenAPI yapılandırması hakkında daha fazla bilgi için: https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen();

// DbContext'i ekle
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Token servisini ekle
builder.Services.AddScoped<ITokenService, TokenService>();

// JWT Kimlik doğrulamasını ekle
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT anahtarı yapılandırılmamış."))
            )
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
