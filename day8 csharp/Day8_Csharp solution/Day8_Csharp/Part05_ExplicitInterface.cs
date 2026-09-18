using System;

namespace Part05_ExplicitInterface
{
    public interface IWalkable
    {
        void Walk();
    }

    public class Robot : IWalkable
    {
        // Robot's OWN Walk() method - its normal, publicly visible behavior.
        public void Walk()
        {
            Console.WriteLine("Robot: walking normally using leg motors.");
        }

        // Explicit interface implementation. This method can ONLY be called
        // through an IWalkable-typed reference, never through a Robot
        // reference directly. It lets Robot have two different behaviors
        // that would otherwise collide under the same name "Walk".
        void IWalkable.Walk()
        {
            Console.WriteLine("Robot: executing IWalkable.Walk() - diagnostic walk-test routine.");
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Robot robot = new Robot();

            // Calling through the concrete Robot type invokes Robot's own Walk().
            robot.Walk();

            // Calling through the interface type invokes the explicit
            // implementation instead.
            IWalkable walkable = robot;
            walkable.Walk();
        }
    }
}

/*
QUESTION: How does explicit interface implementation help in resolving
naming conflicts?

- It lets a class provide a DIFFERENT implementation for a method required
  by an interface than the method it exposes under its own public API,
  even if they share the same name and signature (Walk() in this example).
  The explicit version is only reachable through a variable typed as the
  interface, keeping it out of the class's normal public surface.

- It's essential when a class implements two or more interfaces that both
  declare a member with the same name/signature (e.g. IWalkable.Walk() and
  IPerformer.Walk() with different meanings) - explicit implementation lets
  you implement both without one hiding or overriding the other.

- It also lets a class implement an interface member deliberately with
  restricted/limited visibility: callers who only have a Robot reference
  won't even see IWalkable.Walk() in IntelliSense/autocomplete - they need
  to know to cast to IWalkable first. This can be used to keep an
  interface's technical/plumbing members out of a class's everyday API.
*/
