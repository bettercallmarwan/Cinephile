using Application.Exceptions;
using Application.Interfaces.Auth;
using Application.Interfaces.GenreInterfaces;
using Application.Interfaces.LogInterfaces;
using Application.Interfaces.MovieDiaryInterfaces;
using Application.Interfaces.MovieInterfaces;
using Application.Mapping;
using Application.Services.Auth;
using Application.Services.CommonServices;
using Application.Services.GenreServices;
using Application.Services.LogServices;
using Application.Services.MovieDiaryServices;
using Application.Services.MovieServices;
using Domain.Entities;
using Domain.Entities.Identity;
using Domain.Interfaces.Repositories;
using Infrastructure.Data.Contexts;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Generic;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Cinephile
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            #region Database
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            #endregion

            #region AutoMapper
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            #endregion

            #region Identity
            services.AddIdentity<ApplicationUser, IdentityRole<int>>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 6;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            #endregion

            #region JWT
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = configuration["Jwt:Audience"],
                        ValidIssuer = configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
                    };
                });
            #endregion

            #region JWT Services
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IAuthService, AuthService>();
            #endregion

            #region Movie
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            #endregion

            services.AddScoped<IGenericRepository<Genre>, GenericRepository<Genre>>();
            services.AddScoped<IGenreService, GenreService>();


            services.AddScoped<ILogService, LogService>();
            services.AddScoped<ILogRepository, LogRepository>();


            services.AddScoped<IMovieDiaryService, MovieDiaryService>();
            services.AddScoped<IMovieDiaryRepository, MovieDiaryRepository>();



            services.AddScoped<FileService, FileService>();
            #region Validation error
            services
            .AddControllers()
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = false;

                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(p => p.Value!.Errors.Count() > 0)
                                        .ToDictionary
                                        (
                                            kvp => kvp.Key,
                                            kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
                                        );

                    var exception = new ValidationErrorResponse(errors);

                    return new BadRequestObjectResult(exception.ToString());
                };
            });

            return services;
            #endregion
        }
    }
}
