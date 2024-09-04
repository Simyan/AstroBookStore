
using Application.CommandHandlers;
using Core;
using Core.BookInventory;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace Atlas
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
                [typeof(CreateBookCommandHandler).Assembly]));
            builder.Services.AddDbContext<AstroDBContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("AstroDbConnectionString")));

            

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
