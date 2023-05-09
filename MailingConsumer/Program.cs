using Common.RabbitMq;
using Common.Smtp;
using MailingProducer.Contracts;
using MailingProducer.Contracts.Messages;
using MailingProducer.Contracts.Requests;
using System.Net.Mail;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddSmtpClientConfig(configuration);
builder.Services.AddMessageBus(configuration);
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


var messageBus = app.Services.GetRequiredService<IMessageBus>();
var smtpSettings = app.Services.GetRequiredService<SmtpSettings>();
messageBus.Subscribe<MailingMessage>(AppConsts.MailRequestsExchange, AppConsts.MailRequestsQueue, AppConsts.MailRequestsRoutingKey, 
    (MailingMessage request)
    =>
    {
        if(request.SendMailType is SendMailType.Server)
        {
            using (var smptClient = SmtpClientFactory.Create(smtpSettings))
            {

                var message = new MailMessage(request.From, request.To, request.Title, request.Content);

                smptClient.Send(message);
            };

        } else
        {
            Console.WriteLine($"---------------MESSAGE SEND---------------");
            Console.WriteLine($"FROM: {request.From}");
            Console.WriteLine($"TO: {request.To}");
            Console.WriteLine($"TITLE: {request.Title}");
            Console.WriteLine($"CONTENT: {request.Content}");
            Console.WriteLine($"------------------------------------------");

        }
    }
    );


app.Run();

