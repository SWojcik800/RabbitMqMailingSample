using RabbitMQ.Client;
using Microsoft.Extensions.Configuration;
using Common.RabbitMq;
using FluentValidation;
using MailingProducer;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;
// Add services to the container.

//builder.Services.AddSingleton(configuration);
builder.Services.AddMessageBus(configuration);

builder.Services.AddValidatorsFromAssembly(typeof(IMailingProducerAssemblyMarker).Assembly);

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

app.UseAuthorization();

app.MapControllers();

app.Run();
