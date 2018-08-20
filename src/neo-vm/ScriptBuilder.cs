using System;
using System.IO;
using System.Numerics;
using System.Text;
using DbgViewTR;

namespace Neo.VM
{
    public class ScriptBuilder : IDisposable
    {
        private readonly MemoryStream ms = new MemoryStream();
        private readonly BinaryWriter writer;

        public int Offset => (int)ms.Position;

        public ScriptBuilder()
        {
            TR.Enter();
            this.writer = new BinaryWriter(ms);
            TR.Exit();
        }

        public void Dispose()
        {
            TR.Enter();
            writer.Dispose();
            ms.Dispose();
            TR.Exit();
        }

        public ScriptBuilder Emit(OpCode op, byte[] arg = null)
        {
            TR.Enter();
            writer.Write((byte)op);
            if (arg != null)
                writer.Write(arg);
            return TR.Exit(this);
        }

        public ScriptBuilder EmitAppCall(byte[] scriptHash, bool useTailCall = false)
        {
            TR.Enter();
            if (scriptHash.Length != 20)
            {
                TR.Exit();
                throw new ArgumentException();
            }
            return TR.Exit(Emit(useTailCall ? OpCode.TAILCALL : OpCode.APPCALL, scriptHash));
        }

        public ScriptBuilder EmitJump(OpCode op, short offset)
        {
            TR.Enter();
            if (op != OpCode.JMP && op != OpCode.JMPIF && op != OpCode.JMPIFNOT && op != OpCode.CALL)
            {
                TR.Exit();
                throw new ArgumentException();
            }
            return TR.Exit(Emit(op, BitConverter.GetBytes(offset)));
        }

        public ScriptBuilder EmitPush(BigInteger number)
        {
            TR.Enter();
            if (number == -1) return TR.Exit(Emit(OpCode.PUSHM1));
            if (number == 0) return TR.Exit(Emit(OpCode.PUSH0));
            if (number > 0 && number <= 16) return TR.Exit(Emit(OpCode.PUSH1 - 1 + (byte)number));
            return TR.Exit(EmitPush(number.ToByteArray()));
        }

        public ScriptBuilder EmitPush(bool data)
        {
            TR.Enter();
            return TR.Exit(Emit(data ? OpCode.PUSHT : OpCode.PUSHF));
        }

        public ScriptBuilder EmitPush(byte[] data)
        {
            TR.Enter();
            if (data == null)
            {
                TR.Exit();
                throw new ArgumentNullException();
            }
            if (data.Length <= (int)OpCode.PUSHBYTES75)
            {
                writer.Write((byte)data.Length);
                writer.Write(data);
            }
            else if (data.Length < 0x100)
            {
                Emit(OpCode.PUSHDATA1);
                writer.Write((byte)data.Length);
                writer.Write(data);
            }
            else if (data.Length < 0x10000)
            {
                Emit(OpCode.PUSHDATA2);
                writer.Write((ushort)data.Length);
                writer.Write(data);
            }
            else// if (data.Length < 0x100000000L)
            {
                Emit(OpCode.PUSHDATA4);
                writer.Write(data.Length);
                writer.Write(data);
            }
            return TR.Exit(this);
        }

        public ScriptBuilder EmitPush(string data)
        {
            TR.Enter();
            return TR.Exit(EmitPush(Encoding.UTF8.GetBytes(data)));
        }

        public ScriptBuilder EmitSysCall(string api)
        {
            TR.Enter();
            if (api == null)
            {
                TR.Exit();
                throw new ArgumentNullException();
            }
            byte[] api_bytes = Encoding.ASCII.GetBytes(api);
            if (api_bytes.Length == 0 || api_bytes.Length > 252)
            {
                TR.Exit();
                throw new ArgumentException();
            }
            byte[] arg = new byte[api_bytes.Length + 1];
            arg[0] = (byte)api_bytes.Length;
            Buffer.BlockCopy(api_bytes, 0, arg, 1, api_bytes.Length);
            return TR.Exit(Emit(OpCode.SYSCALL, arg));
        }

        public byte[] ToArray()
        {
            TR.Enter();
            writer.Flush();
            return TR.Exit(ms.ToArray());
        }
    }
}
