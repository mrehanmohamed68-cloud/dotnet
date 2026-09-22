# Self Study Report

## 1. Generalization Concept Using Generics

**Generalization** in OOP means designing a type or a piece of logic so it works
for a *family* of related things instead of one specific thing — pulling out
what's common and leaving the specific "what type" as a detail to be filled in
later.

**Generics** are C#'s tool for achieving this at the language level. Instead of
writing a separate `Swap`, `SearchArray`, or `Stack` for `int`, then another
for `string`, then another for `Employee`, you write the logic **once** using
a placeholder type parameter (`T`), and the compiler generates the correct,
type-safe version for whichever concrete type is used at the call site.

```csharp
// Without generics: one method per type (repetition, not scalable)
public static void SwapInt(ref int a, ref int b) { ... }
public static void SwapDouble(ref double a, ref double b) { ... }

// With generics: one generalized method for ANY type
public static void Swap<T>(ref T a, ref T b)
{
    T temp = a;
    a = b;
    b = temp;
}
```

Why this **is** Generalization:
- The *behavior* (swap, search, store in LIFO order...) is what's common across
  all types — that's the part we keep.
- The *specific data type* is what varies — that's the part we generalize away
  using `T`.
- We can further **constrain** the generalization when the logic needs a
  guarantee about T (e.g. `where T : IComparable<T>` for `Max<T>` / `FindMax<T>`),
  which is a controlled, narrower form of generalization: "any type, as long
  as it can compare itself."

Benefits: code reuse (DRY), compile-time type safety (no casting/boxing like
the old `object`-based approach), better performance (no boxing for value
types), and one place to fix bugs or add features instead of N duplicated
methods.

---

## 2. Hierarchy Design in Real Business

**Hierarchy design** means modeling real business entities using
**inheritance**, based on an **"Is-A"** relationship, so that shared
state/behavior lives once in a common base, and each specific role only adds
or changes what's different about it.

Example from a training/education system (the same domain used in the course
demos):

```
        User (Common: Id, Name, Email, Login())
         |
   ------------------------
   |                      |
 Instructor            Student
   |
 ---------------
 |             |
FullTime     PartTime
```

- `User` holds what's common to *everyone* in the system (identity, contact
  info, authentication).
- `Instructor` and `Student` are both a `User` (Is-A), but each has extra
  state/behavior an ordinary user doesn't (an Instructor has a `Salary` and
  `TeachCourse()`; a Student has `EnrolledCourses` and `Submit()`).
- `FullTime`/`PartTime` further specialize `Instructor` (different salary
  calculation, different `virtual`/`override` behavior).

Why businesses design hierarchies this way:
- **DRY / Code reuse** — shared fields and methods are written once in the
  base class instead of copy-pasted into every role.
- **Maintainability** — a change to shared behavior (e.g. how `Login()`
  works) is made in one place and every derived type gets it automatically.
- **Polymorphism** — code can work against the base type (`User` or
  `Instructor`) and still get the correct derived behavior at runtime
  (`override`), which is what makes systems extensible: adding a new kind of
  Instructor later doesn't require touching existing code.
- **Reflects the real domain** — the hierarchy should mirror how the business
  actually thinks about its entities, which makes the code easier to discuss
  with non-technical stakeholders and easier to extend as the business
  evolves (e.g. adding a new `Instructor` subtype for "Guest Lecturer").

The risk to watch for: over-deep or incorrect hierarchies (forcing an
"Is-A" relationship where it's really "Has-A") lead to fragile, hard-to-change
designs — this is why "favor composition over inheritance" is a common
guideline when the relationship isn't a true Is-A.

---

## 3. What Is Event-Driven Programming

**Event-driven programming** is a programming model where the flow of the
program is controlled by **events** — things that happen (a button click, a
sensor reading, a message arriving, a value changing) — rather than by a
fixed, top-to-bottom sequence of instructions.

Core pieces (as implemented in C#/.NET):
- **Event source (publisher)** — an object that can raise/fire an event, e.g.
  `event EventHandler OnSalaryChanged;`
- **Event** — a signal the source raises when something happens, e.g.
  `OnSalaryChanged?.Invoke(this, EventArgs.Empty);`
- **Event handler (subscriber)** — a method that "subscribes" to the event
  and runs automatically when it's raised:
  `employee.OnSalaryChanged += HandleSalaryChanged;`
- **Delegate** — the underlying type that defines the signature the handler
  method must match; events in C# are built on top of delegates.

```csharp
public class Employee
{
    public event EventHandler SalaryChanged;
    private decimal salary;

    public decimal Salary
    {
        get => salary;
        set
        {
            salary = value;
            SalaryChanged?.Invoke(this, EventArgs.Empty); // raise the event
        }
    }
}

// Subscriber
employee.SalaryChanged += (sender, e) =>
    Console.WriteLine("Salary was updated!");
```

Why it matters in real applications:
- **Decoupling** — the class raising the event (`Employee`) doesn't need to
  know *who* is listening or *what* they'll do; it just announces "this
  happened." Any number of independent subscribers can react.
- **Responsiveness** — natural fit for UI programming (button clicks, text
  changed), where the program must react to whatever the user does next
  rather than execute a predetermined sequence.
- **Extensibility** — new behavior can be added later by subscribing a new
  handler, without modifying the class that raises the event (this lines up
  with the Open/Closed Principle already covered in the course).

Contrast with procedural flow: in a normal method, the caller decides exactly
when and what runs next. In event-driven code, the *event* decides when a
handler runs — the program spends much of its time waiting/listening rather
than executing a fixed sequence.
