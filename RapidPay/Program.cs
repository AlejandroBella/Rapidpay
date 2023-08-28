using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using RapidPay.Business.Entities;
using RapidPay.Business.Services;
using RapidPay.Data;
using RapidPay.Data.Interfaces;
using RapidPay.Data.Repositories;
using RapidPay.Mappings;
using RapidPay.View.Entities;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//services
builder.Services.AddScoped<DataServiceBase<CardView, string>, CreditCardService>();
builder.Services.AddScoped<DataServiceBase<CardHolderView, string>, BalanceService>();

//repositories
builder.Services.AddScoped<IRepository<Card, string>,CreditCardRepository>();
builder.Services.AddScoped<IRepository<CardHolder, string>, CardHolderRepository>();

// Auto Mapper Configurations
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddDbContext<Database>(options =>
{
    var config = builder.Configuration;
    var connectionString = config.GetConnectionString("database");

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthentication();
//app.UseAuthorization();

app.MapControllers();

app.Run();
