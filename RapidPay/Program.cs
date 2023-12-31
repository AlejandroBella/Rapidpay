using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using RapidPay.Business.Entities;
using RapidPay.Business.Services;
using RapidPay.Data;
using RapidPay.Mappings;
using RapidPay.View.Entities;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//services
builder.Services.AddAuthentication("Bearer")
           .AddJwtBearer("Bearer", options =>
           {
               options.Authority = "https://localhost:5001";

               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateAudience = false
               };
           });

builder.Services.AddScoped<DataServiceBase<Card, string>, CardService>();


// Auto Mapper Configurations
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});


IMapper MapperService = mapperConfig.CreateMapper();
builder.Services.AddSingleton(MapperService);

builder.Services.AddDbContext<Database>(options =>
{
    var config = builder.Configuration;
    var connectionString = config.GetConnectionString("database");

});

builder.Services.AddScoped<DbContext, Database>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
