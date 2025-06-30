using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

partial class BookControl // Added 'partial' modifier
{
    public class LibraryContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseMySQL("server=localhost;database=library_db;user=root;password=ARGJENTINA.12!");
    }

    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty; // Default value added
        public string Author { get; set; } = string.Empty; // Default value added
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

    static void Main(string[] args) // Changed signature to include 'string[] args'
    {
        Program program = new Program(); // Create an instance of Program
        program.ListAllBooksInstance(); // Call the renamed instance method
    }

    private void ListAllBooksInstance() // Renamed to avoid conflict
    {
        using var context = new LibraryContext();

        Console.WriteLine("Books in Library:");
        var books = context.Books.ToList();

        foreach (var book in books)
        {
            Console.WriteLine($"{book.Id} - {book.Title} by {book.Author}, Qty: {book.Quantity}, Price: ${book.Price}");
        }

        Console.WriteLine("\nBooks with quantity > 5:");
        var filteredBooks = context.Books.Where(b => b.Quantity > 5).ToList();
        foreach (var book in filteredBooks)
        {
            Console.WriteLine($"{book.Title} (Qty: {book.Quantity})");
        }
    }
}

