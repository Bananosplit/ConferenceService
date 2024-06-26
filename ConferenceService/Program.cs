using App.BidServices;
using Asp.Versioning;
using ConferenceService.Utils;
using DAL.Repositories;
using Domain.interfaces.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddControllers();

builder.Services.AddApiVersioning( opt =>
{
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.DefaultApiVersion = new ApiVersion(1,0);
    opt.ReportApiVersions = true;
    opt.ApiVersionReader = new UrlSegmentApiVersionReader();

}).AddApiExplorer(opt =>
{
    opt.GroupNameFormat = "'v'V";
    opt.SubstituteApiVersionInUrl = true;
});

builder.Services.AddSwaggerGen();

builder.Services.AddConferenceDbContext(builder.Configuration);

builder.Services.AddScoped<IBidRepository, BidRepository>();
builder.Services.AddScoped<BidServices>();


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

