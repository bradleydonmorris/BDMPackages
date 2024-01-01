using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace BDMCommandLine
{
	public class AssetVersions : IEnumerable<AssetVersion>
	{
		public AssetVersions() { }

		private readonly List<AssetVersion> _AssetVersions = [];

		public IEnumerator<AssetVersion> GetEnumerator()
			=> this._AssetVersions.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator()
			=> this._AssetVersions.GetEnumerator();

		public AssetVersion this[Int32 index] => this._AssetVersions[index];
		public AssetVersion? this[String name] => this.Get(name);

		public Int32 Count => this._AssetVersions.Count;

		public Int32 IndexOf(String name)
			=> this._AssetVersions.FindIndex(c => c.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));

		public Boolean Contains(String name)
			=> this._AssetVersions.Any(c => c.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));

		public AssetVersion? Get(String name)
			=> this._AssetVersions.FirstOrDefault(c => c.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));

		public void Add(AssetVersion value)
			=> this._AssetVersions.Add(value);

		public void AddRange(IEnumerable<AssetVersion> values)
			=> this._AssetVersions.AddRange(values);

		public Boolean TryGet(String name, out AssetVersion? result)
		{
			Boolean returnValue;
			if (this.Contains(name) && this[name] is AssetVersion assetVersion)
			{
				result = assetVersion;
				returnValue = true;
			}
			else
			{
				result = null;
				returnValue = false;
			}
			return returnValue;
		}

		public void Clear()
			=> this._AssetVersions.Clear();

		public void Remove(String name)
		{
			if (this.Contains(name) && this[name] is AssetVersion assetVersion)
				this._AssetVersions.Remove(assetVersion);
		}
		public void Replace(AssetVersion assetVersion)
		{
			if (this.Contains(assetVersion.Name))
				this.Remove(assetVersion.Name);
			this.Add(assetVersion);
		}
	}
}