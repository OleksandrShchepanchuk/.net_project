using System.Text;
using Microsoft.EntityFrameworkCore;
using Hospital.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
//using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
	.AddJsonOptions(options =>
	{
		options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
	});

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAll",
		builder =>
		{
			builder.AllowAnyOrigin()
				.AllowAnyMethod()
				.AllowAnyHeader();
		});
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
	AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			ValidIssuer = builder.Configuration["Jwt:Issuer"],
			ValidAudience = builder.Configuration["Jwt:Audience"],
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))

		};
	});


builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<HospitalContext>(
    o => o.UseNpgsql(builder.Configuration.GetConnectionString("HospitalDB")));

// builder.Services.AddIdentity<User, Role>()
// 	.AddEntityFrameworkStores<HospitalContext>()
// 	.AddDefaultTokenProviders();
var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseCors("AllowAll");


app.UseHttpsRedirection();

////  Directory for images
var uploadDirectory = Path.Combine(app.Environment.ContentRootPath, "uploads");
Directory.CreateDirectory(uploadDirectory);

app.UseFileServer(new FileServerOptions
{
    FileProvider = new PhysicalFileProvider(uploadDirectory),
    RequestPath = "/uploads",
    EnableDirectoryBrowsing = true
});
/////????

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
