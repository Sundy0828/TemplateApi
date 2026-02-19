using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using Microsoft.AspNetCore.Mvc;
using TemplateApi.Attributes;
using TemplateApi.Services;
using TemplateApi.Dao;
using TemplateApi.Dao.Interfaces;
using TemplateApi.Domains;
using TemplateApi.Domains.Interfaces;

Console.WriteLine($"Mongo Template...");
var builder = WebApplication.CreateBuilder(args);
try
{
    // MongoDB
    var mongoClient = new MongoClient(builder.Configuration["MongoDb:ConnectionString"]);
    var mongoDatabase = mongoClient.GetDatabase(builder.Configuration["MongoDb:DatabaseName"]);
    builder.Services.AddSingleton(mongoDatabase);

    // Domains
    builder.Services.AddScoped<IBasicDomain, BasicDomain>();

    // Dao
    builder.Services.AddScoped<IBasicDao, BasicDao>();

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.ApiKey,
        In = ParameterLocation.Header,
        Name = "X-Api-Key",
        Description = "API Key Authentication"
    });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "ApiKey"
                    }
                },
                Array.Empty<string>()
            }
        });
    });

    if (builder.Environment.IsDevelopment())
    {
        builder.Services.AddCors(options => options.AddPolicy("DevCors", policy => policy.WithOrigins("http://localhost:3000")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()));
    }

    var app = builder.Build();

    // Global Exception Handling Middleware
    app.UseExceptionHandler(appBuilder =>
    {
        appBuilder.Run(async context =>
        {
            var feature = context.Features.Get<IExceptionHandlerFeature>();
            var ex = feature?.Error;

            var statusCode = ex switch
            {
                ArgumentException => StatusCodes.Status400BadRequest,
                KeyNotFoundException => StatusCodes.Status404NotFound,
                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                OperationCanceledException => StatusCodes.Status408RequestTimeout,
                _ => StatusCodes.Status500InternalServerError
            };

            context.Response.StatusCode = statusCode;

            var problem = new ProblemDetails
            {
                Status = statusCode,
                Title = ex?.Message ?? "An unexpected error occurred.",
                Type = $"https://httpstatuses.com/{statusCode}",
                Instance = context.Request.Path
            };

            await context.Response.WriteAsJsonAsync(problem);
        });
    });

    if (app.Environment.IsDevelopment())
    {
        app.UseCors("DevCors");
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine($"Error!\n" + ex.ToString());
    Environment.Exit(1);
}
