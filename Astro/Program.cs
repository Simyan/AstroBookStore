
using Application.CommandHandlers;
using Core;
using Core.BookInventory;
using Infrastructure;
using Marten;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Weasel.Core;

namespace Astro
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblies(
                [
                    typeof(CreateBookCommandHandler).Assembly,
                    typeof(StartOrderCommandHandler).Assembly,
                    typeof(CompleteOrderCommandHandler).Assembly,
                 ]));
            builder.Services.AddDbContext<AstroDBContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("AstroDbConnectionString")));


            builder.Services.AddMarten(options =>
            {
                options.Connection(builder.Configuration.GetConnectionString("AstroDbConnectionString") 
                                                                    ?? throw new ArgumentNullException());
                options.UseSystemTextJsonForSerialization();
                if (builder.Environment.IsDevelopment())
                {
                    options.AutoCreateSchemaObjects = AutoCreate.CreateOnly;
                }
            });
            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();



            app.Run();
        }
    }
}
