using AddessoCase.Service.Validations;
using AdessoCase.API.Filters;
using AdessoCase.API.Middlewares;
using AdessoCase.API.Modules;
using AdessoCase.Core.Repositories;
using AdessoCase.Core.Services;
using AdessoCase.Core.UnitOfWorks;
using AdessoCase.Repository;
using AdessoCase.Repository.Repositories;
using AdessoCase.Repository.UnitOfWorks;
using AdessoCase.Service.Mapping;
using AdessoCase.Service.Services;
using AdessoCase.Service.Validations;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NetCore.AutoRegisterDi;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();

builder.Services.AddScoped(typeof(NotFoundFilter<>));
builder.Services.AddAutoMapper(typeof(MapProfile));

#region Add api versioning & Api explorer
builder.Services.AddApiVersioning(e =>
{
    e.DefaultApiVersion = new ApiVersion(1, 0);
    e.AssumeDefaultVersionWhenUnspecified = true;
    e.ReportApiVersions = true;
});
builder.Services.AddVersionedApiExplorer(e =>
{
    e.DefaultApiVersion = new ApiVersion(1, 0);
    e.AssumeDefaultVersionWhenUnspecified = true;
    e.GroupNameFormat = "'v'V";
});
#endregion

builder.Services.AddControllers(options => 
    options.Filters.Add(new ValidateFilterAttribute()))
        .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<TravelDtoValidator>());

builder.Services.AddControllers(options =>
    options.Filters.Add(new ValidateFilterAttribute()))
        .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<TravelFilterDtoValidator>());

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;

});

builder.Services
.AddDbContext<AppDbContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Host.UseServiceProviderFactory
    (new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new RepoServiceModule()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();
app.UseCustomException();

app.UseAuthorization();

app.MapControllers();

app.Run();
