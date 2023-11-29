//the main settings
//where we wire in functionalities
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


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

