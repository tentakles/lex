using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace PersonData
{
    public class PersonInfoProvider
    {
	    
	    public static PersonInfo GetPerson(string p)
	    {
		    if (p.Length != 10 && p.Length != 11)
			    return null;

		    using (var mycontext = LexDb.LexUtil.LexContext) {
			    if (!p.Contains("-"))
				    p = p.Substring(0, 6) + "-" + p.Substring(6, 4);

			    var data = mycontext.Lexes.FirstOrDefault(x => x.Ssn == p && x.Name != null);
			    if (data != null) {
				    var result = new PersonInfo {
					    Adress = data.NewAddress,
					    City = data.NewPostcode,
					    Name = data.Name,
					    Ssn = data.Ssn,
					    Telephone = new List<string> { data.Telephone }
				    };
				    return result;
			    }
		    }

		    var person = GetPersonInfo(p);

		    if (person != null)
		    {
			    StorePerson(person);
		    }
		    else
		    {
			    MarkNotFound(p);
		    }

		    return person;
	    }

	    private static void MarkNotFound(string p)
	    {
		    using (var mycontext = LexDb.LexUtil.LexContext)
		    {
			    var data = mycontext.Lexes.FirstOrDefault(x => x.Ssn == p && x.Name != null);
			    if (data != null)
			    {
				    data.Name = "-";
				    mycontext.SaveChanges();
			    }
		    }

	    }

	    private static void StorePerson(PersonInfo person)
	    {
		    using (var mycontext = LexDb.LexUtil.LexContext) {

			    var data = mycontext.Lexes.FirstOrDefault(x => x.Ssn == person.Ssn);

			    if (data != null) {
				    data.Name = person.Name;
				    data.NewAddress = person.Adress;
				    data.NewPostcode = person.City;
				    data.Telephone = person.TelephoneAsString();

				    mycontext.SaveChanges();
			    }
		    }
	    }


	    public static PersonInfo GetPersonInfo(string personnummer)
	    {
		    const string urlprefix ="http://www.merinfo.se/search/search?who=" ;
		  //  const string urlprefix = "http://localhost/lex/olle.html?p=";
		   
		    personnummer = personnummer.Replace("-", "");
		    personnummer = personnummer.Replace(" ", "");

		    if (personnummer.Length != 10)
			    return null;

		    using (var client = new WebClient())
		    {
			    try
			    {
				    client.Encoding = System.Text.Encoding.UTF8;
     
				    var result = new PersonInfo();

				    var response = client.DownloadString(urlprefix + personnummer);

				    string[] htmlArray = response.Split('\n');

				    result.Name = GetMyRow("<p class=\"name\">",htmlArray);
				    result.Adress = GetMyRow("<p class=\"street\">", htmlArray);
				    result.City = GetMyRow("<p class=\"city\">", htmlArray);
				    result.Ssn = personnummer.Substring(0,6)+"-"+personnummer.Substring(6,4);
				    result.Telephone = new List<string>();
				    var tele = GetMyRow("<abbr title=\"Telefonnummer\">", htmlArray);

				    result.Telephone.Add(tele.Replace("Tel:","").Trim());

				    if (string.IsNullOrEmpty(result.Name))
					    return null;

				    return result;
			    }
			    catch (Exception e)
			    {
				    return null;
			    }
		    }
	    }

	    private static string GetMyRow(string p, string[] htmlArray)
	    {
		    foreach (var s in htmlArray)
		    {
			    if (s.Contains(p))
			    {
				    var result =  Regex.Replace(s, "<.*?>", "", RegexOptions.IgnoreCase);
				    result = result.Replace("\t", "");
				    return result.Trim();
			    }
		    }
		    return "";
	    }
    }
}
