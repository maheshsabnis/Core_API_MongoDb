using Core_API_MongoDb.Models;
using Core_API_MongoDb.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

/*
 the configuration instance to which the appsettings.json file's DatabaseSettings section binds is registered in the Dependency Injection (DI) container. 
 */


builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("DatabaseSettings"));

builder.Services.AddSingleton<NovelService>();

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

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();


app.MapGet("/novels", async (NovelService serv) => 
{
    var novels = await serv.GetAsync();
    return Results.Ok<List<Novel>>(novels); 
});
app.MapGet("/novels/{id}", async (NovelService serv, string id) =>
{
    var novel = await serv.GetAsync(id);
    return Results.Ok<Novel>(novel);
});

app.MapPost("/novels", async (NovelService serv, Novel novel) =>
{
     await serv.CreateAsync(novel);
    return Results.Ok("Record Added Successfully");
});

app.MapPut("/novels/{id}", async (NovelService serv, string id, Novel novel) =>
{
    await serv.UpdateAsync(id,novel);
    return Results.Ok("Record Updated Successfully");
});

app.MapDelete("/novels/{id}", async (NovelService serv, string id) =>
{
    await serv.RemoveAsync(id);
    return Results.Ok("Record Deleted Successfully");
});


app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
