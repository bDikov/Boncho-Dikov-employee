using AutoMapper;
using InfrastructureOrchestrator.Infrastructure.Mapper;
using InfrastructureOrchestrator.Ochestrate.Interfaces;
using InfrastructureOrchestrator.Ochestrate.Services;
using PE.BusinessLogic.Interfaces;
using PE.BusinessLogic.Services;
using PE.Common.Interfaces;
using PE.Repository.Interfaces;
using PE.Repository.Services;
using PE.Serializers.Csv;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<ICsvSerializer, CsvHelperSerializer>();
builder.Services.AddTransient<IProcessFileData, ProcessFileData>();
builder.Services.AddSingleton<IMapper>(new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()).CreateMapper());
builder.Services.AddTransient<IRepository, Repository>();
builder.Services.AddTransient<IPairingService, PairingService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();