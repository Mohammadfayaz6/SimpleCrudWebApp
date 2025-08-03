using Microsoft.EntityFrameworkCore;
using SimpleCrudWebApp.Implementation;
using SimpleCrudWebApp.Interface;
using SimpleCrudWebApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Add CORS service
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins("https://simple-crud-app-livid.vercel.app/") // frontend React URL
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<OrgDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));





builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IEmployeeRepositorycs, EmployeeRepository>();
// Assuming you have an ISalary interface and SalaryDetailsRepository implementation
builder.Services.AddScoped<ISalaryDetailsRepository, SalaryDetailsRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// Use CORS
app.UseCors("AllowFrontend");

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
