using Common.RabbitMq;
using MailingProducer.Contracts;
using MailingProducer.Contracts.Messages;
using MailingProducer.Contracts.Requests;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
messageBus.Subscribe<MailingMessage>(AppConsts.MailRequestsExchange, AppConsts.MailRequestsQueue, AppConsts.MailRequestsRoutingKey, 
    (MailingMessage request)
    =>
    {
        if(request.SendMailType is SendMailType.Server)
        {
            //send via mail server
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

