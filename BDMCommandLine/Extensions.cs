﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
