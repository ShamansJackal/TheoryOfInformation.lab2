namespace TheoryOfInformation.lab1.Encryptions.Models
{
    public interface IEncryption
    {
        string Encrypte(string text, string key);
        string Decrypte(string text, string key);
        string BuildKey(long beginState);
    }
}
