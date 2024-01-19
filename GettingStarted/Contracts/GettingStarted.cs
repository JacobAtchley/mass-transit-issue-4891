namespace GettingStarted.Contracts
{
    public record GettingStartedRequest 
    {
        public string Request { get; init; }
    }
    
    public record GettingStartedResponse 
    {
        public string Response { get; init; }
    }
}