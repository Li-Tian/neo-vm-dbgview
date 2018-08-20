using System;
using System.Linq;
using DbgViewTR;

namespace Neo.VM.Types
{
    public class ByteArray : StackItem
    {
        private byte[] value;

        public ByteArray(byte[] value)
        {
            TR.Enter();
            this.value = value;
            TR.Exit();
        }

        public override bool Equals(StackItem other)
        {
            TR.Enter();
            if (ReferenceEquals(this, other)) return TR.Exit(true);
            if (ReferenceEquals(null, other)) return TR.Exit(false);
            byte[] bytes_other;
            try
            {
                bytes_other = other.GetByteArray();
            }
            catch (NotSupportedException)
            {
                return TR.Exit(false);
            }
            return TR.Exit(value.SequenceEqual(bytes_other));
        }

        public override byte[] GetByteArray()
        {
            TR.Enter();
            return TR.Exit(value);
        }
    }
}
