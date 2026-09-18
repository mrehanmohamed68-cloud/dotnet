using System;

namespace Part08_ConstructorOverloading
{
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }

        // Default constructor
        public Book()
        {
            Title = "Untitled";
            Author = "Unknown";
        }

        // Constructor taking only Title - delegates to the two-argument
        // constructor with a default Author, avoiding duplicated logic.
        public Book(string title) : this(title, "Unknown")
        {
        }

        // Constructor taking both Title and Author
        public Book(string title, string author)
        {
            Title = title;
            Author = author;
        }

        public override string ToString()
        {
            return $"\"{Title}\" by {Author}";
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Book b1 = new Book();
            Book b2 = new Book("The Pragmatic Programmer");
            Book b3 = new Book("Clean Code", "Robert C. Martin");

            Console.WriteLine(b1);
            Console.WriteLine(b2);
            Console.WriteLine(b3);
        }
    }
}

/*
QUESTION: How does constructor overloading improve class usability?

- It lets callers create an object with only the information they actually
  have at hand, instead of forcing every caller to supply every field every
  time (e.g. you can create a Book knowing only its title, and fill in the
  author later, or accept a sensible default).

- It provides sensible defaults (Book(), Book(title)) while still allowing
  full control when needed (Book(title, author)), making the class
  convenient for simple cases and flexible for complex ones.

- It centralizes initialization logic: by having the simpler constructors
  delegate to the most complete one (via `: this(...)`), you avoid
  duplicating the same assignment logic in multiple places, which reduces
  bugs and makes the class easier to maintain.

- It makes the API more discoverable/self-documenting - IntelliSense shows
  the different ways a Book can be constructed, guiding the caller toward
  valid ways to build one.
*/
