using MailingProducer.Contracts.Messages;

namespace MailingProducer.Contracts.Requests
{
    public sealed class MailingMessage
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public SendMailType SendMailType { get; set; }
    }
}
