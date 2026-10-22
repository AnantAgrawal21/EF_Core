namespace BookHaven;

public class Book
{
    // "Id" is picked up as the primary key by convention (it's Identity in the DB).
    public int Id { get; set; }

    // 'required' forces these to be set when constructing a Book, which pairs
    // nicely with non-nullable reference types. Both map to NOT NULL columns.
    public required string Title { get; set; }
    public required string Author { get; set; }   // plain string for now — becomes its own entity later

    // Money -> decimal (never double/float for currency).
    public decimal Price { get; set; }

    // A publication date has no time component, so DateOnly is the natural fit.
    // EF Core 8+ maps DateOnly across providers, including SQLite.
    public DateOnly PublishedOn { get; set; }
}