using System;
using System.Linq;
using System.Security.Cryptography;

namespace BDMEntityNumbering
{
    public static class LetterSubsituteSets
    {
        public static String[] CreateNewSetWithoutVisualAmbiguity() => LetterSubsituteSets.CreateNewSet("abcdefghjkmnpqrstuvwxyz23456789");
        public static String[] CreateNewSet() => LetterSubsituteSets.CreateNewSet("abcdefghijklmnopqrstuvwxyz0123456789");
        public static String[] CreateNewSet(String acceptableCharacters)
        {
            String[] returnValue = new String[256];
            for (Int32 index = 0; index < 256; index++)
            {
                Boolean isFound = true;
                while (isFound)
                {
                    String replacement =
                        acceptableCharacters[RandomNumberGenerator.GetInt32(acceptableCharacters.Length)].ToString()
                        + acceptableCharacters[RandomNumberGenerator.GetInt32(acceptableCharacters.Length)].ToString();
                    if (!Array.Exists(returnValue, element => element == replacement))
                    {
                        returnValue[index] = replacement;
                        isFound = false;
                    }
                }
            }
            return returnValue;
        }

        public static String GetRandom2Bytes(String[] replacementSet) => (
                replacementSet[RandomNumberGenerator.GetInt32(255)].ToString()
                + replacementSet[RandomNumberGenerator.GetInt32(255)].ToString()
        );

        public static String GetRandom2Bytes(String replacements) =>
            LetterSubsituteSets.GetRandom2Bytes(LetterSubsituteSets.ParseSet(replacements));

        public static String[] ParseSet(String replacements)
        {
            if (replacements.Length != 512)
                throw new ArgumentOutOfRangeException(nameof(replacements), "replacements string must contain exactly 512 characters");
            return Enumerable.Range(0, replacements.Length / 2).Select(x => replacements.Substring(x * 2, 2)).ToArray();
        }

        public static String JoinSet(String[] replacements) => String.Join("", replacements);
    }
}
