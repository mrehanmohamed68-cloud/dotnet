using System;

namespace day6_csharp
{
    // Part1: Car with Id, Brand, Price + 4 constructors
    // Part8: also implements IMovable (reused instead of creating a duplicate Car class)
    internal class Car : IMovable
    {
        #region attributes
        private int id;
        private string brand;
        private double price;
        #endregion

        #region Properties
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Brand
        {
            get { return brand; }
            set { brand = value; }
        }
        public double Price
        {
            get { return price; }
            set { price = value; }
        }
        #endregion

        #region Constructors
        // 1- Default constructor
        // note: once ANY ctor is user-defined, the compiler stops
        // generating the default (parameterless) one automatically,
        // so we have to write it ourselves if we still need it
        public Car()
        {
            id = 0;
            brand = "Unknown";
            price = 0;
        }

        // 2- One parameter (Id) -> chaining to the general ctor
        public Car(int _id) : this(_id, "Unknown", 0)
        {
        }

        // 3- Two parameters (Id, Brand) -> chaining to the general ctor
        public Car(int _id, string _brand) : this(_id, _brand, 0)
        {
        }

        // 4- General ctor (all three parameters)
        public Car(int _id, string _brand, double _price)
        {
            id = _id;
            brand = _brand;
            price = _price;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"Id: {id}, Brand: {brand}, Price: {price}";
        }

        // Part8: IMovable implementation
        public void Move()
        {
            Console.WriteLine($"{brand} is moving...");
        }
        #endregion
    }
}
