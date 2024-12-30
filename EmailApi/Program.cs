using EmailApi;
using EmailApi.Handler;
using EmailApi.Models.BAO;
using EmailApi.Models.Context;
using EmailApi.Models.DAO;
using EmailApi.Models.Entities;
using EmailApi.Models.Middleware;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PracticeAPIS.Models.BAO;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using System.Text;
using TokensGenarate.Models.DAO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var securityKey = builder.Configuration["Jwt:SecurityKey"];
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = true;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        };
    });


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    c.OperationFilter<SwaggerFileOperationFilter>(); // Add this line
});
builder.Services.AddAuthentication("BasicAuthentication").AddScheme<AuthenticationSchemeOptions,BasicAuthenticationHandler>("BasicAuthentication",null);

builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<EmailDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default"))
);
// Comment out the InMemory database if not needed
// builder.Services.AddDbContext<EmailDbContext>(options =>
//             options.UseInMemoryDatabase("FileDb"));

builder.Services.AddScoped<FileDao>();
builder.Services.AddScoped<UserDao>();
builder.Services.AddScoped<UserBao>();
builder.Services.AddScoped<User1Dao>();
builder.Services.AddScoped<ITokenDAO, TokenDAO>();
builder.Services.AddScoped<TokenBAO>();
builder.Services.Configure<Jwtset>(builder.Configuration.GetSection("Jwt"));
builder.Services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<Jwtset>>().Value);

builder.Services.AddTransient<EmailService>();

builder.Services.AddLogging(config =>
{
    config.AddConsole();
    config.AddDebug();
});

const string policyName = "CorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: policyName, builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty;  // Serves Swagger UI at the app's root
    });
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseCors("CorsPolicy");

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();
app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

// Custom Operation Filter for Swagger
public class SwaggerFileOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var fileParameters = context.MethodInfo.GetParameters()
            .Where(p => p.ParameterType == typeof(IFormFile))
            .ToList();

        if (fileParameters.Any())
        {
            operation.Parameters.Clear();
            foreach (var fileParameter in fileParameters)
            {
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = fileParameter.Name,
                    In = ParameterLocation.Query,
                    Description = "Upload File",
                    Required = true,
                    Schema = new OpenApiSchema
                    {
                        Type = "file"
                    }
                });
            }
        }
    }
}
