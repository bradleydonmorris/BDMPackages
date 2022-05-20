using System;
using System.Linq;
using System.Security.Cryptography;

namespace BDMEntityNumbering
{
    public class LetterSubstituteIdentifier
    {
        public String[] Replacements { get; set; }

        public String ReplacementsString => LetterSubsituteSets.JoinSet(this.Replacements);

        public Int16 EntityIdentifier { get; set; }
        public Int16 SystemIdentifier { get; set; }
        public Int32 LastUsedIdentifier { get; set; }

        public String EntityCode { get; set; }
        public String SystemCode { get; set; }
        public String LastUsedCode { get; set; }

        public String LastUsed => $"{this.EntityCode}{this.SystemCode}{this.LastUsedCode}";


        public LetterSubstituteIdentifier()
        {
            this.Replacements = new String[256] { "yb", "se", "kg", "s3", "ec", "df", "md", "yc", "2z", "wn", "t6", "az", "m4", "qz", "bz", "de", "dm", "62", "14", "x0", "tl", "pp", "hj", "zp", "ep", "d0", "nm", "6q", "vy", "ic", "sd", "dt", "ah", "qx", "c1", "u8", "al", "vo", "j7", "m7", "nu", "j2", "xf", "q8", "f4", "q5", "e7", "u3", "n5", "d3", "c4", "yi", "4g", "nf", "1w", "bd", "j3", "b8", "of", "u5", "b2", "q4", "l3", "2f", "zm", "e1", "1h", "7k", "oq", "ro", "yr", "s0", "4z", "7a", "6t", "z3", "am", "ot", "vd", "my", "r6", "46", "hq", "71", "y1", "ib", "4l", "5s", "0h", "10", "gw", "cj", "ni", "nv", "w1", "mm", "no", "ze", "tn", "ea", "bm", "kf", "0b", "ir", "kx", "4v", "pc", "1m", "hg", "pb", "mj", "4n", "1u", "4w", "n2", "4i", "ha", "0x", "gq", "wg", "k1", "dp", "ng", "po", "fh", "pr", "bk", "xb", "da", "m1", "36", "ti", "li", "rt", "fj", "zz", "pz", "mt", "vb", "7e", "xz", "hs", "i8", "p8", "34", "00", "3e", "io", "z7", "rf", "d5", "ls", "38", "fu", "a6", "b7", "qd", "e8", "lm", "6l", "0v", "d2", "ns", "25", "pv", "ga", "be", "k5", "id", "03", "5z", "o6", "kl", "3w", "gi", "q6", "zo", "0q", "2v", "dx", "sw", "r8", "o7", "td", "jv", "qe", "74", "g8", "ey", "2n", "p7", "6n", "2i", "ff", "yo", "15", "7w", "qh", "wf", "ov", "bq", "aw", "fn", "pe", "ps", "hl", "e2", "68", "2m", "y5", "1v", "t2", "jr", "u2", "ou", "bj", "qn", "5n", "6f", "33", "l0", "eq", "ym", "zk", "6s", "oo", "1q", "4a", "us", "r3", "gh", "di", "06", "ap", "1b", "pg", "2o", "ji", "va", "z6", "77", "ss", "nd", "l4", "h8", "mx", "tq", "7r", "3m", "j5", "hf", "dh", "kb", "yp", "j0", "6x" }; ;
            this.EntityIdentifier = 1;
            this.SystemIdentifier = 1;
            this.LastUsedIdentifier = 0;
        }

        public LetterSubstituteIdentifier(String[] replacementSet)
        {
            if (replacementSet.Length != 256)
                throw new ArgumentOutOfRangeException(nameof(replacementSet), "replacementSet array must contain exactly 256 elements of two characters");
            this.Replacements = replacementSet;
            this.EntityIdentifier = 1;
            this.SystemIdentifier = 1;
            this.LastUsedIdentifier = 0;
        }

        public LetterSubstituteIdentifier(String replacements)
            : this(LetterSubsituteSets.ParseSet(replacements))
        { }

        public LetterSubstituteIdentifier(String[] replacementSet, Int16 entity, Int16 system, Int32 lastUsed)
            : this(replacementSet)
        {
            this.SetEntity(entity);
            this.SetSystem(system);
            this.SetLastUsed(lastUsed);
        }

        public LetterSubstituteIdentifier(String replacements, Int16 entity, Int16 system, Int32 lastUsed)
            : this(LetterSubsituteSets.ParseSet(replacements), entity, system, lastUsed)
        { }

        public LetterSubstituteIdentifier(String[] replacementSet, String entity, String system, String lastUsed)
            : this(replacementSet)
        {
            this.SetEntity(entity);
            this.SetSystem(system);
            this.SetLastUsed(lastUsed);
        }

