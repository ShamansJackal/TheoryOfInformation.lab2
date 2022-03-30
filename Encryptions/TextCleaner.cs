using System.Collections.Generic;
using TheoryOfInformation.lab1.Structs;
using System.Linq;

namespace TheoryOfInformation.lab1.Encryptions
{
    public delegate string Operation(string text, string key);

    public static class TextCleaner
    {
        private static readonly Dictionary<LangIds, string> langs;

        static TextCleaner()
        {
            langs = new Dictionary<LangIds, string>
            {
                [LangIds.RU] = "широкая электрификация южных губерний даст мощный толчок подъёму сельского хозяйства",
                [LangIds.EN] = "the quick brown fox jumps over the lazy dog",
                [LangIds.NU] = "0123456789"
            };

            langs[LangIds.RU] = (langs[LangIds.RU] + langs[LangIds.RU].ToUpper()).Replace(" ", "");
            langs[LangIds.EN] = (langs[LangIds.EN] + langs[LangIds.EN].ToUpper()).Replace(" ", "");
        }

        public static string WorkWithText(string key, string text, Operation operation, LangIds lang)
        {
            List<RemovedSymbl> removedSymbls = CleanText(ref text, lang);
            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(text)) return null;
            text = text.ToLower();
            key = key.ToLower();

            string encodedTxt = operation(text, key);

            return ReturnText(encodedTxt, removedSymbls);
        }

        public static List<RemovedSymbl> CleanText(ref string text, LangIds lang)
        {
            List<RemovedSymbl> result = new List<RemovedSymbl>();
            for (int i = 0; i < text.Length; i++)
            {
                if (!langs[lang].Contains(text[i]))
                {
                    result.Add(new RemovedSymbl(i, text));
                    text = text.Remove(i, 1);
                    i--;
                }
            }

            return result;
        }

        private static string ReturnText(string text, List<RemovedSymbl> removedSymbls)
        {
            if (string.IsNullOrWhiteSpace(text)) return null;
            for (int i = removedSymbls.Count - 1; i >= 0; i--)
                text = text.Insert(removedSymbls[i].index, removedSymbls[i].symble.ToString());
            return text;
        }
    }
}