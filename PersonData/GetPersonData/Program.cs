using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Collections.Specialized;
using System.IO;
using System.Text.RegularExpressions;

namespace post
{
	class Program
	{
		static void Main(string[] args)
		{
			List<string> personnummer = new List<string>();

			var reader = new StreamReader(File.OpenRead("lexbase.txt"), Encoding.UTF8);

			// Kollar hur många rader vi har så vi kan typ starta där vi sluta. 
			var lineCount = File.ReadLines("output.txt").Count();

			// Läs in personnummer ifrån lexbase.txt 
			while (!reader.EndOfStream) {
				var line = reader.ReadLine();
				var values = line.Split(';');

				personnummer.Add(values[2]);
			}

			using (var client = new WebClient()) {
				using (var streamWriter = new StreamWriter("output.txt", true)) {
					for (int i = lineCount; i < personnummer.Count; i++) {
						var personNr = personnummer[i];

						// alla personnnummer har inte 4 sista siffror 
						if (personNr.Length < 8)
							continue;

						var response = client.DownloadString("http://www.merinfo.se/search/search?who=" + personNr);
						string[] htmlArray = response.Split('\n');

						//Namnet förekommer på rad 707 i html koden 
						string rad = htmlArray[707].Replace("\t", "");

						var namn = Regex.Replace(rad, "<.*?>", "", RegexOptions.IgnoreCase);

						var prn_namn = String.Format("{0}; {1}", personNr, namn);
						streamWriter.WriteLine(prn_namn);
						streamWriter.Flush();
						Console.WriteLine(prn_namn);
					}
				}
			}
		}
	}
}