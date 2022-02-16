global using codingAssigment.Data;
using Microsoft.EntityFrameworkCore;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
	options.AddPolicy(name: MyAllowSpecificOrigins,
		builder =>
		{
			//builder.WithOrigins("https://localhost:7006", "https://localhost:5032");
			builder.SetIsOriginAllowed(origin =>
				new Uri(origin).Host == "localhost" &&
				(new Uri(origin).Port == 7006 || new Uri(origin).Port == 5032)
			)
			.AllowAnyHeader()
			.AllowAnyMethod();
		});
});

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddDbContext<DataContext>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var db = scope.ServiceProvider.GetRequiredService<DataContext>();
	db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
