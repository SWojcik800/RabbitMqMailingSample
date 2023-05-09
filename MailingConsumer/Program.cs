using Common.RabbitMq;
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
messageBus.Subscribe<MailingRequest>("mailRequestsExchange", "mailRequestsQueue", "mailRequestsRoutingKey", (MailingRequest request)
    => Console.WriteLine(request.From));


app.Run();

