using Eskisehirspor.Application.Common.Interfaces;
using MediatR;

namespace Eskisehirspor.Application.UseCases.Thread.CreateThread
{
    public class CreateThreadCommand : IRequest<CreateThreadResponse>
    {
        public string Content { get; set; }
        public int TopicId { get; set; }
    }

    public class CreateThreadCommandHandler : IRequestHandler<CreateThreadCommand, CreateThreadResponse>
    {
        IForumDbContext _context;
        IIdentityManager _identityManager;

        public CreateThreadCommandHandler(IForumDbContext context, IIdentityManager identityManager)
        {
            _context = context;
            _identityManager = identityManager;
        }

        public async Task<CreateThreadResponse> Handle(CreateThreadCommand request, CancellationToken cancellationToken)
        {

            var user = GetUserById(_identityManager.User.Id);
            var topic = GetTopicById(request.TopicId);

            var thread = new Domain.Entities.Thread(request.Content, topic, user);

            await _context.Threads.AddAsync(thread);
            await _context.SaveChangesAsync(cancellationToken);

            return new CreateThreadResponse
            {
                Id = thread.Id
            };
        }

        private Domain.Entities.User GetUserById(int userId)
        {
            var user = _context.Users.FirstOrDefault(m => m.Id == userId);
            ArgumentNullException.ThrowIfNull(user);

            return user;
        }
        private Domain.Entities.Topic GetTopicById(int topicId)
        {
            var topic = _context.Topics.FirstOrDefault(m => m.Id == topicId);
            ArgumentNullException.ThrowIfNull(topic);

            return topic;
        }
    }

    public class CreateThreadResponse
    {
        public int Id { get; set; }
    }
}
