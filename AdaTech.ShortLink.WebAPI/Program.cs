using AdaTech.ShortLink.DataLibrary;
using AdaTech.ShortLink.Service;
using AdaTech.ShortLink.Service.AttributeTags;
using AdaTech.ShortLink.WebAPI.Handlers;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Link Shortener", Version = "v1" });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

    c.TagActionsBy(api =>
    {
        if (api.GroupName != null)
        {
            return new[] { api.GroupName };
        }

        var controllerActionDescriptor = api.ActionDescriptor as ControllerActionDescriptor;
        if (controllerActionDescriptor != null)
        {
            var displayNameAttribute = controllerActionDescriptor.ControllerTypeInfo
                .GetCustomAttributes(typeof(SwaggerDisplayNameAttribute), true)
                .FirstOrDefault() as SwaggerDisplayNameAttribute;

            if (displayNameAttribute != null)
            {
                return new[] { displayNameAttribute.DisplayName };
            }
        }

        return new[] { api.ActionDescriptor.RouteValues["controller"] };
    });
});

builder.Services.AddScoped<LinkService>();
builder.Services.AddScoped<ApplicationContext>();

builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ApplicationContext")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOriginPolicy",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAnyOriginPolicy");

app.UseMiddleware<HandlerException>();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
