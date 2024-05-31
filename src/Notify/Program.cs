var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureApplicationInbox(builder.Configuration);
builder.Services.ConfigureSmsFeature(builder.Configuration);

builder.Services.ConfigureMediatR();
builder.Services.ConfigureBroker(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCarter();

builder.Services.Configure<AppSettings>(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapCarter();

app.Run();
