using System.Reflection;
using System.Text.Json.Serialization;
using Books.Service.Core.Interfaces;
using Books.Service.Core.Startup;
using Books.Service.Infrastructure.Startup;
using Books.Service.Web.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using static System.Net.Mime.MediaTypeNames;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddCoreServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

// Configure controllers to return errors on invalid model
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context => {
            return new BadRequestObjectResult(
                new BadRequestResponse(context.ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)))
                {
                    ContentTypes = { Application.Json } 
                };
            };
    })
    .AddJsonOptions(options => 
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
    
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>{ 
    options.SwaggerDoc("v1", new OpenApiInfo {
        Version = "1.0.0",
        Title = "Books API",
        Description = "API that manages a collection of books in a fictional store",
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    options.EnableAnnotations();
    options.CustomSchemaIds(type => type.Name.EndsWith("Dto") ? type.Name.Replace("Dto", string.Empty) : type.Name);
});

builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.DefaultModelsExpandDepth(1));
}

builder.Services.AddLogging();

// Seed database
var bookRepository = app.Services.GetService<IBookRepository>()!;
var seedLogger = app.Services.GetService<ILogger<DbInitializer>>()!;
var dbInitialiser = new DbInitializer(bookRepository, seedLogger);

await dbInitialiser.Seed();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

//Add program class to make program.cs visible to other projects
public partial class Program { }