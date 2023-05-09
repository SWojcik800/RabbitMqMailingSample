using FluentValidation;
using MailingProducer.Contracts.Requests;

namespace MailingProducer.Mailing
{
    public class MailingMessageValidator : AbstractValidator<MailingMessage>
    {
        public MailingMessageValidator()
        {
            RuleFor(x => x.From).NotEmpty().EmailAddress()
                .WithMessage("Invalid source email address");
            RuleFor(x => x.To).NotEmpty().EmailAddress()
                .WithMessage("Invalid destination email address");
            RuleFor(x => x.Title).NotEmpty()
                .WithMessage("Email title cannot be empty");
            RuleFor(x => x.Content).NotEmpty()
                .WithMessage("Email content cannot be empty");

        }
    }
}
