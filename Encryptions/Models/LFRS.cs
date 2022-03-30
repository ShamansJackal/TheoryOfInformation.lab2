using System;

namespace TheoryOfInformation.lab1.Encryptions.Models
{
    public class LFRS : IEncryption
    {
        private int _size;
        public LFRS(ulong manyDicks, int power)
        {
            _size = power;
        }
        public string BuildKey(ulong beginState, ushort length)
        {
            throw new NotImplementedException();
        }

        public string Decrypte(string text, string key)
        {
            throw new NotImplementedException();
        }

        public string Encrypte(string text, string key)
        {
            throw new NotImplementedException();
        }
    }
}
