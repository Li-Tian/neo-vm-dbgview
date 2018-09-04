using System;
using System.Linq;
using System.Numerics;
using NoDbgViewTR;

namespace Neo.VM.Types
{
    public class Integer : StackItem
    {
        private BigInteger value;

        public Integer(BigInteger value)
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
            if (other is Integer i) return TR.Exit(value == i.value);
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
            return TR.Exit(value);
        }

        public override bool GetBoolean()
        {
            TR.Enter();
            return TR.Exit(value != BigInteger.Zero);
        }

        public override byte[] GetByteArray()
        {
            TR.Enter();
            return TR.Exit(value.ToByteArray());
        }
    }
}
