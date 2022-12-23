using BooksAPI;
using Microsoft.EntityFrameworkCore;

//builder to actually build web app
var builder = WebApplication.CreateBuilder(args);

//add services to builder
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BooksContext>(options => options.UseInMemoryDatabase("Books"));

//build the builder into the app
var app = builder.Build();

//adding endpoints
app.MapGet("/books", async (BooksContext db) => await MethodsAPI.GetAllBooks(db));
app.MapGet("/books/{id}", async (int id, BooksContext db) => await MethodsAPI.GetBookById(id, db));
app.MapPost("/books", async (Book book, BooksContext db) => await MethodsAPI.PostBook(book, db));
app.MapDelete("/books/{id}", async (int id, BooksContext db) => await MethodsAPI.DeleteBook(id, db));
app.MapPut("/books/{id}", async (int id, Book book, BooksContext db) => await MethodsAPI.UpdateBook(id, book, db));

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//redirects http requests to https
app.UseHttpsRedirection();


//run the app
app.Run();
