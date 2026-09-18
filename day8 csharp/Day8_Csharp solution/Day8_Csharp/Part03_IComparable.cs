using System;

namespace Part03_IComparable
{
    public class Product : IComparable<Product>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public Product(int id, string name, double price)
        {
            Id = id;
            Name = name;
            Price = price;
        }

        // Defines the "natural" ordering for Product: by Price, ascending.
        // Because of this, Array.Sort / List.Sort / OrderBy etc. all know
        // how to sort Product objects without us telling them how each time.
        public int CompareTo(Product other)
        {
            if (other == null) return 1;
            return this.Price.CompareTo(other.Price);
        }

        public override string ToString()
        {
            return $"[{Id}] {Name} - ${Price:F2}";
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Product[] products = new Product[]
            {
                new Product(1, "Keyboard", 29.99),
                new Product(2, "Monitor", 189.50),
                new Product(3, "Mouse", 14.25),
                new Product(4, "Laptop", 899.00)
            };

            Console.WriteLine("Before sorting:");
            foreach (var p in products) Console.WriteLine(p);

            // Array.Sort uses Product.CompareTo internally because Product
            // implements IComparable<Product>.
            Array.Sort(products);

            Console.WriteLine("\nAfter sorting by Price (ascending):");
            foreach (var p in products) Console.WriteLine(p);

            // Sorting descending is trivial by reversing.
            Array.Reverse(products);
            Console.WriteLine("\nDescending:");
            foreach (var p in products) Console.WriteLine(p);
        }
    }
}

/*
QUESTION: How does implementing IComparable improve flexibility in sorting?

- It gives the type a single, well-defined "default" ordering (here: by
  Price) that all of .NET's built-in sorting facilities understand
  automatically: Array.Sort, List<T>.Sort, SortedList, SortedSet,
  Enumerable.OrderBy fallback, binary search, etc. You don't need to write
  or pass a custom comparison function every time you sort.

- Any code that receives a Product doesn't need to know the sorting rule
  in advance - it can just call Sort() and rely on the type itself to
  define what "less than" means for it.

- It plays well with generic algorithms and collections that constrain
  their type parameter with `where T : IComparable<T>` - such methods can
  now accept Product without any extra wiring.

- Flexibility remains even for other orderings: if you sometimes need a
  different sort (e.g. by Name), you're not stuck - you write a separate
  IComparer<Product> (e.g. NameComparer) and pass it to Sort/OrderBy,
  while the default IComparable ordering stays available as the sensible
  fallback.
*/
