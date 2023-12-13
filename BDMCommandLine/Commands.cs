using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace BDMCommandLine
{
	public class Commands : ICommands
	{
		public Commands() { }

		private readonly List<ICommand> _Commands = [];

		public IEnumerator<ICommand> GetEnumerator()
			=> this._Commands.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator()
			=> this._Commands.GetEnumerator();

		public ICommand this[Int32 index] => this._Commands[index];
		public ICommand? this[String nameOrAlias] => this.Get(nameOrAlias);

		public Int32 Count => this._Commands.Count;

		public Int32 IndexOf(String nameOrAlias)
			=> this._Commands.FindIndex(c =>
			c.Name.Equals(nameOrAlias, StringComparison.InvariantCultureIgnoreCase)
			|| c.Aliases.Any(a => a.Equals(nameOrAlias, StringComparison.InvariantCultureIgnoreCase)));

		public Boolean Contains(String nameOrAlias)
			=> this._Commands.Any(c =>
			c.Name.Equals(nameOrAlias, StringComparison.InvariantCultureIgnoreCase)
			|| c.Aliases.Any(a => a.Equals(nameOrAlias, StringComparison.InvariantCultureIgnoreCase)));

		public ICommand? Get(String nameOrAlias)
			=> this._Commands.FirstOrDefault(c =>
			c.Name.Equals(nameOrAlias, StringComparison.InvariantCultureIgnoreCase)
			|| c.Aliases.Any(a => a.Equals(nameOrAlias, StringComparison.InvariantCultureIgnoreCase)));

		public void Add(ICommand value)
			=> this._Commands.Add(value);

		public void AddRange(IEnumerable<ICommand> values)
			=> this._Commands.AddRange(values);

		public Boolean TryGet(String nameOrAlias, out ICommand? result)
		{
			Boolean returnValue;
			if (this.Contains(nameOrAlias) && this[nameOrAlias] is ICommand command)
			{
				result = command;
				returnValue = true;
			}
			else
			{
				result = null;
				returnValue = false;
			}
			return returnValue;
		}

		public void Remove(String nameOrAlias)
		{
			if (this.Contains(nameOrAlias) && this[nameOrAlias] is ICommand command)
				this._Commands.Remove(command);
		}
		public void Replace(ICommand command)
		{
			if (this.Contains(command.Name))
				this.Remove(command.Name);
			this.Add(command);
		}
	}
}