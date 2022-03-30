using TheoryOfInformation.lab1.Structs;

namespace TheoryOfInformation.lab1.Encryptions.Models
{
    internal interface IEncryption
    {
        string Encrypte(string text, string key);
        string Decrypte(string text, string key);
        LangIds Lang { get; }
        LangIds KeyLang { get; }
    }
}
