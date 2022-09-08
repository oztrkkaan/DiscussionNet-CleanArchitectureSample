using Eskisehirspor.Application;
using Eskisehirspor.Application.UseCases.Email.RegistrationEmail.Consumer;
using Eskisehirspor.Application.UseCases.ThreadReactions.CreateOrUpdate.Consumer;
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
        x.AddConsumer<SendRegistrationEmailConsumer>()
            .Endpoint(cfg=>cfg.Name= "emailservice.registration");

        x.AddConsumer<CreateOrUpdateThreadReactionConsumer>()
           .Endpoint(cfg => cfg.Name = "reactionservice.reaction");
        x.UsingRabbitMq((context, cfg) =>
        {
            cfg.ConfigureEndpoints(context);
        });
    });
    services.AddMassTransitHostedService();
}
