namespace MailingProducer.Contracts.Requests
{
    public sealed class MailingRequest
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
