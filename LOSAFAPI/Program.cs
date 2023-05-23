using LOSAFAPI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using LOSAFAPI;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Cors;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<UserApiDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("LosafApiConnectionString")));

builder.Services.AddDbContext<AddRegisterItemsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LosafApiConnectionString")));


builder.Services.AddDbContext<ReportItemsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LosafApiConnectionString")));

builder.Services.AddDbContext<ClaimItemDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LosafApiConnectionString")));

builder.Services.AddDbContext<FoundItemsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LosafApiConnectionString")));





var key = "test12345678$$$";
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddSingleton<JwtAuthenticationManager>(new JwtAuthenticationManager(key));

builder.Services.AddCors(options =>
 options.AddPolicy("MyallowedPolicy", policy =>
 policy.WithOrigins("https://localhost:7058", "http://127.0.0.1:5500").AllowAnyHeader().AllowAnyMethod())
);
//var corsBuilder = new CorsPolicyBuilder();
//corsBuilder.AllowAnyOrigin();
//corsBuilder.AllowAnyHeader();
//corsBuilder.AllowAnyMethod();
//corsBuilder.AllowCredentials();
//var app = builder.Build();


///////



/////




var app = builder.Build();

app.UseCors("MyallowedPolicy");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseDefaultFiles();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
