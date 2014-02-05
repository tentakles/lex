using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetPersonDataTest
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Ange personnummer: ");
				
			var personnummer = Console.ReadLine();
			var person = PersonData.PersonInfoProvider.GetPersonInfo(personnummer);
			
			if (person != null)
			{
				Console.WriteLine("Hittade person: ");
				Console.WriteLine("Namn: " + person.Name);
				Console.WriteLine("Personnummer: " + person.Ssn);
				Console.WriteLine("Adress: " + person.Adress);
				Console.WriteLine("Stad: " + person.City);
			
				Console.WriteLine("Telefonnummer: ");

				foreach (var tele in person.Telephone)
				{
					Console.WriteLine(tele);
				}
				
			}
			else
			{
				Console.WriteLine("Ingen träff");
			}

			Console.WriteLine("Tryck valfri tangent för att avsluta.");
			Console.ReadKey();
			


		}
	}
}
