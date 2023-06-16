using FoodMessages.Interfaces;
using FoodMessages.Messenger;
using FoodMessages.Models;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddHttpsRedirection(options =>
//{
//    options.RedirectStatusCode = (int)HttpStatusCode.PermanentRedirect;
//    options.HttpsPort = 80;
//});

builder.Services.AddTransient<IMessenger, MessengerTwillio>();
builder.Services.Configure<ConfigTwillio>(builder.Configuration.GetSection("ConfigTwillio"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseCors(builder => builder.AllowAnyOrigin()
//                       .AllowAnyMethod()
//                       .AllowAnyHeader());
//app.UseHsts();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
