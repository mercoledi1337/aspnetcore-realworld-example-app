using Conduit.Features.Articles.Application.Commands;
using Conduit.Features.Articles.Application.Interfaces;
using Conduit.Features.Articles.Application.Repos;
using Conduit.Features.MIddleware;
using Conduit.Infrastructure;
using Conduit.Infrastructure.DataAccess;
using Conduit.Infrastructure.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var authenticationSettings = new AuthenticationSettings();

builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);

builder.Services.AddSingleton(authenticationSettings);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<ICurrentUser, CurrentUser>();
builder.Services.AddScoped<IPasswordHash, PasswordHash>();
builder.Services.AddScoped<Create>();
builder.Services.AddScoped<SetTagsForArticles>();
builder.Services.AddScoped<IArticlesRepository, ArticlesRepository>();
builder.Services.AddScoped<IJwtToken, JwtToken>();
//Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("Front", builder =>
    {
        builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
    });
});

var inmemory = builder.Configuration.GetValue<bool>("UseInMemory");
var connectionString = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING"));
});

//builder.Services.AddDefaultIdentity<Profile>(options => options.SignIn.RequireConfirmedAccount = false)
//             .AddEntityFrameworkStores<ConduitContext>();

//builder.Services.AddIdentityServer()
//    .AddApiAuthorization<Profile, ConduitContext>()
//    .AddDeveloperSigningCredential();

builder.Services.AddSwaggerGen(x =>
{
    x.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
    });

    x.OperationFilter<SecurityRequirementsOperationFilter>();
});


builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                builder.Configuration.GetSection("Authentication:JwtKey").Value!))
    };
})

    .AddIdentityServerJwt();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("is-admin", policy =>
    {
        policy.RequireRole("admin");
    });

});

builder.Services.AddMediatR(cfg =>
     cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Host.UseSerilog((context, configuration) => 
    configuration.ReadFrom.Configuration(context.Configuration));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// Inject an implementation of ISwaggerProvider with defaulted settings applied

builder.Services.AddHttpContextAccessor();
var app = builder.Build();

// Configure the HTTP request pipeline.

    app.UseSwagger(c =>
    {
        c.RouteTemplate = "swagger/{documentName}/swagger.json";
    });
    
    // Enable middleware to serve swagger-ui assets(HTML, JS, CSS etc.)
    app.UseSwaggerUI(x =>
    {
        x.SwaggerEndpoint("/swagger/v1/swagger.json", "RealWorld API V1");
    });


app.UseCors("Front");

//app.UseSerilogRequestLogging();



app.UseHttpsRedirection();



app.UseMiddleware<GlobalExceptionHandleingMiddleware>();

app.UseMiddleware<GlobalResponsExceptionHandlingMiddleware>();



app.UseAuthorization();

app.MapControllers();

app.Run();
