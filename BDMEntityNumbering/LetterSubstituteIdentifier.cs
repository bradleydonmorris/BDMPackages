﻿namespace BDMEntityNumbering
{
    public class LetterSubstituteIdentifier
    {
        public String Prefix { get; set; }
        public String Suffix { get; set; }
        public Int16 EntitySegment { get; set; }
        public Int16 SystemSegment { get; set; }
        public Int32 LastUsedIdentifier { get; set; }

        public LetterSubstituteIdentifier()
        {
            this.Prefix = String.Empty;
            this.Suffix = String.Empty;
            this.EntitySegment = 1;
            this.SystemSegment = 1;
            this.LastUsedIdentifier = 0;
        }

        public LetterSubstituteIdentifier(String prefix, String suffix, Int16 entity, Int16 system, Int32 lastUsedIdentifier)
        {
            this.Prefix = prefix;
            this.Suffix = suffix;
            this.EntitySegment = entity;
            this.SystemSegment = system;
            this.LastUsedIdentifier = lastUsedIdentifier;
        }

        public LetterSubstituteIdentifier(String prefix, String suffix, String value)
        {
            this.Prefix = prefix;
            this.Suffix = suffix;
            if (!String.IsNullOrEmpty(this.Prefix))
                value = value[this.Prefix.Length..];
            if (!String.IsNullOrEmpty(this.Suffix))
                value = value[..^this.Suffix.Length];
            String entity = value[..4];
            String system = value.Substring(4, 4);
            String identifier = value[8..];
            this.EntitySegment = entity.HexToInt16();
            this.SystemSegment = system.HexToInt16();
            this.LastUsedIdentifier = identifier.HexToInt16();
        }

        public String GetNext()
        {
            this.LastUsedIdentifier ++;
            return this.Prefix
                + this.EntitySegment.ToHexidecimal()
                + this.SystemSegment.ToHexidecimal()
                + this.LastUsedIdentifier.ToHexidecimal()
                + this.Suffix;
        }
    }
}