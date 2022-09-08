using Eskisehirspor.Application.UseCases.Email.RegistrationEmail.Publisher;
using MassTransit;
using MediatR;

namespace Eskisehirspor.Application.UseCases.Email.RegistrationEmail.Consumer
{
    public class SendRegistrationEmailConsumer : IConsumer<SendRegistrationEmailPublisher>
    {
        private readonly IMediator _mediator;
        public SendRegistrationEmailConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task Consume(ConsumeContext<SendRegistrationEmailPublisher> context)
        {
            await Task.Run(async () =>
            {
                await _mediator.Publish(new SendRegistrationEmailEvent
                {
                    Email = context.Message.Email,
                    Username = context.Message.Username,
                    DisplayName = context.Message.DisplayName
                });
            });
        }
    }
}
