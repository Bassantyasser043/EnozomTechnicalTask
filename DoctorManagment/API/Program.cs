
using Repositories.Context;
using Repositories.Rep;
using Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MyContext>();
builder.Services.AddEndpointsApiExplorer();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Register Dependencies
builder.Services.AddScoped<IEntityConfiguration, EntityConfiguration>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IDoctorTimeAvailabilityRepository, DoctorTimeAvailabilityRepository>();
builder.Services.AddScoped<ITimeRangeRepository, TimeRangeRepository>();

builder.Services.AddScoped<IDoctorServices, DoctorServices>();
builder.Services.AddScoped<IDoctorAvailabilityService, DoctorAvailabilityService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
