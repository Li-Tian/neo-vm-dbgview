using System.Collections.Generic;
using System.Linq;
using NoDbgViewTR;

namespace Neo.VM.Types
{
    public class Struct : Array
    {
        public Struct() : this(new List<StackItem>()) { }

        public Struct(IEnumerable<StackItem> value) : base(value)
        {
        }

        public StackItem Clone()
        {
            TR.Enter();
            List<StackItem> newArray = new List<StackItem>(this._array.Count);
            for (var i = 0; i < _array.Count; i++)
            {
                if (_array[i] is Struct s)
                {
                    newArray.Add(s.Clone());
                }
                else
                {
                    newArray.Add(_array[i]); //array = 是引用
                                             //其他的由于是固定值类型，不会改内部值，所以虽然需要复制，直接= 就行
                }
            }
            return TR.Exit(new Struct(newArray));
        }

        public override bool Equals(StackItem other)
        {
            TR.Enter();
            if (ReferenceEquals(this, other)) return TR.Exit(true);
            if (ReferenceEquals(null, other)) return TR.Exit(false);
            Struct a = other as Struct;
            if (a == null)
                return TR.Exit(false);
            else
                return TR.Exit(_array.SequenceEqual(a._array));
        }
    }
}
