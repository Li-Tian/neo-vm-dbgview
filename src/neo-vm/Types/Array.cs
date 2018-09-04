using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NoDbgViewTR;

namespace Neo.VM.Types
{
    public class Array : StackItem, ICollection, IList<StackItem>
    {
        protected readonly List<StackItem> _array;

        public StackItem this[int index]
        {
            get => _array[index];
            set => _array[index] = value;
        }

        public int Count => _array.Count;
        public bool IsReadOnly => false;

        bool ICollection.IsSynchronized => false;
        object ICollection.SyncRoot => _array;

        public Array() : this(new List<StackItem>()) { }

        public Array(IEnumerable<StackItem> value)
        {
            TR.Enter();
            this._array = value as List<StackItem> ?? value.ToList();
            TR.Exit();
        }

        public void Add(StackItem item)
        {
            TR.Enter();
            _array.Add(item);
            TR.Exit();
        }

        public void Clear()
        {
            TR.Enter();
            _array.Clear();
            TR.Exit();
        }

        public bool Contains(StackItem item)
        {
            TR.Enter();
            return TR.Exit(_array.Contains(item));
        }

        void ICollection<StackItem>.CopyTo(StackItem[] array, int arrayIndex)
        {
            TR.Enter();
            _array.CopyTo(array, arrayIndex);
            TR.Exit();
        }

        void ICollection.CopyTo(System.Array array, int index)
        {
            TR.Enter();
            foreach (StackItem item in _array)
                array.SetValue(item, index++);
            TR.Exit();
        }

        public override bool Equals(StackItem other)
        {
            TR.Enter();
            return TR.Exit(ReferenceEquals(this, other));
        }

        public override bool GetBoolean()
        {
            TR.Enter();
            return TR.Exit(true);
        }

        public override byte[] GetByteArray()
        {
            TR.Log();
            throw new NotSupportedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            TR.Enter();
            return TR.Exit(GetEnumerator());
        }

        public IEnumerator<StackItem> GetEnumerator()
        {
            TR.Enter();
            return TR.Exit(_array.GetEnumerator());
        }

        int IList<StackItem>.IndexOf(StackItem item)
        {
            TR.Enter();
            return TR.Exit(_array.IndexOf(item));
        }

        public void Insert(int index, StackItem item)
        {
            TR.Enter();
            _array.Insert(index, item);
            TR.Exit();
        }

        bool ICollection<StackItem>.Remove(StackItem item)
        {
            TR.Enter();
            return TR.Exit(_array.Remove(item));
        }

        public void RemoveAt(int index)
        {
            TR.Enter();
            _array.RemoveAt(index);
            TR.Exit();
        }

        public void Reverse()
        {
            TR.Enter();
            _array.Reverse();
            TR.Exit();
        }
    }
}
