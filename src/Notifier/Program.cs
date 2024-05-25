using MassTransit;
using Microsoft.EntityFrameworkCore;
using Notifier;
using Notifier.Common.InBox;
using Notifier.Common.Persistence;
using Notifier.Features.Sms;
using Notifier.Features.Sms.Providers;
using PayamakCore;

var builder = WebApplication.CreateBuilder(args);

var appSettings = builder.Configuration.Get<AppSettings>();
builder.Services.AddDbContext<NotifierDbContext>(options =>
{

    if (appSettings is null)
    {
        throw new ArgumentNullException(nameof(appSettings));
    }

    var svcDbContext = appSettings.SvcDbContext;
    options.UseMongoDB(svcDbContext.Host, svcDbContext.DatabaseName);
});

builder.Services.AddMassTransit(configure =>
{
    if (appSettings is null)
    {
        throw new ArgumentNullException(nameof(appSettings));
    }

    var brokerConfig = appSettings.BrokerConfiguration;

    configure.AddConsumers(typeof(IAssemblyMarker).Assembly);

    configure.UsingRabbitMq((context, cfg) =>
    {
        cfg.UseRawJsonDeserializer();

        cfg.Host(brokerConfig.Host, hostConfigure =>
        {
            hostConfigure.Username(brokerConfig.Username);
            hostConfigure.Password(brokerConfig.Password);
        });

        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddMediatR(configure =>
{
    var assembly = typeof(IAssemblyMarker).Assembly;
    configure.RegisterServicesFromAssembly(assembly);
});


builder.Services.AddKeyedScoped<ISmsProvider, FarapayamakSmsProvider>("Farapayamak");
builder.Services.AddKeyedScoped<ISmsProvider, KaveNegarSmsProvider>("KavehNegar");
builder.Services.AddScoped<SmsService>();
builder.Services.AddScoped<InboxService>();

builder.Services.AddHostedService<InquirySmsBackgroundService>();
builder.Services.AddHostedService<InboxProcessBackgroundSerice>();

builder.Services.AddPayamakService();

builder.Services.Configure<AppSettings>(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
