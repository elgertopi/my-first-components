using System;
using MySql.Data.MySqlClient;

class BookService
{
    static string connectionString = "server=localhost;user=root;password=Argjentina.12!;database=library_db";

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\nLibrary Management Console");
            Console.WriteLine("1. Add Book");
            Console.WriteLine("2. Update Book");
            Console.WriteLine("3. Delete Book");
            Console.WriteLine("4. List All Books");
            Console.WriteLine("5. Exit");
            Console.Write("Choose option: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    AddBook();
                    break;
                case "2":
                    UpdateBook();
                    break;
                case "3":
                    DeleteBook();
                    break;
                case "4":
                    ListAllBooks();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        }
    }

    static void AddBook()
    {
        Console.Write("Title: ");
        string title = Console.ReadLine() ?? "";
        Console.Write("Author: ");
        string author = Console.ReadLine() ?? "";
        Console.Write("Quantity: ");
        int quantity = int.Parse(Console.ReadLine() ?? "0");
        Console.Write("Price: ");
        decimal price = decimal.Parse(Console.ReadLine() ?? "0");

        using var connection = new MySqlConnection(connectionString);
        connection.Open();

        using var cmd = new MySqlCommand("AddBook", connection);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("p_title", title);
        cmd.Parameters.AddWithValue("p_author", author);
        cmd.Parameters.AddWithValue("p_quantity", quantity);
        cmd.Parameters.AddWithValue("p_price", price);
        cmd.ExecuteNonQuery();

        Console.WriteLine("Book added successfully.");
    }

    static void UpdateBook()
    {
        Console.Write("Book ID to update: ");
        int id = int.Parse(Console.ReadLine() ?? "0");
        Console.Write("New Title: ");
        string title = Console.ReadLine() ?? "";
        Console.Write("New Author: ");
        string author = Console.ReadLine() ?? "";
        Console.Write("New Quantity: ");
        int quantity = int.Parse(Console.ReadLine() ?? "0");
        Console.Write("New Price: ");
        decimal price = decimal.Parse(Console.ReadLine() ?? "0");

        using var connection = new MySqlConnection(connectionString);
        connection.Open();

        using var cmd = new MySqlCommand("UpdateBook", connection);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("p_book_id", id);
        cmd.Parameters.AddWithValue("p_title", title);
        cmd.Parameters.AddWithValue("p_author", author);
        cmd.Parameters.AddWithValue("p_quantity", quantity);
        cmd.Parameters.AddWithValue("p_price", price);
        int affectedRows = cmd.ExecuteNonQuery();

        Console.WriteLine(affectedRows > 0 ? "Book updated successfully." : "Book ID not found.");
    }

    static void DeleteBook()
    {
        Console.Write("Book ID to delete: ");
        int id = int.Parse(Console.ReadLine() ?? "0");

        using var connection = new MySqlConnection(connectionString);
        connection.Open();

        using var cmd = new MySqlCommand("DeleteBook", connection);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("p_book_id", id);
        int affectedRows = cmd.ExecuteNonQuery();

        Console.WriteLine(affectedRows > 0 ? "Book deleted successfully." : "Book ID not found.");
    }

    static void ListAllBooks()
    {
        using var connection = new MySqlConnection(connectionString);
        connection.Open();

        using var cmd = new MySqlCommand("GetAllBooks", connection);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        using var reader = cmd.ExecuteReader();
        Console.WriteLine("\nID | Title | Author | Quantity | Price");
        Console.WriteLine("-------------------------------------------");
        while (reader.Read())
        {
            Console.WriteLine($"{reader["id"]} | {reader["title"]} | {reader["author"]} | {reader["quantity"]} | {reader["price"]}");
        }
    }
}

