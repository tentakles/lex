using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Spatial;

namespace LexDb
{
    public class Lex
    {
	    [Key]
	    public int LexId { get; set; }
	    public DbGeography Location { get; set; }
	    public string Ssn { get; set; }
	    public string Address { get; set; }
	    public string Postcode { get; set; }
	    public string NewAddress { get; set; }
	    public string NewPostcode { get; set; }
	    public string Name { get; set; }
	    public string Telephone { get; set; }
	    public string Country { get; set; }
    }
}
