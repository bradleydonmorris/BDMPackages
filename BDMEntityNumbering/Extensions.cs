using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDMEntityNumbering
{
    public static class Extensions
    {
        public static String ToHexidecimal(this Int16 value)
        {
            Byte[] valueArray = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(valueArray);
            return BitConverter.ToString(valueArray).Replace("-", "");
        }

        public static String ToHexidecimal(this Int32 value)
        {
            Byte[] valueArray = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(valueArray);
            return BitConverter.ToString(valueArray).Replace("-", "");
        }

        public static String ToHexidecimal(this Int64 value)
        {
            Byte[] valueArray = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(valueArray);
            return BitConverter.ToString(valueArray).Replace("-", "");
        }

        public static Int16 HexToInt16(this String value)
        {
            List<Byte> list = new();
            for (Int32 index = 0; index < value.Length; index++)
            {
                String characters = value.Substring(index, 2);
                if (characters.Length > 0)
                    list.Add(Byte.Parse(characters, System.Globalization.NumberStyles.HexNumber));
                index++;
            }
            Byte[] valueArray = list.ToArray();
            if (BitConverter.IsLittleEndian)
                Array.Reverse(valueArray);
            return BitConverter.ToInt16(valueArray);
        }

        public static Int32 HexToInt32(this String value)
        {
            List<Byte> list = new();
            for (Int32 index = 0; index < value.Length; index++)
            {
                String characters = value.Substring(index, 2);
                if (characters.Length > 0)
                    list.Add(Byte.Parse(characters, System.Globalization.NumberStyles.HexNumber));
                index++;
            }
            Byte[] valueArray = list.ToArray();
            if (BitConverter.IsLittleEndian)
                Array.Reverse(valueArray);
            return BitConverter.ToInt32(valueArray);
        }

        public static Int64 HexToInt64(this String value)
        {
            List<Byte> list = new();
            for (Int32 index = 0; index < value.Length; index++)
            {
                String characters = value.Substring(index, 2);
                if (characters.Length > 0)
                    list.Add(Byte.Parse(characters, System.Globalization.NumberStyles.HexNumber));
                index++;
            }
            Byte[] valueArray = list.ToArray();
            if (BitConverter.IsLittleEndian)
                Array.Reverse(valueArray);
            return BitConverter.ToInt64(valueArray);
        }
    }
}
