using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace TheoryOfInformation.lab1.Encryptions
{
    public delegate string Operation(string text, string key);

    public static class TextWorker
    {
        public static string IntToBin(ulong number)
        {
            StringBuilder builder = new StringBuilder(512);

            while (number > 0)
            {
                builder.Append(number & 1);
                number >>= 1;
            }
            return new string(builder.ToString().Reverse().ToArray());
        }

        public static string IntToBin(ulong number, uint count)
        {
            StringBuilder builder = new StringBuilder(512);

            for (uint i = 0; i < count; i++)
            {
                builder.Append(number & 1);
                number >>= 1;
            }
            return new string(builder.ToString().Reverse().ToArray());
        }

        public static string IntToBin(this BigInteger bigint)
        {
            var bytes = bigint.ToByteArray();
            var idx = bytes.Length - 1;
            var base2 = new StringBuilder(bytes.Length * 8);
            var binary = Convert.ToString(bytes[idx], 2);
            /*if (binary[0] != '0' && bigint.Sign == 1)
            {
                base2.Append('0');
            }*/
            base2.Append(binary);
            for (idx--; idx >= 0; idx--)
            {
                base2.Append(Convert.ToString(bytes[idx], 2).PadLeft(8, '0'));
            }

            return base2.ToString();
        }

        public static BigInteger BinToDec(string value)
        {
            BigInteger res = 0;

            foreach (char c in value)
            {
                res <<= 1;
                res += c == '1' ? 1 : 0;
            }

            return res;
        }
    }
}