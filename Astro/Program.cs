
using Application.CommandHandlers;
using Core;
using Core.BookInventory;
using Core.Order;
using Infrastructure;
using Marten;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Oakton;
using System.Configuration;
using Weasel.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ApplyOaktonExtensions();

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
    options.Projections.Add<OrderSummaryProjection>(Marten.Events.Projections.ProjectionLifecycle.Async);
    if (builder.Environment.IsDevelopment())
    {
        options.AutoCreateSchemaObjects = AutoCreate.All;
    }
})
.AddAsyncDaemon(Marten.Events.Daemon.Resiliency.DaemonMode.Solo);


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



//app.Run();
return await app.RunOaktonCommands(args);

//namespace Astro
//{
//    public class Program
//    {

//    }
//}
