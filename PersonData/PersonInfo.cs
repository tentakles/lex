using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersonData
{
	public class PersonInfo
	{
		public string Name;
		public string Ssn;
		public List<string> Telephone;
		public string Adress;
		public string City;

		public  PersonInfo()
		{
			Telephone = new List<string>();
		}

		public string TelephoneAsString()
		{
			string result = "";
			foreach (var i in Telephone)
			{
				if (result != "")
					result += ", ";
				result += i;
			}
			return result;
		}

	}

}
