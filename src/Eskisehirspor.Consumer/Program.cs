using Eskisehirspor.Application;
using Eskisehirspor.Application.UseCases.Feed.LatestThreads.JobConsumer;
using Eskisehirspor.Application.UseCases.Feed.LatestThreads.Worker;
using Eskisehirspor.Infrastructure;
using Eskisehirspor.Persistence;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructureLayer();
builder.Services.AddPersistenceLayer(builder.Configuration);
ConsumerDefines(builder.Services);

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

void ConsumerDefines(IServiceCollection services)
{

    services.AddMassTransit(x =>
    {
        //x.SetKebabCaseEndpointNameFormatter();


        //x.AddConsumer<SendRegistrationEmailConsumer>()
        //    .Endpoint(cfg => cfg.Name = "emailservice.registration");

        //x.AddConsumer<CreateOrUpdateThreadReactionConsumer>()
        //   .Endpoint(cfg => cfg.Name = "reactionservice.reaction");
        x.AddPublishMessageScheduler();
        x.AddConsumer<GetLatestTopicsConsumer>();
        x.UsingRabbitMq((context, cfg) =>
        {
            cfg.UsePublishMessageScheduler();
            cfg.ConfigureEndpoints(context);
        });
        services.AddHostedService<GetLatestTopicsWorker>();
    });

}
