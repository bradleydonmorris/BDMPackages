using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BDMCommandLine
{
	public class AssetVersion
	{
		public AssetVersion() { }

		public AssetVersion(String name, String version, String description, String copyright, String infoURL)
		{
			this.Name = name;
			this.Version = version;
			this.Description = description;
			this.Copyright = copyright;
			this.InfoURL = infoURL;
		}

		public AssetVersion(FileVersionInfo fileVersionInfo)
		{
			if (fileVersionInfo is not null)
			{
				this.Name = fileVersionInfo.ProductName ?? String.Empty;
				this.Description = fileVersionInfo.Comments ?? String.Empty;
				this.Version = $"v{fileVersionInfo.ProductMajorPart}.{fileVersionInfo.ProductMinorPart}.{fileVersionInfo.ProductBuildPart}";
				this.Copyright = fileVersionInfo.LegalCopyright ?? String.Empty;
			}
		}

		public AssetVersion(FileVersionInfo fileVersionInfo, String infoURL)
			: this(fileVersionInfo)
			=> this.InfoURL = infoURL;

		public String Name { get; set; } = String.Empty;
		public String Version { get; set; } = String.Empty;
		public String Description { get; set; } = String.Empty;
		public String Copyright { get; set; } = String.Empty;
		public String InfoURL { get; set; } = String.Empty;
	}
}
