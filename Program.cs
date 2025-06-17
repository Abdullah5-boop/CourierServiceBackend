
using System.Text;
using Courier_Service_part_1.DbContext;
using Courier_Service_part_1.Model.Service;
using Courier_Service_part_1.Model.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Courier_Service_part_1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            

            builder.Services.AddControllers();
            builder.Services.AddDbContext<Order_tracking_dbContext>(option => 
            option.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

            builder.Services.AddDbContext<AdminDbContext>
                (optionsAction => optionsAction.UseSqlServer(builder.Configuration.GetConnectionString("Default")));


            builder.Services.AddDbContext<UserIdentityDbContext>(option=>option.UseSqlServer(builder.Configuration.GetConnectionString("identification")));

            builder.Services.AddIdentityApiEndpoints<UserClass>().AddEntityFrameworkStores<UserIdentityDbContext>();




            builder.Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement { { new OpenApiSecurityScheme { Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                } },new string[]{} } });
            });



            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(x =>
           {
               x.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"])),
                   //IssuerSigningKey = new SymmetricSecurityKey("dzxc!@Rauiofgjhgfhjghjkhjkhhgdhfgdofihg!"u8.ToArray()),
                   ValidIssuer = builder.Configuration["JWT:Issuer"],
                   ValidAudience = builder.Configuration["JWT:Audience"],

               };
           });

            builder.Services.AddScoped<TokenProviderClass>();



            builder.Services.AddCors(option => {
                option.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin();
                    policy.AllowAnyMethod();
                    policy.AllowAnyHeader();
                });

            });



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

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors("AllowAll");
            app.MapControllers();

            app.Run();
        }
    }
}
