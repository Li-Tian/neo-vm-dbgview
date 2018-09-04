using System;
using System.Collections;
using System.Collections.Generic;
using NoDbgViewTR;

namespace Neo.VM
{
    public class RandomAccessStack<T> : IReadOnlyCollection<T>
    {
        private readonly List<T> list = new List<T>();

        public int Count => list.Count;

        public void Clear()
        {
            TR.Enter();
            list.Clear();
            TR.Exit();
        }

        public IEnumerator<T> GetEnumerator()
        {
            TR.Enter();
            return TR.Exit(list.GetEnumerator());
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            TR.Enter();
            return TR.Exit(GetEnumerator());
        }

        public void Insert(int index, T item)
        {
            TR.Enter();
            if (index > list.Count)
            {
                TR.Exit();
                throw new InvalidOperationException();
            }
            list.Insert(list.Count - index, item);
            TR.Exit();
        }

        public T Peek(int index = 0)
        {
            TR.Enter();
            if (index >= list.Count)
            {
                TR.Exit();
                throw new InvalidOperationException();
            }
            return TR.Exit(list[list.Count - 1 - index]);
        }

        public T Pop()
        {
            TR.Enter();
            return TR.Exit(Remove(0));
        }

        public void Push(T item)
        {
            TR.Enter();
            list.Add(item);
            TR.Exit();
        }

        public T Remove(int index)
        {
            TR.Enter();
            if (index >= list.Count)
            {
                TR.Exit();
                throw new InvalidOperationException();
            }
            T item = list[list.Count - index - 1];
            list.RemoveAt(list.Count - index - 1);
            return TR.Exit(item);
        }

        public void Set(int index, T item)
        {
            TR.Enter();
            if (index >= list.Count)
            {
                TR.Exit();
                throw new InvalidOperationException();
            }
            list[list.Count - index - 1] = item;
            TR.Exit();
        }
    }
}
