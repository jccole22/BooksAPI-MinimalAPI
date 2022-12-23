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
app.MapGet("/books", async (BooksContext db) => await db.Books.ToListAsync());
app.MapGet("/books/{id}", async (int id, BooksContext db) =>
{
    var book = await db.Books.FindAsync(id);

    return book != null ? Results.Ok(book) : Results.NotFound();

});
app.MapPost("/books", async (Book book, BooksContext db) =>
{
    db.Books.Add(book);
    await db.SaveChangesAsync();

    return Results.Created($"/books/{book.Id}", book);
});
app.MapDelete("/books/{id}", async (int id, BooksContext db) =>
{
    var book = await db.Books.FindAsync(id);
    if (book != null)
    {
        db.Books.Remove(book);
        db.SaveChanges();

        return Results.NoContent();
    }
    else
    {
        return Results.NotFound();
    }
});
app.MapPut("/books/{id}", async (int id, Book book, BooksContext db) =>
{
    //could also use db.Books.Update(book); and wouldnt need following line and all the manual updating below
    var bookInDb = await db.Books.FindAsync(id);

    if (bookInDb != null)
    {
        //Updating each property manually
        //could also only update the needed ones
        bookInDb.Title = book.Title;
        bookInDb.Author = book.Author;
        bookInDb.Genre = book.Genre;
        bookInDb.Publisher = book.Publisher;
        bookInDb.PublicationDate = book.PublicationDate;
        db.SaveChanges();

        return Results.Ok(bookInDb);
    }
    else
    {
        return Results.NotFound();
    }

});


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//redirects http requests to https
app.UseHttpsRedirection();


//run the app
app.Run();
