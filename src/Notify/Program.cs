var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureApplicationInbox(builder.Configuration);
builder.Services.ConfigureSmsFeature(builder.Configuration);
builder.Services.ConfigureEmailFeature(builder.Configuration);
builder.Services.Configure<AppSettings>(builder.Configuration);

builder.Services.ConfigureMediatR();
builder.Services.ConfigureBroker(builder.Configuration);
builder.Services.AddCarter();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapCarter();

app.Run();
