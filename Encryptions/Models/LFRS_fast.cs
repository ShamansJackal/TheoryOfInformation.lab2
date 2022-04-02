using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using static TheoryOfInformation.lab2.Encryptions.TextWorker;

namespace TheoryOfInformation.lab2.Encryptions.Models
{
    public class LFRS_fast : IEncryption
    {
        private int _size = 34;
        public string BuildKey(ulong beginState, ulong length)
        {
            StringBuilder builder = new StringBuilder((int)length);
            ulong state = beginState;
            for (ulong i = 0; i < length; i++)
            {
                ushort rigthBit = 0;

                ulong mask1 = 1; mask1 <<= 33; mask1 &= state;
                rigthBit += (ushort)(mask1 >> 33);
                rigthBit += (ushort)((state & (1 << 14)) >> 14);
                rigthBit += (ushort)((state & (1 << 13)) >> 13);
                rigthBit += (ushort)((state & 1));
                if (rigthBit == 4)
                {
                    rigthBit = 0;
                } else
                {
                    rigthBit = 1;
                }
                state = (state << 1) + rigthBit;
                ulong mask = 1; mask <<= _size; mask = state & mask;
                builder = builder.Append(mask > 0 ? "1" : "0");
                mask = 1; mask <<= _size;
                state &= (mask - 1);
            }
            return builder.ToString();
        }

        public byte[] BuildKeyForFile(ulong beginState, ulong lengthOfBytes)
        {
            byte[] result = new byte[lengthOfBytes];
            ulong state = beginState;
            for (ulong i = 0; i < lengthOfBytes << 3; i++)
            {
                ushort rigthBit = 0;

                ulong mask1 = 1; mask1 <<= 33; mask1 &= state;
                rigthBit += (ushort)(mask1 >> 33);
                rigthBit += (ushort)((state & (ulong)(1 << 14)) >> 14);
                rigthBit += (ushort)((state & (ulong)(1 << 13)) >> 13);
                rigthBit += (ushort)((state & 1));
                if (rigthBit == 4)
                {
                    rigthBit = 0;
                }
                else
                {
                    rigthBit = 1;
                }
                state = (state << 1) + rigthBit;
                result[i >> 3] = (byte)((result[i >> 3] << 1) + (byte)(state >> _size));
                ulong mask = 1; mask <<= _size;
                state &= mask - 1;
            }
            return result;
        }

        public byte[] Encrypte(byte[] file, byte[] key)
        {
            byte[] result = new byte[file.Length];
            for(int i = 0; i < file.Length; i++)
            {
                result[i] = (byte)(file[i] ^ key[i]);
            }
            return result;
        }

        public BigInteger Encrypte(BigInteger text, BigInteger key)
        {
            return text ^ key;
        }
    }
}
