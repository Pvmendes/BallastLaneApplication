using BallastLaneApplication.Core.Interfaces.Repository;
using BallastLaneApplication.Core.Interfaces.Service;
using BallastLaneApplication.Infrastructure.Data.MongoDB;
using BallastLaneApplication.Infrastructure.Data.MongoDB.Repository;
using BallastLaneApplication.Infrastructure.Interfaces.Repository;
using BallastLaneApplication.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<MongoDbContext>(sp => new MongoDbContext(builder.Configuration.GetConnectionString("DefaultConnectionMongoDB"), "BallastLaneDB"));

builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("user", policy => policy.RequireClaim("Task", "user"));
    options.AddPolicy("admin", policy => policy.RequireClaim("Task", "admin"));
});

var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(x => x
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin()
    );

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

//app.UseRouting();
app.UseEndpoints(endpoints => 
    {
        endpoints.MapControllers();
    });

//app.MapControllers();

app.Run();
