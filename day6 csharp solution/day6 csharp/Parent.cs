namespace day6_csharp.Inheritance
{
    internal class Parent
    {
        #region Properties
        public int X { get; set; }
        public int Y { get; set; }
        #endregion

        #region Constructor
        public Parent(int x, int y)
        {
            X = x;
            Y = y;
        }
        #endregion

        #region Methods
        // virtual -> so Child CAN either hide it (new) or truly override it (override)
        public virtual int Product()
        {
            return X * Y;
        }

        public virtual int Sum()
        {
            return X + Y;
        }

        // Part5: overridden ToString
        public override string ToString()
        {
            return $"({X},{Y})";
        }
        #endregion
    }
}
