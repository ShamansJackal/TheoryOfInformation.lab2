using System.Numerics;

namespace TheoryOfInformation.lab2.Encryptions.Models
{
    public interface IEncryption
    {
        string BuildKey(ulong beginState, ulong length);
        byte[] BuildKeyForFile(ulong beginState, ulong lengthOfBytes);
        byte[] Encrypte(byte[] file, byte[] key);
        BigInteger Encrypte(BigInteger text, BigInteger key);
    }
}
