using System;

namespace BDMCommandLine
{
	public static class Extensions
	{
		public static String Replicate(this String value, Int32 count)
		{
			String returnValue = String.Empty;
			for (Int32 loop = 1; loop <= count; loop++)
				returnValue += value;
			return returnValue;
		}
	}
}
