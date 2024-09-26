using System.Text.Json.Serialization;
using Microsoft.Extensions.FileProviders;
using QMan.Infrastructure.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddControllers().AddJsonOptions(options=>options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddResponseCompression();
builder.Services.AddResponseCaching();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"uploads")),
    RequestPath = new PathString("/uploads"),
    OnPrepareResponse = context =>
    {
        context.Context.Response.Headers.Append("Access-Control-Allow-Origin", "*");
    }
    
});

app.MapControllers();
app.UseResponseCaching();
app.UseResponseCompression();
app.UseAuthentication();
app.Run();
