using DiscussionNet.Application;
using DiscussionNet.Application.Common.Hangfire;
using DiscussionNet.Application.UseCases.Email.RegistrationEmail.Consumer;
using DiscussionNet.Application.UseCases.Feed.RefreshLatestTopics.Consumer;
using DiscussionNet.Application.UseCases.Notification.ReactionNotification.Consumer;
using DiscussionNet.Application.UseCases.ThreadReactions.CreateOrUpdate.Consumer;
using DiscussionNet.Infrastructure;
using DiscussionNet.Persistence;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructureLayer();
builder.Services.AddPersistenceLayer(builder.Configuration);

var hangfireConfiguration = builder.Services.BuildServiceProvider().GetService<IHangfireConfiguration>();
hangfireConfiguration.Configure(builder.Services);

ConsumerDefines(builder.Services);

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
hangfireConfiguration.ConfigureDashboard(app);
hangfireConfiguration.InitializeJobs();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

void ConsumerDefines(IServiceCollection services)
{
    services.AddMassTransit(x =>
    {
        x.SetKebabCaseEndpointNameFormatter();

        x.AddConsumer<SendRegistrationEmailConsumer>()
            .Endpoint(cfg => cfg.Name = "emailservice.registration");

        x.AddConsumer<CreateOrUpdateThreadReactionConsumer>()
           .Endpoint(cfg => cfg.Name = "reactionservice.reaction");
       
        x.AddConsumer<RefreshLatestTopicsConsumer>()
           .Endpoint(cfg => cfg.Name = "jobs.refresh-latest-topics");

        x.AddConsumer<ReactionNotificationConsumer>()
           .Endpoint(cfg => cfg.Name = "notificationservice.reaction-notifications");

        x.UsingRabbitMq((context, cfg) =>
        {
            cfg.ConfigureEndpoints(context);
        });
    });
}
