using ConferenceService.Utils;
using DAL.Repositories;
using Domain.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddConferenceDbContext(builder.Configuration);

builder.Services.AddScoped<IBidRepository, BidRepository>();


var app = builder
    .Build()
    .MigrateDataBase();

if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();


app.Run();

