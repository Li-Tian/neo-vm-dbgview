using System;
using System.Collections;
using System.Collections.Generic;
using DbgViewTR;

namespace Neo.VM.Types
{
    public class Map : StackItem, ICollection, IDictionary<StackItem, StackItem>
    {
        private readonly Dictionary<StackItem, StackItem> dictionary;

        public StackItem this[StackItem key]
        {
            get => this.dictionary[key];
            set => this.dictionary[key] = value;
        }

        public ICollection<StackItem> Keys => dictionary.Keys;
        public ICollection<StackItem> Values => dictionary.Values;
        public int Count => dictionary.Count;
        public bool IsReadOnly => false;

        bool ICollection.IsSynchronized => false;
        object ICollection.SyncRoot => dictionary;

        public Map() : this(new Dictionary<StackItem, StackItem>()) { }

        public Map(Dictionary<StackItem, StackItem> value)
        {
            TR.Enter();
            this.dictionary = value;
            TR.Exit();
        }

        public void Add(StackItem key, StackItem value)
        {
            TR.Enter();
            dictionary.Add(key, value);
            TR.Exit();
        }

        void ICollection<KeyValuePair<StackItem, StackItem>>.Add(KeyValuePair<StackItem, StackItem> item)
        {
            TR.Enter();
            dictionary.Add(item.Key, item.Value);
            TR.Exit();
        }

        public void Clear()
        {
            TR.Enter();
            dictionary.Clear();
            TR.Exit();
        }

        bool ICollection<KeyValuePair<StackItem, StackItem>>.Contains(KeyValuePair<StackItem, StackItem> item)
        {
            TR.Enter();
            return TR.Exit(dictionary.ContainsKey(item.Key));
        }

        public bool ContainsKey(StackItem key)
        {
            TR.Enter();
            return TR.Exit(dictionary.ContainsKey(key));
        }

        void ICollection<KeyValuePair<StackItem, StackItem>>.CopyTo(KeyValuePair<StackItem, StackItem>[] array, int arrayIndex)
        {
            TR.Enter();
            foreach (KeyValuePair<StackItem, StackItem> item in dictionary)
                array[arrayIndex++] = item;
            TR.Exit();
        }

        void ICollection.CopyTo(System.Array array, int index)
        {
            TR.Enter();
            foreach (KeyValuePair<StackItem, StackItem> item in dictionary)
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
            TR.Enter();
            TR.Exit();
            throw new NotSupportedException();
        }

        IEnumerator<KeyValuePair<StackItem, StackItem>> IEnumerable<KeyValuePair<StackItem, StackItem>>.GetEnumerator()
        {
            TR.Enter();
            return TR.Exit(dictionary.GetEnumerator());
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            TR.Enter();
            return TR.Exit(dictionary.GetEnumerator());
        }

        public bool Remove(StackItem key)
        {
            TR.Enter();
            return TR.Exit(dictionary.Remove(key));
        }

        bool ICollection<KeyValuePair<StackItem, StackItem>>.Remove(KeyValuePair<StackItem, StackItem> item)
        {
            TR.Enter();
            return TR.Exit(dictionary.Remove(item.Key));
        }

        public bool TryGetValue(StackItem key, out StackItem value)
        {
            TR.Enter();
            return TR.Exit(dictionary.TryGetValue(key, out value));
        }
    }
}
