using Eskisehirspor.Application;
using Eskisehirspor.Application.Common.Hangfire;
using Eskisehirspor.Application.UseCases.Email.RegistrationEmail.Consumer;
using Eskisehirspor.Application.UseCases.Feed.LatestThreads;
using Eskisehirspor.Application.UseCases.Feed.LatestThreads.Consumer;
using Eskisehirspor.Application.UseCases.Feed.LatestThreads.Publisher;
using Eskisehirspor.Application.UseCases.ThreadReactions.CreateOrUpdate.Consumer;
using Eskisehirspor.Infrastructure;
using Eskisehirspor.Persistence;
using Hangfire;
using MassTransit;
using MediatR;
using System.Reflection;

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

        x.AddConsumer<GetLatestTopicsConsumer>()
        .Endpoint(cfg => cfg.Name = "jobs.get-latest-topics");

        x.UsingRabbitMq((context, cfg) =>
        {
            cfg.ConfigureEndpoints(context);
        });
    });
}
