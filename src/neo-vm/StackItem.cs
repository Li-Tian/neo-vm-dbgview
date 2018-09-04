using Neo.VM.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Array = Neo.VM.Types.Array;
using Boolean = Neo.VM.Types.Boolean;
using NoDbgViewTR;

namespace Neo.VM
{
    public abstract class StackItem : IEquatable<StackItem>
    {
        public abstract bool Equals(StackItem other);

        public sealed override bool Equals(object obj)
        {
            TR.Enter();
            if (obj == null) return TR.Exit(false);
            if (obj == this) return TR.Exit(true);
            if (obj is StackItem other)
                return TR.Exit(Equals(other));
            return TR.Exit(false);
        }

        public static StackItem FromInterface(IInteropInterface value)
        {
            TR.Enter();
            return TR.Exit(new InteropInterface(value));
        }

        public virtual BigInteger GetBigInteger()
        {
            TR.Enter();
            return TR.Exit(new BigInteger(GetByteArray()));
        }

        public virtual bool GetBoolean()
        {
            TR.Enter();
            return TR.Exit(GetByteArray().Any(p => p != 0));
        }

        public abstract byte[] GetByteArray();

        public override int GetHashCode()
        {
            TR.Enter();
            unchecked
            {
                int hash = 17;
                foreach (byte element in GetByteArray())
                    hash = hash * 31 + element;
                return TR.Exit(hash);
            }
        }

        public virtual string GetString()
        {
            TR.Enter();
            return TR.Exit(Encoding.UTF8.GetString(GetByteArray()));
        }

        public static implicit operator StackItem(int value)
        {
            TR.Enter();
            return TR.Exit((BigInteger)value);
        }

        public static implicit operator StackItem(uint value)
        {
            TR.Enter();
            return TR.Exit((BigInteger)value);
        }

        public static implicit operator StackItem(long value)
        {
            TR.Enter();
            return TR.Exit((BigInteger)value);
        }

        public static implicit operator StackItem(ulong value)
        {
            TR.Enter();
            return TR.Exit((BigInteger)value);
        }

        public static implicit operator StackItem(BigInteger value)
        {
            TR.Enter();
            return TR.Exit(new Integer(value));
        }

        public static implicit operator StackItem(bool value)
        {
            TR.Enter();
            return TR.Exit(new Boolean(value));
        }

        public static implicit operator StackItem(byte[] value)
        {
            TR.Enter();
            return TR.Exit(new ByteArray(value));
        }

        public static implicit operator StackItem(StackItem[] value)
        {
            TR.Enter();
            return TR.Exit(new Array(value));
        }

        public static implicit operator StackItem(List<StackItem> value)
        {
            TR.Enter();
            return TR.Exit(new Array(value));
        }
    }
}
