using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.WebHost.ConfigureKestrel((context, options) =>
{
    {
        // Handle requests up to 320 MB
        options.Limits.MaxRequestBodySize = 335544320;
    }
});

builder.Services.Configure<FormOptions>(x =>
{
    x.ValueLengthLimit = 335544320;
    x.MultipartBodyLengthLimit = 335544320; // In case of multipart
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.UseAuthorization();

app.MapControllers();

app.Run();