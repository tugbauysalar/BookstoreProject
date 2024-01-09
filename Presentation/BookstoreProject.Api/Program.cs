using System.Reflection;
using BookstoreProject.Application;
using BookstoreProject.Application.Mapping;
using BookstoreProject.Application.Services;
using BookstoreProject.Domain.Entities;
using BookstoreProject.Persistence;
using BookstoreProject.Persistence.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddFluentValidation(options =>
{
    options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
});

builder.Services.Configure<CustomTokenOption>(builder.Configuration.GetSection("Token Option"));
// Add services to the container.
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opts =>
{
    var tokenOption = builder.Configuration.GetSection("TokenOption").Get<CustomTokenOption>();
    opts.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidIssuer = tokenOption.Issuer,
        ValidAudience = tokenOption.Audience,
        IssuerSigningKey = SignService.GetSymmetricSecurityKey(tokenOption.SecurityKey),

        ValidateIssuerSigningKey = true,
        ValidateAudience = true, 
        ValidateIssuer = true,
        ValidateLifetime = true,

    };
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitofWork, UnitofWork>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddAutoMapper(typeof(MapProfile));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("BookstoreProject.Persistence")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        builder => builder
            .SetIsOriginAllowed(origin=>true)
            .AllowAnyHeader()
            .AllowAnyMethod()
        
        );
});
builder.Services.AddIdentity<User, Role>().AddEntityFrameworkStores<ApplicationDbContext>();
var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c=>c.SwaggerEndpoint("/swagger/v1/swagger.json","BookstoreProject v1"));
}

app.UseHttpsRedirection();
app.UseCors("AllowOrigin");
app.UseRouting();
app.UseAuthorization();

app.MapControllers();
app.Run();
