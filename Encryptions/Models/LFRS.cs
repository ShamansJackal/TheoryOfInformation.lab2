using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using static TheoryOfInformation.lab1.Encryptions.TextWorker;

namespace TheoryOfInformation.lab1.Encryptions.Models
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
        public string BuildKey(ulong beginState, ushort length)
        {
            if (beginState > (ulong)((1 << (_size + 1)) - 1)) throw new Exception("число выходит за регистер");

            StringBuilder builder = new StringBuilder();
            ulong state = beginState;
            for (ushort i = 0; i < length; i++)
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

        public byte[] Encrypte(byte[] file, byte[] key)
        {
            throw new NotImplementedException();
        }

        public BigInteger Encrypte(BigInteger text, BigInteger key)
        {
            throw new NotImplementedException();
        }
    }
}
