using Microsoft.EntityFrameworkCore;
using Quartz;
using QuartzJob_Scheduler.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataContext>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddQuartz(q =>
{
    q.UsePersistentStore(options =>
    {
        //options.UseProperties = true;
        options.UseNewtonsoftJsonSerializer();
        options.UseMySql(o =>
        {
            o.ConnectionString = "server=localhost; database=quartz-scheduler; user=root; password=123456";
        });
    });
});

builder.Services.AddQuartzHostedService(options =>
{
    options.WaitForJobsToComplete = true;
});

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