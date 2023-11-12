using Microsoft.AspNetCore.Mvc;
using DL.UnitOfWork;
using DL.SachDL;
using BL.SachBL;
using DL.NguoiDungDL;
using BL.NguoiDungBL;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
                      });
});

var connectionString = builder.Configuration["ConnectionStrings"];

builder.Services.AddScoped<IUnitOfWork>(provider => new UnitOfWork(connectionString));

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
// Add services to the container.

builder.Services.AddScoped<ISachDL, SachDL>();
builder.Services.AddScoped<ISachBL, SachBL>();
builder.Services.AddScoped<INguoiDungDL, INguoiDungDL>();
builder.Services.AddScoped<INguoiDungBL, NguoiDungBL>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
