using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDMCommandLine
{
    public interface ICommandArgumentOption
	{
		public String Value { get; }
		public String Description { get; }
	}
}
