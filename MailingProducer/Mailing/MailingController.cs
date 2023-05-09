using Common.RabbitMq;
using FluentValidation;
using FluentValidation.Results;
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
        private readonly IValidator<MailingMessage> _validator;
        public MailingController(IMessageBus messageBus, IValidator<MailingMessage> validator)
        {
            _messageBus = messageBus;
            _validator = validator;
        }

        [HttpPost]
        public async Task<IResult> SendMail(MailingMessage request, CancellationToken cancellationToken)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if(!validationResult.IsValid)
                return Results.ValidationProblem(validationResult.ToDictionary());

            _messageBus.Publish<MailingMessage>(request, AppConsts.MailRequestsExchange, AppConsts.MailRequestsRoutingKey);

            return Results.Ok();
        }
    }
}
