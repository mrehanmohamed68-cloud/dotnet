using System;

namespace Part06_Struct
{
    public struct Account
    {
        // Private backing fields - hidden from the outside world.
        private int _accountId;
        private string _accountHolder;
        private decimal _balance;

        public Account(int accountId, string accountHolder, decimal balance)
        {
            _accountId = accountId;
            _accountHolder = accountHolder;
            _balance = balance;
        }

        // Public properties expose controlled access to the private state.
        public int AccountId
        {
            get => _accountId;
        }

        public string AccountHolder
        {
            get => _accountHolder;
            set => _accountHolder = value; // allow renaming the holder
        }

        public decimal Balance
        {
            get => _balance;
            private set => _balance = value; // only this struct can set balance directly
        }

        // Behavior is exposed through methods instead of letting outside
        // code manipulate _balance directly, so invalid states (like a
        // negative deposit) are prevented.
        public void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Deposit amount must be positive.");
            Balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Withdrawal amount must be positive.");
            if (amount > Balance)
                throw new InvalidOperationException("Insufficient funds.");
            Balance -= amount;
        }

        public override string ToString()
        {
            return $"Account #{AccountId} ({AccountHolder}): {Balance:C}";
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Account acc = new Account(1001, "Nourhan", 500m);
            Console.WriteLine(acc);

            acc.Deposit(250m);
            Console.WriteLine("After deposit: " + acc);

            acc.Withdraw(100m);
            Console.WriteLine("After withdrawal: " + acc);

            try
            {
                acc.Withdraw(10000m); // will throw - insufficient funds
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}

/*
QUESTION: What is the key difference between encapsulation in structs and
classes?

The encapsulation MECHANISM itself is identical - private fields exposed
through public properties/methods, exactly as shown above. The real
differences come from struct vs class being VALUE types vs REFERENCE types:

- Copy semantics: a struct is copied by value every time it's assigned,
  passed as a parameter, or returned. So calling Deposit() on a struct
  variable only changes that particular copy, unless the struct is passed
  by `ref` or is a field of another object. Encapsulating mutable state
  in a struct is more error-prone for this reason - it's easy to "modify a
  copy" by mistake. Classes are reference types, so all variables
  referencing the same object see the same encapsulated state after a
  mutation.

- No inheritance: structs cannot inherit from another struct or class
  (though they can implement interfaces), so encapsulation can't be
  extended/overridden through a class hierarchy the way it can with
  classes (virtual/abstract members, protected access, etc.).

- Default constructor: every struct implicitly has a public parameterless
  constructor that zero-initializes all fields, which can bypass the
  invariants your custom constructor tries to enforce. Classes give you
  full control over whether a parameterless constructor exists at all.

- Storage/lifetime: structs are typically stack-allocated (or inline in
  their container) and have no concept of null (unless declared as
  Nullable<T> or a reference is boxed), while classes live on the heap and
  are accessed via references that can be null. This affects how "safe"
  encapsulated state really is against being in an unexpected state.
*/
