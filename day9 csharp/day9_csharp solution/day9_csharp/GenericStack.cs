using System;

namespace day9_csharp
{
    // Problem 2: Generic class implementing a stack (LIFO) data structure.
    // Type safety: because the class is GenericStack<T>, once you declare
    // GenericStack<int> the compiler only allows int items — no boxing,
    // no casting, no risk of pushing the wrong type by mistake.
    public class GenericStack<T>
    {
        private T[] items;
        private int count;

        public GenericStack(int capacity = 10)
        {
            items = new T[capacity];
            count = 0;
        }

        public int Count => count;

        public bool IsEmpty => count == 0;

        public void Push(T item)
        {
            if (count == items.Length)
                Array.Resize(ref items, items.Length * 2); // grow when full

            items[count] = item;
            count++;
        }

        public T Pop()
        {
            if (IsEmpty)
                throw new InvalidOperationException("Stack is empty.");

            count--;
            T item = items[count];
            items[count] = default; // clear the slot
            return item;
        }

        public T Peek()
        {
            if (IsEmpty)
                throw new InvalidOperationException("Stack is empty.");

            return items[count - 1];
        }
    }
}
