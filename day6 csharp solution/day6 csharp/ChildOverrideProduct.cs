namespace day6_csharp.Inheritance
{
    // Part4 requires seeing Product() behave with BOTH "new" and "override".
    // A class can't declare the same method twice (once new, once override),
    // so this second small class exists ONLY to prove what override does to
    // Product() -- compare its output against Child's "new" version in Program.cs.
    internal class ChildOverrideProduct : Parent
    {
        public int Z { get; set; }

        public ChildOverrideProduct(int x, int y, int z) : base(x, y)
        {
            Z = z;
        }

        // true override -> runtime/dynamic binding
        public override int Product()
        {
            return X * Y * Z;
        }
    }
}
