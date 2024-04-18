using ActorsWebApplication.Data.Interfaces;
using ActorsWebApplication.Data;
using ActorsWebApplication.DataProvidors;
using Microsoft.EntityFrameworkCore;
using ActorsWebApplication.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IActorDataProvider, IMDbDataProvider>();
builder.Services.AddScoped<IActorRepository, ActorRepository>();
builder.Services.AddDbContext<ApiContext>(options => options.UseInMemoryDatabase(databaseName: "ActorsDb"));
builder.Services.AddHostedService<DataLoadHostedService>();
builder.Services.AddTransient<IActorsService, ActorsService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
