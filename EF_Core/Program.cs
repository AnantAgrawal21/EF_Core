using BookHaven;
using Microsoft.EntityFrameworkCore;

using var db = new BookHavenContext();

// ---------------------------------------------------------------------------
// Step 5: create the database + schema from the model.
//
// EnsureDeleted() is here ONLY so you can run this demo repeatedly and always
// start from a clean database. EnsureCreated() alone won't touch an existing
// db, so re-running would keep appending rows.
//
// IMPORTANT: EnsureCreated bypasses migrations. We replace both of these calls
// with a real migration in Part 3. Never mix EnsureCreated() with migrations.
// ---------------------------------------------------------------------------
db.Database.EnsureDeleted();
db.Database.EnsureCreated();

// ---------------------------- CREATE ----------------------------
db.Books.AddRange(
    new Book { Title = "The Pragmatic Programmer", Author = "Hunt & Thomas", Price = 39.99m, PublishedOn = new DateOnly(1999, 10, 20) },
    new Book { Title = "Clean Code", Author = "Robert C. Martin", Price = 32.50m, PublishedOn = new DateOnly(2008, 8, 1) },
    new Book { Title = "Domain-Driven Design", Author = "Eric Evans", Price = 54.00m, PublishedOn = new DateOnly(2003, 8, 30) });

int inserted = db.SaveChanges();   // SaveChanges returns the number of affected rows
Console.WriteLine($"Inserted {inserted} rows.\n");

// ---------------------------- READ ------------------------------
Console.WriteLine("All books:");
PrintAll();

// ---------------------------- UPDATE ----------------------------
// Query the entity, change a property, save. The change tracker notices the
// modified property and issues an UPDATE touching only that column.
Book cleanCode = db.Books.Single(b => b.Title == "Clean Code");
cleanCode.Price = 27.99m;
db.SaveChanges();
Console.WriteLine($"\nUpdated \"{cleanCode.Title}\" price -> {cleanCode.Price:0.00}\n");

// ---------------------------- DELETE ----------------------------
Book ddd = db.Books.Single(b => b.Title == "Domain-Driven Design");
db.Books.Remove(ddd);
db.SaveChanges();
Console.WriteLine($"Deleted \"{ddd.Title}\".\n");

Console.WriteLine("Final state:");
PrintAll();

// Local helper — reads straight from the database each time it's called.
void PrintAll()
{
    foreach (Book b in db.Books.OrderBy(b => b.Id))
        Console.WriteLine($"  [{b.Id}] {b.Title,-28} {b.Author,-18} {b.Price,7:0.00}  {b.PublishedOn:yyyy-MM-dd}");
}