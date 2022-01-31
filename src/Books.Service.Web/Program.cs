using System.Reflection;
using Books.Service.Infrastructure;
using Books.Service.Web.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructureServices(builder.Configuration);

// Return error message on invalid model
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();