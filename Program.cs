//builder to actually build web app
using BooksAPI;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//add services to builder
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BooksContext>(options => options.UseInMemoryDatabase("Books"));

//build the builder into the app
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//redirects http requests to https
app.UseHttpsRedirection();


//run the app
app.Run();
