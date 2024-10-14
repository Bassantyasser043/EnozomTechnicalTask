using DoctorAvailabiltity.Data.Context;
using DoctorAvailabiltity.DoctorManagment.Services;
using DoctorAvailabiltity.TimeAvailabilityManagment.Repository;
using DoctorAvailabiltity.TimeAvailabilityManagment.Services;
using DoctorManagment.Repository.DoctorRepository;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MyContext>();
builder.Services.AddEndpointsApiExplorer();

//Swagger 
builder.Services.AddSwaggerGen(
    options =>
    {
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        options.IncludeXmlComments(xmlPath);
    }
    );

//Register Dependencies 
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IDoctorTimeAvailabilityRepository, DoctorTimeAvailabilityRepository>();
builder.Services.AddScoped<ITimeRangeRepository, TimeRangeRepository>();

builder.Services.AddScoped<IDoctorServices, DoctorServices>();
builder.Services.AddScoped<IDoctorAvailabilityService, DoctorAvailabilityService>();

//add Controllers
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Enables the Swagger endpoint
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapControllers();

app.Run();


