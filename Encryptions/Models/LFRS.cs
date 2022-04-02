using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using static TheoryOfInformation.lab2.Encryptions.TextWorker;

namespace TheoryOfInformation.lab2.Encryptions.Models
{
    public class LFRS : IEncryption
    {
        private int _size;
        private IEnumerable<ushort> _polyMorph;
        public LFRS(ushort[] manyDicks, ushort power)
        {
            if (manyDicks.Any(x => x > power)) throw new Exception("бит отвественный за формирование ключ должен быть меньше степени много члена");

            _size = power;
            _polyMorph = manyDicks.Select(x => (ushort)(x - 1));
        }
        public string BuildKey(ulong beginState, ulong length)
        {
            if (beginState > (ulong)((1 << (_size + 1)) - 1)) throw new Exception("число выходит за регистер");

            StringBuilder builder = new StringBuilder((int)length);
            ulong state = beginState;
            for (ulong i = 0; i < length; i++)
            {
                ushort rigthBit = 0;
                foreach (ushort item in _polyMorph)
                {
                    rigthBit += (ushort)((state & (ulong)(1 << item)) >> item);
                    if (rigthBit > 1)
                    {
                        rigthBit = 0;
                        break;
                    }
                }
                state = (state << 1) + rigthBit;
                builder = builder.Append((state & (ulong)(1 << _size)) > 0 ? "1" : "0");
                state ^= (ulong)(1 << _size);
            }
            return builder.ToString();
        }

        public byte[] BuildKeyForFile(ulong beginState, ulong lengthOfBytes)
        {
            if (beginState > (ulong)((1 << (_size + 1)) - 1)) throw new Exception("число выходит за регистер");

            byte[] result = new byte[lengthOfBytes];
            ulong state = beginState;
            for (ulong i = 0; i < lengthOfBytes << 3; i++)
            {
                ushort rigthBit = 0;
                foreach (ushort item in _polyMorph)
                {
                    rigthBit += (ushort)((state & (ulong)(1 << item)) >> item);
                    if (rigthBit > 1)
                    {
                        rigthBit = 0;
                        break;
                    }
                }
                state = (state << 1) + rigthBit;
                result[i >> 3] = (byte)((result[i >> 3] << 1) + (byte)(state >> _size));
                state &= (ulong)((1 << _size) - 1);
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
