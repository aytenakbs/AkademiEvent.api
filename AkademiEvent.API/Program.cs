
using AkademiEvent.API.Models.ORM;
using AkademiEvent.API.Models.Validations.Activity;
using AkademiEvent.API.Models.Validations.Category;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace AkademiEvent.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers().AddFluentValidation();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<AkademiEventContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });
        });
        builder.Services.AddValidatorsFromAssemblyContaining<CreateCategoryRequestValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<UpdateCategoryRequestValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<CreateActivityRequestValidator>();
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = "https://localhost:5001";
                options.Audience = "resourceapi";
            });
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();

        app.UseAuthorization();


        app.MapControllers();
        app.UseCors();
        app.UseStaticFiles();

        app.Run();
    }
}
