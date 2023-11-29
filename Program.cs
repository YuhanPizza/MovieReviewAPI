//the main settings
//where we wire in functionalities
using Microsoft.EntityFrameworkCore;
using MovieReviewApp.Data;
using PokemonReviewApp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient<Seed>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

if (args.Length == 1 && args[0].ToLower() == "seeddata")
	SeedData(app);

void SeedData(IHost app)
{
	var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

	using (var scope = scopedFactory.CreateScope())
	{
		var service = scope.ServiceProvider.GetService<Seed>();
		service.SeedDataContext();
	}
}

//Middle Wear ---------------------------//
//whenever you pass a http request it goes through here
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
//-------------------------------------//

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

