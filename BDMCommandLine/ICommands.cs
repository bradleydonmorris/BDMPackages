using System;
using System.Collections.Generic;

namespace BDMCommandLine
{
	public interface ICommands : IEnumerable<ICommand>
	{
		public Int32 Count { get; }
		public Int32 IndexOf(String nameOrAlias);
		public Boolean Contains(String nameOrAlias);
		public ICommand? Get(String nameOrAlias);
		public void Add(ICommand value);
		public void AddRange(IEnumerable<ICommand> values);
		public Boolean TryGet(String nameOrAlias, out ICommand? result);
		public void Remove(String nameOrAlias);
	}
}