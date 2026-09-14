namespace day6_csharp
{
    // Part2: Method overloading -> same name (Sum), different parameter list
    // (difference in Number - Type - Order of parameters)
    internal class Calculator
    {
        // 1- two integers
        public int Sum(int a, int b)
        {
            return a + b;
        }

        // 2- three integers (different Number of params)
        public int Sum(int a, int b, int c)
        {
            return a + b + c;
        }

        // 3- two doubles (different Type of params)
        public double Sum(double a, double b)
        {
            return a + b;
        }
    }
}
