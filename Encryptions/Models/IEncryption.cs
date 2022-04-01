using System.Numerics;

namespace TheoryOfInformation.lab1.Encryptions.Models
{
    public interface IEncryption
    {
        string BuildKey(ulong beginState, ushort length);
        byte[] Encrypte(byte[] file, byte[] key);
        BigInteger Encrypte(BigInteger text, BigInteger key);
    }
}
