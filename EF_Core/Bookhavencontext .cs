using Microsoft.EntityFrameworkCore;

namespace BookHaven;

public class BookHavenContext : DbContext
{
    // Using Set<Book>() instead of an auto-property avoids a nullable warning
    // while still giving you the familiar db.Books API.
    public DbSet<Book> Books => Set<Book>();

    // Simplest possible configuration: hard-code the connection string here.
    // "Data Source=bookhaven.db" creates a file called bookhaven.db in the
    // working directory. In a real app you'd move this to configuration and
    // register the context with dependency injection instead.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=bookhaven.db");
}