using System;
using System.Collections.Generic;
using System.Linq;
using NoDbgViewTR;

namespace Neo.VM
{
    internal class HashComparer : IEqualityComparer<byte[]>
    {
        public bool Equals(byte[] x, byte[] y)
        {
            TR.Enter();
            return TR.Exit(x.SequenceEqual(y));
        }

        public int GetHashCode(byte[] obj)
        {
            TR.Enter();
            return TR.Exit(BitConverter.ToInt32(obj, 0));
        }
    }
}
