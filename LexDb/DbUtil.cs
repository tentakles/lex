using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexDb
{
	public class LexUtil
	{
		public static LexContext LexContext
		{
			get
			{
				string con = ConfigurationManager.ConnectionStrings["LexConnection"].ConnectionString;
				LexContext c = new LexContext(con);
				return c;
			}
		}


		public static void CreateDb()
		{
			var c = LexContext;

			c.Database.Delete();
			c.Database.CreateIfNotExists();
		}

	}
}
