using System.Threading.Tasks;
using MassTransit;

namespace GettingStarted.Consumers
{
    public class GettingStartedRequestConsumer :
        IConsumer<Contracts.GettingStartedRequest>
    {
        public async Task Consume(ConsumeContext<Contracts.GettingStartedRequest> context)
        {
            await context.RespondAsync(new Contracts.GettingStartedResponse{ Response = $"Responded to: {context.Message.Request}" });
        }
    }
}