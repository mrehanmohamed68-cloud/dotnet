using System;

namespace day6_csharp.Interfaces
{
    internal interface IShape
    {
        // Part6: get-only property -> no setter, implementing class decides how to compute it
        double Area { get; }

        // Part6: method every implementer MUST provide
        void Draw();

        // Part7 (C# 8.0+): default interface implementation.
        // Any class implementing IShape gets this method body for FREE
        // unless it explicitly overrides it -> no need to duplicate this
        // logic in every shape class (DRY).
        void PrintDetails()
        {
            Console.WriteLine($"Shape area = {Area}");
        }
    }
}
