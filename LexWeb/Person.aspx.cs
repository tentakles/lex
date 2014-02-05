using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PersonData;

namespace LexWeb
{
	public partial class Person : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			string p = Request.QueryString["p"];

			if(p!=null)
				MyPersonInfo = PersonInfoProvider.GetPerson(p);

			if(MyPersonInfo==null)
			{
				MyPersonInfo= new PersonInfo();
				
			}
		}
		
		public PersonInfo MyPersonInfo { get; set; }

	}
}