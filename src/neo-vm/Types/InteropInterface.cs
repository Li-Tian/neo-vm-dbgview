using System;
using NoDbgViewTR;

namespace Neo.VM.Types
{
    public class InteropInterface : StackItem
    {
        private IInteropInterface _object;

        public InteropInterface(IInteropInterface value)
        {
            TR.Enter();
            this._object = value;
            TR.Exit();
        }

        public override bool Equals(StackItem other)
        {
            TR.Enter();
            if (ReferenceEquals(this, other)) return TR.Exit(true);
            if (ReferenceEquals(null, other)) return TR.Exit(false);
            InteropInterface i = other as InteropInterface;
            if (i == null) return TR.Exit(false);
            return TR.Exit(_object.Equals(i._object));
        }

        public override bool GetBoolean()
        {
            TR.Enter();
            return TR.Exit(_object != null);
        }

        public override byte[] GetByteArray()
        {
            TR.Enter();
            TR.Exit();
            throw new NotSupportedException();
        }

        public T GetInterface<T>() where T : class, IInteropInterface
        {
            TR.Enter();
            return TR.Exit(_object as T);
        }
    }
}
