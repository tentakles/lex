using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexDb
{
	public class LexContext : DbContext
	{

		public LexContext()
		{

		}

		public LexContext(string connectionstring)
			: base(connectionstring)
		{

		}

		public DbSet<Lex> Lexes { get; set; }
	}
}
