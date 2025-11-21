using API.Middlewares;
using Application;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Persistence;
using Persistence.data;
using Persistence.Identity;
using System;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureApplicationServices();
builder.Services.ConfigureInfrastructureServices(builder.Configuration);
builder.Services.ConfigurePersistenceServices(builder.Configuration);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddHttpContextAccessor();

//builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
//    .AddEntityFrameworkStores<ApplicationDbContext>()
//    .AddDefaultTokenProviders();   // This will not work, it will return 404 on every request
builder.Services.AddIdentityCore<ApplicationUser>()
    .AddRoles<ApplicationRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();



// Add CORS
builder.Services.AddCors(o => o.AddPolicy("CorsPolicy", corsBuilder =>
{
    corsBuilder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
}));

builder.Services.AddApiVersioning(options=>
{
    options.AssumeDefaultVersionWhenUnspecified=true;
    options.DefaultApiVersion=new ApiVersion(1,0);
    options.ApiVersionReader=new UrlSegmentApiVersionReader();
})
    .AddMvc()
    .AddApiExplorer(options =>
{
    options.GroupNameFormat="'v'V";
    options.SubstituteApiVersionInUrl=true;
});






var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();     // ✅ Swagger middleware
    app.UseSwaggerUI(o =>
    {
        var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
        foreach (var d in provider.ApiVersionDescriptions)
        {
            o.SwaggerEndpoint($"/swagger/{d.GroupName}/swagger.json", d.GroupName.ToUpperInvariant());
        }
    });   // ✅ Swagger UI middleware
}


app.UseGlobalExceptionMiddleware();
app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthentication();

app.UseAuthorization();
app.MapControllers();

app.Run();
