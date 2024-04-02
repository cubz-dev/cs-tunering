using cs_tunering;
using cs_tunering.Players;
using cs_tunering.Tournaments;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "CS Tournament For Friends",
            Version = "v1"
        }
        );

    var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

    var xmlCommentsPath = Path.Combine(baseDirectory, "cs-tunering.xml");

    c.IncludeXmlComments(xmlCommentsPath);
});

builder.Services.AddDbContext<DataContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// register endpoint extensions
app.TournamentsEndpoints();
app.PlayersEndpoints();

app.Run();
