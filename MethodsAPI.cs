using Microsoft.EntityFrameworkCore;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BooksAPI
{
    public class MethodsAPI
    {

        public static async Task<List<Book>> GetAllBooks(BooksContext db)
        {
            return await db.Books.ToListAsync();
        }

        public static async Task<IResult> GetBookById (int id, BooksContext db)
        {
            var book = await db.Books.FindAsync(id);

            return book != null ? Results.Ok(book) : Results.NotFound();
        }

        public static async Task<IResult> PostBook(Book book, BooksContext db)
        {
            db.Books.Add(book);
            await db.SaveChangesAsync();

            return Results.Created($"/books/{book.Id}", book);
        }

        public static async Task<IResult> DeleteBook(int id, BooksContext db)
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
        }

        public static async Task<IResult> UpdateBook(int id, Book book, BooksContext db)
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
        }
    }
}
