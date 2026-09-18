using System;

namespace Part07_DefaultInterfaceMethod
{
    public interface ILogger
    {
        // Default interface implementation (C# 8.0+): a method body lives
        // right in the interface. Any class implementing ILogger gets this
        // behavior "for free" unless it chooses to override it.
        void Log(string message)
        {
            Console.WriteLine($"[DEFAULT LOG] {message}");
        }
    }

    // Doesn't override Log() at all - it uses the interface's default body.
    public class SilentLogger : ILogger
    {
    }

    // Overrides the default implementation with its own.
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine($"[CONSOLE] {DateTime.Now:HH:mm:ss} - {message}");
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            ILogger defaultLogger = new SilentLogger();
            defaultLogger.Log("Using the interface's default implementation.");

            ILogger consoleLogger = new ConsoleLogger();
            consoleLogger.Log("Using ConsoleLogger's overridden implementation.");
        }
    }
}

/*
QUESTION: How do default interface implementations affect backward
compatibility in C#?

Default interface methods (introduced in C# 8.0) let you add a NEW member
to an already-published interface without breaking every existing class
that implements it. Before this feature, adding any member to an interface
was a breaking change: every class implementing that interface would fail
to compile until it added the new member too.

With a default implementation:
- Old classes that implement the interface (like SilentLogger above, if it
  existed before Log() was added) keep compiling and working, automatically
  inheriting the new default behavior.
- Classes that want custom behavior can still override the default, exactly
  like ConsoleLogger does here.

This is primarily useful for library/API authors: it allows an interface's
contract to evolve over time (adding new capabilities) while remaining
source- and binary-compatible with code that was written against the
older version of the interface.
*/
