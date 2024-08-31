using DiscussionNet.Application.Features.CommentReactions.CreateOrUpdate.Publisher;
using MassTransit;
using MediatR;

namespace DiscussionNet.Application.Features.CommentReactions.CreateOrUpdate.Consumer
{
    public class CreateOrUpdateCommentReactionConsumer : IConsumer<CreateOrUpdateCommentReactionPublisher>
    {
        private readonly IMediator _meidator;
        public CreateOrUpdateCommentReactionConsumer(IMediator meidator)
        {
            _meidator = meidator;
        }

        public async Task Consume(ConsumeContext<CreateOrUpdateCommentReactionPublisher> context)
        {
            await _meidator.Publish(new CreateOrUpdateCommentReactionEvent
            {
                Reaction = context.Message.Reaction,
                CommentId = context.Message.CommentId,
                ReactedUserId = (int)context.Message.ReactedUserId
            });
        }
    }
}
