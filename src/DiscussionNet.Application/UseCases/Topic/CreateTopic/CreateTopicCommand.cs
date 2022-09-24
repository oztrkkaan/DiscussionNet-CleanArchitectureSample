using DiscussionNet.Application.Common.Interfaces;
using DiscussionNet.Application.UseCases.Thread.CreateThread;
using DiscussionNet.Domain.Entities;
using MediatR;
using System.Transactions;

namespace DiscussionNet.Application.UseCases.Topic.CreateTopic
{
    public class CreateTopicCommand : IRequest<CreateTopicResponse>
    {
        public string Subject { get; set; }
        public string ThreadContent { get; set; }
        public List<Tag> Tags { get; set; }
    }
    public class CreateTopicCommandHandler : IRequestHandler<CreateTopicCommand, CreateTopicResponse>
    {
        IForumDbContext _context;
        IMediator _mediator;
        public CreateTopicCommandHandler(IForumDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<CreateTopicResponse> Handle(CreateTopicCommand request, CancellationToken cancellationToken)
        {
            throw new Exception("lalal");

            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            ThrowIfTopicAlreadyExist(request.Subject);

            var topic = await CreateTopic(request.Subject, cancellationToken);
            await CreateThread(request.ThreadContent, topic.Id);

            scope.Complete();

            return new CreateTopicResponse
            {
                Subject = topic.Subject,
                UrlName = topic.UrlName
            };
        }

        private async Task<Domain.Entities.Topic> CreateTopic(string subject, CancellationToken cancellationToken)
        {
            var topic = new Domain.Entities.Topic(subject);
            await _context.Topics.AddAsync(topic);
            await _context.SaveChangesAsync(cancellationToken);

            return topic;
        }

        private async Task<CreateThreadResponse> CreateThread(string threadContent, int topicId)
        {
            var createThreadCommand = new CreateThreadCommand
            {
                Content = threadContent,
                TopicId = topicId
            };

            return await _mediator.Send(createThreadCommand);
        }

        private void ThrowIfTopicAlreadyExist(string subject)
        {
            var isSUbjectExist = _context.Topics.Any(m => m.Subject == subject);
            if (isSUbjectExist)
            {
                throw new Exception("This topic is already exist");
            }
        }
    }

    public class CreateTopicResponse
    {
        public string Subject { get; set; }
        public string UrlName { get; set; }
    }


}
