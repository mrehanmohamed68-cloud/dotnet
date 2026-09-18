using System;

namespace Part01_Interface
{
    // Interface defines a contract: any "vehicle" must know how to start and stop its engine.
    public interface IVehicle
    {
        void StartEngine();
        void StopEngine();
    }

    public class Car : IVehicle
    {
        public void StartEngine()
        {
            Console.WriteLine("Car: Engine started. Vroom!");
        }

        public void StopEngine()
        {
            Console.WriteLine("Car: Engine stopped.");
        }
    }

    public class Bike : IVehicle
    {
        public void StartEngine()
        {
            Console.WriteLine("Bike: Engine started. Brrr!");
        }

        public void StopEngine()
        {
            Console.WriteLine("Bike: Engine stopped.");
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            // We declare variables as IVehicle, not as Car or Bike.
            // The calling code doesn't know or care which concrete type it's using.
            IVehicle[] vehicles = new IVehicle[]
            {
                new Car(),
                new Bike()
            };

            foreach (IVehicle vehicle in vehicles)
            {
                vehicle.StartEngine();
                vehicle.StopEngine();
                Console.WriteLine();
            }
        }
    }
}

/*
QUESTION: Why is it better to code against an interface rather than a concrete class?

- Loose coupling: the calling code depends only on the interface's contract
  (StartEngine/StopEngine), not on how Car or Bike implement it internally.
  You can change the implementation of Car completely without breaking any
  code that only knows about IVehicle.

- Flexibility / polymorphism: the same array, method parameter, or field can
  hold any type that implements IVehicle (Car, Bike, Truck, ElectricScooter...)
  without changing the code that uses it. New vehicle types can be added later
  with zero changes to the consuming code.

- Testability: in unit tests you can substitute a fake/mock IVehicle instead
  of a real Car, without touching the class under test.

- Multiple implementations / multiple inheritance of behavior: a class can
  implement several interfaces (IVehicle, IComparable, IDisposable...) even
  though C# only allows single class inheritance. Coding against interfaces
  keeps the design open to that.

In short: interfaces let you program to "what an object can do" instead of
"what an object is", which keeps code decoupled, extensible, and easier to
test and maintain.
*/
