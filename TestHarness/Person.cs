using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SARTeam.Models
{
	public class Provider : EntityBase
	{
		[EntityKey]
		[JsonProperty("Name")]
		public String Name { get; set; }

		[JsonProperty("InformationLink")]
		public String? InformationLink { get; set; }

		public Provider()
			=> this.Name = String.Empty;
	}

	public class Person : EntityBase
	{
		[EntityKey]
		[JsonProperty("FullName")]
		public String FullName { get; set; }

		[EntityKey("Name")]
		[JsonProperty("FirstName")]
		public String FirstName { get; set; }

		[EntityKey("Name")]
		[JsonProperty("LastName")]
		public String LastName { get; set; }

		[JsonProperty("InformationLink")]
		public String? InformationLink { get; set; }

		public Person()
		{
			this.FirstName = String.Empty;
			this.LastName = String.Empty;
			this.FullName = String.Empty;
		}
	}
}
