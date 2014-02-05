using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LexDb;
using PersonData;

namespace Lexbas
{
	internal class Program
	{

		private static string GetWellKnownTextFromCoords(double lat, double lon)
		{
			var wkt = "POINT(" + lon.ToString(CultureInfo.InvariantCulture) + " " + lat.ToString(CultureInfo.InvariantCulture) + ")";
			return wkt;
		}

		private static void Main(string[] args)
		{
		//	LexDb.LexUtil.CreateDb();
		//	readLex();
			//insertLex();
			Func<Lex, bool> p1 = x => x.Name != null && x.Postcode.StartsWith("118");
			//populateLexes(p1);

			printLexes(p1,"Farliga banditer, postkod 118 **");

			Console.WriteLine("Tryck valfri tangent för att avsluta.");
			Console.ReadKey();
		}

		private static void printLexes(Func<Lex, bool> p1,string header)
		{
			using (var mycontext = LexUtil.LexContext)
			{
				var data = mycontext.Lexes.Where(p1);

				Console.WriteLine("hittade utskrivbara: " + data.Count());

				
				Printer.Print(data.ToList(), header);
			}

		}

	

		private static void populateLexes(Func<Lex,bool> predicate)
		{
			using (var mycontext = LexUtil.LexContext)
			{
				var data = mycontext.Lexes.Where(predicate);
				
				Console.WriteLine("hittade cachningsbara: " + data.Count());
				
				foreach (var lex in data)
				{
					var mycachedperson = PersonInfoProvider.GetPerson(lex.Ssn);

					if (mycachedperson != null) {
						Console.WriteLine("cachade: " + mycachedperson.Name);
					}
				}
			}
		}

		private static void readLex()
		{
			using (var mycontext = LexDb.LexUtil.LexContext)
			{
				var data = mycontext.Lexes.Take(100);

				foreach (var lex in data)
				{
					Console.WriteLine(lex.Ssn);
				}
			}
		}

		private static void insertLex()
		{
			string[] lines = File.ReadAllLines(@"lexbase.txt", System.Text.Encoding.Default);
			
			using (var mycontext = LexDb.LexUtil.LexContext)
			{
				var list = new List<Lex>();

				int errorCount = 0;
				int bufferSize = 5000;

				for (int i = 1; i < lines.Count(); i++)
				{
					var cols = lines[i].Split(';');

					var lat = Double.Parse(cols[0].Trim(), CultureInfo.InvariantCulture);
					var lon = Double.Parse(cols[1].Trim(), CultureInfo.InvariantCulture);

					var ssn = cols[2].Trim();
					var address = cols[3].Trim();
					var postcode = cols[4].Trim();
					var country = cols[5].Trim();

					//Latitude;Longitude;Personnummer;Adress;Postort;Land
					try
					{
						var lex = new Lex
							{
								Address = address,
								Postcode = postcode,
								Ssn = ssn,
								Country = country,
								Location = DbGeography.FromText(GetWellKnownTextFromCoords(lat,lon))//, lon))
							};
						list.Add(lex);
					}
					catch (Exception e)
					{
						errorCount++;
					}

					if (i % bufferSize == 0)
					{
						
						saveList(mycontext, list, i);
					}
				}
				saveList(mycontext, list, lines.Count());
				Console.WriteLine("antal fel: "+errorCount);
			}
		}

		private static void saveList(LexContext mycontext, List<Lex> list, int i)
		{
			mycontext.Lexes.AddRange(list);
			mycontext.SaveChanges();
			list.Clear();
			Console.WriteLine("Antal sparade: " + i);
		}
	}
}

