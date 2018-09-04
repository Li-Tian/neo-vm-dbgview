using System;
using System.IO;
using System.Text;
using NoDbgViewTR;

namespace Neo.VM
{
    internal static class Helper
    {
        public static byte[] ReadVarBytes(this BinaryReader reader, int max = 0X7fffffc7)
        {
            TR.Enter();
            return TR.Exit(reader.ReadBytes((int)reader.ReadVarInt((ulong)max)));
        }

        public static ulong ReadVarInt(this BinaryReader reader, ulong max = ulong.MaxValue)
        {
            TR.Enter();
            byte fb = reader.ReadByte();
            ulong value;
            if (fb == 0xFD)
                value = reader.ReadUInt16();
            else if (fb == 0xFE)
                value = reader.ReadUInt32();
            else if (fb == 0xFF)
                value = reader.ReadUInt64();
            else
                value = fb;
            if (value > max)
            {
                TR.Exit();
                throw new FormatException();
            }
            return TR.Exit(value);
        }

        public static string ReadVarString(this BinaryReader reader)
        {
            TR.Enter();
            return TR.Exit(Encoding.UTF8.GetString(reader.ReadVarBytes()));
        }

        public static void WriteVarBytes(this BinaryWriter writer, byte[] value)
        {
            TR.Enter();
            writer.WriteVarInt(value.Length);
            writer.Write(value);
            TR.Exit();
        }

        public static void WriteVarInt(this BinaryWriter writer, long value)
        {
            TR.Enter();
            if (value < 0)
            {
                TR.Exit();
                throw new ArgumentOutOfRangeException();
            }
            if (value < 0xFD)
            {
                writer.Write((byte)value);
            }
            else if (value <= 0xFFFF)
            {
                writer.Write((byte)0xFD);
                writer.Write((ushort)value);
            }
            else if (value <= 0xFFFFFFFF)
            {
                writer.Write((byte)0xFE);
                writer.Write((uint)value);
            }
            else
            {
                writer.Write((byte)0xFF);
                writer.Write(value);
            }
            TR.Exit();
        }

        public static void WriteVarString(this BinaryWriter writer, string value)
        {
            TR.Enter();
            writer.WriteVarBytes(Encoding.UTF8.GetBytes(value));
            TR.Exit();
        }
    }
}
