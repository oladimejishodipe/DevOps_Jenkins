using MeMawKNowsBestCrime.MeMawKnowsBestCrimeData;
using Microsoft.EntityFrameworkCore;
using OpenAIApp.Configurations;
using OpenAIApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<OpenAiConfig>(builder.Configuration.GetSection("OpenAI"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MeMawKnowsBestCrimeDataContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("CrimeDataConnectionString")));

builder.Services.AddScoped<IOpenAiService, OpenAiService>();
/*
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});
*/
var app = builder.Build();
/*
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
*/
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
