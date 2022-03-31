using System.Collections.Generic;
using System.Linq;
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
    }
}