namespace day6_csharp.Inheritance
{
    // Part3: inherits from Parent, adds Z
    internal class Child : Parent
    {
        public int Z { get; set; }

        // Part3: constructor chaining
        // base(x, y) builds the Parent part FIRST and guarantees it's
        // in a valid state before Child adds its own member Z
        public Child(int x, int y, int z) : base(x, y)
        {
            Z = z;
        }

        #region Part4 - Product() hidden using "new"
        // "new" -> this is a completely NEW, unrelated method that just
        // happens to share the same name. It does NOT participate in
        // polymorphism. Binding is resolved at COMPILE time based on the
        // declared (reference) type, not the actual object type.
        public new int Product()
        {
            return X * Y * Z;
        }
        #endregion

        #region Part4 - Sum() overridden using "override" (true polymorphism)
        // "override" -> replaces Parent's implementation for real.
        // Binding is resolved at RUNTIME based on the actual object type
        // (late/dynamic binding), even through a Parent-typed reference.
        public override int Sum()
        {
            return X + Y + Z;
        }
        #endregion

        // Part5: overridden ToString
        public override string ToString()
        {
            return $"({X},{Y},{Z})";
        }
    }
}