        public LetterSubstituteIdentifier(String replacements, String entity, String system, String lastUsed)
            : this(LetterSubsituteSets.ParseSet(replacements), entity, system, lastUsed)
        { }

        public LetterSubstituteIdentifier(String[] replacementSet, Int16 entity, Int16 system)
            : this(replacementSet)
        {
            this.SetEntity(entity);
            this.SetSystem(system);
            this.SetLastUsed(0);
        }

        public LetterSubstituteIdentifier(String replacements, Int16 entity, Int16 system)
            : this(LetterSubsituteSets.ParseSet(replacements), entity, system)
        { }

        public LetterSubstituteIdentifier(String[] replacementSet, String entity, String system)
            : this(replacementSet)
        {
            this.SetEntity(entity);
            this.SetSystem(system);
            this.SetLastUsed(0);
        }

        public LetterSubstituteIdentifier(String replacements, String entity, String system)
            : this(LetterSubsituteSets.ParseSet(replacements), entity, system)
        { }

        public LetterSubstituteIdentifier(String[] replacementSet, String value)
            : this(replacementSet)
        {
            if (value.Length != 16)
                throw new ArgumentOutOfRangeException(nameof(value), "value must be sixteen characters long");
            this.SetEntity(value[..4]);
            this.SetSystem(value[4..8]);
            this.SetLastUsed(value[^8..]);
        }

        public LetterSubstituteIdentifier(String replacements, String value)
            : this(LetterSubsituteSets.ParseSet(replacements), value[..4], value[4..8], value[^8..])
        {
            if (value.Length != 16)
                throw new ArgumentOutOfRangeException(nameof(value), "value must be sixteen characters long");
        }

        public void SetEntity(Int16 identifier)
        {
            this.EntityIdentifier = identifier;
            Byte[] identifierArray = BitConverter.GetBytes(identifier);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(identifierArray);
            this.EntityCode =
                this.Replacements[identifierArray[0]]
                + this.Replacements[identifierArray[1]];
        }

        public void SetEntity(String code)
        {
            if (code.Length != 4)
                throw new ArgumentOutOfRangeException(nameof(code), "code must be four characters long");
            this.EntityCode = code;
            Byte[] positionArray = new Byte[2];
            positionArray[0] = (Byte)Array.IndexOf(this.Replacements, code[..2]);
            positionArray[1] = (Byte)Array.IndexOf(this.Replacements, code[^2..]);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(positionArray);
            this.EntityIdentifier = BitConverter.ToInt16(positionArray);
        }

        public void SetSystem(Int16 identifier)
        {
            this.SystemIdentifier = identifier;
            Byte[] identifierArray = BitConverter.GetBytes(identifier);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(identifierArray);
            this.SystemCode =
                this.Replacements[identifierArray[0]]
                + this.Replacements[identifierArray[1]];
        }

        public void SetSystem(String code)
        {
            if (code.Length != 4)
                throw new ArgumentOutOfRangeException(nameof(code), "code must be four characters long");
            this.SystemCode = code;
            Byte[] positionArray = new Byte[2];
            positionArray[0] = (Byte)Array.IndexOf(this.Replacements, code[..2]);
            positionArray[1] = (Byte)Array.IndexOf(this.Replacements, code[^2..]);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(positionArray);
            this.SystemIdentifier = BitConverter.ToInt16(positionArray);
        }

        public void SetLastUsed(Int32 identifier)
        {
            this.LastUsedIdentifier = identifier;
            Byte[] identifierArray = BitConverter.GetBytes(identifier);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(identifierArray);
            this.LastUsedCode =
                this.Replacements[identifierArray[0]]
                + this.Replacements[identifierArray[1]]
                + this.Replacements[identifierArray[2]]
                + this.Replacements[identifierArray[3]];
        }

        public void SetLastUsed(String code)
        {
            if (code.Length != 8)
                throw new ArgumentOutOfRangeException(nameof(code), "code must be eight characters long");
            this.LastUsedCode = code;
            Byte[] positionArray = new Byte[4];
            positionArray[0] = (Byte)Array.IndexOf(this.Replacements, code[..2]);
            positionArray[1] = (Byte)Array.IndexOf(this.Replacements, code[2..4]);
            positionArray[2] = (Byte)Array.IndexOf(this.Replacements, code[4..6]);
            positionArray[3] = (Byte)Array.IndexOf(this.Replacements, code[^2..]);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(positionArray);
            this.LastUsedIdentifier = BitConverter.ToInt32(positionArray);
        }

        public String Increment()
        {
            this.LastUsedIdentifier ++;
            return this.LastUsed;
        }
   }
}