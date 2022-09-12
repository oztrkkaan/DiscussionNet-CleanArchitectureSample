using DiscussionNet.Application.UseCases.ThreadReactions.CreateOrUpdate.Publisher;
using DiscussionNet.Application.UseCases.ThreadReactions.CreateOrUpdate;
using MassTransit;
using MediatR;

namespace DiscussionNet.Application.UseCases.ThreadReactions.CreateOrUpdate.Consumer
{
    public class CreateOrUpdateThreadReactionConsumer : IConsumer<CreateOrUpdateThreadReactionPublisher>
    {
        private readonly IMediator _meidator;
        public CreateOrUpdateThreadReactionConsumer(IMediator meidator)
        {
            _meidator = meidator;
        }

        public async Task Consume(ConsumeContext<CreateOrUpdateThreadReactionPublisher> context)
        {
            await Task.Run(async () =>
            {
                await _meidator.Publish(new CreateOrUpdateThreadReactionEvent
                {
                    Reaction = context.Message.Reaction,
                    ThreadId = context.Message.ThreadId,
                    ReactedUserId = (int)context.Message.ReactedUserId

                });
            });
        }
    }
}
