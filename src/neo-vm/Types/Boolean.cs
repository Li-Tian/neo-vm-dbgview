using System;
using System.Linq;
using System.Numerics;
using DbgViewTR;

namespace Neo.VM.Types
{
    public class Boolean : StackItem
    {
        private static readonly byte[] TRUE = { 1 };
        private static readonly byte[] FALSE = new byte[0];

        private bool value;

        public Boolean(bool value)
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
            if (other is Boolean b) return TR.Exit(value == b.value);
            byte[] bytes_other;
            try
            {
                bytes_other = other.GetByteArray();
            }
            catch (NotSupportedException)
            {
                return TR.Exit(false);
            }
            return TR.Exit(GetByteArray().SequenceEqual(bytes_other));
        }

        public override BigInteger GetBigInteger()
        {
            TR.Enter();
            TR.Exit();
            return value ? BigInteger.One : BigInteger.Zero;
        }

        public override bool GetBoolean()
        {
            TR.Enter();
            return TR.Exit(value);
        }

        public override byte[] GetByteArray()
        {
            TR.Enter();
            TR.Exit();
            return value ? TRUE : FALSE;
        }
    }
}
