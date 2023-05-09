using Common.RabbitMq;
using MailingProducer.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;

namespace MailingProducer.Mailing
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailingController : ControllerBase
    {
        private readonly IMessageBus _messageBus;

        public MailingController(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        [HttpPost]
        public void SendMail(MailingRequest request)
        {
            _messageBus.Publish<MailingRequest>(request, "mailRequestsExchange", "mailRequestsRoutingKey");
        }
    }
}
