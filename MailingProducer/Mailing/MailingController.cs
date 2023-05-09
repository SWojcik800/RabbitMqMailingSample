using Common.RabbitMq;
using MailingProducer.Contracts;
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
        public void SendMail(MailingMessage request)
        {
            _messageBus.Publish<MailingMessage>(request, AppConsts.MailRequestsExchange, AppConsts.MailRequestsRoutingKey);
        }
    }
}
