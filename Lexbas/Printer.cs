
using System;
using System.Diagnostics;
using System.IO;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace Lexbas
{
	/// <summary>
	/// This sample is the obligatory Hello World program.
	/// </summary>
	public class Printer
	{
		public static void Print(string[] args)
		{
			// Create a new PDF document
			PdfDocument document = new PdfDocument();
			document.Info.Title = "Created with PDFsharp";

			// Create an empty page
			PdfPage page = document.AddPage();


			// Get an XGraphics object for drawing
			XGraphics gfx = XGraphics.FromPdfPage(page);

			// Create a font
			XFont font = new XFont("Verdana", 20, XFontStyle.BoldItalic);

			// Draw the text
			gfx.DrawString("Hello, World!", font, XBrushes.Black,
			  new XRect(0, 0, page.Width, page.Height),
			  XStringFormats.TopCenter);


			// Save the document...
			const string filename = "HelloWorld.pdf";
			document.Save(filename);
			// ...and start a viewer.
			Process.Start(filename);
		}

		internal static void Print(System.Collections.Generic.List<LexDb.Lex> list,string header)
		{

			PdfDocument document = new PdfDocument();
			document.Info.Title = "Created with PDFsharp";

			// Create an empty page
			PdfPage page = document.AddPage();
			page.Size = PageSize.A4;

			// Get an XGraphics object for drawing
			XGraphics gfx = XGraphics.FromPdfPage(page);
			XTextFormatter tf = new XTextFormatter(gfx);


			// Create a font
			XFont font = new XFont("Verdana", 16, XFontStyle.Bold);
			
			int rowcount = 1;

			int leftpos = 40;
			int topoffset = 100;
			int width = 500;
			int height = 70;
			int numperpage = 10;

			printRow(leftpos, topoffset, 0, height, width, gfx, tf, header + " (" + list.Count +" st)", font);
			font = new XFont("Verdana", 12, XFontStyle.Regular);
			
			for (int i = 0; i < list.Count; i++) {
				// Draw the text
				//	  gfx.DrawString("Hello, World!", font, XBrushes.Black, new XRect(0, 0, page.Width, page.Height), XStringFormats.TopCenter);
				var p = list[i];
				string text = p.Name;


				text+= " (" + p.Ssn + ")";

				if(!string.IsNullOrEmpty(p.Telephone))
					text+=Environment.NewLine+"Tel:" +p.Telephone;

				text += Environment.NewLine + p.NewAddress;
				text += Environment.NewLine + p.NewPostcode;

				printRow(leftpos, topoffset, rowcount, height, width, gfx, tf, text, font);

				rowcount++;

				if (rowcount % numperpage == 0)
				{
					page = document.AddPage();
					page.Size = PageSize.A4;
					// Get an XGraphics object for drawing
					gfx = XGraphics.FromPdfPage(page);
					tf = new XTextFormatter(gfx);
					rowcount = 0;
				}
			}

			// Save the document...
			string filename = "Lexbas FörbrytaR" + Guid.NewGuid().ToString() + ".pdf";
			document.Save(filename);
			// ...and start a viewer.
			Process.Start(filename);
		}

		private static void printRow(int leftpos, int topoffset, int rowcount, int height, int width, XGraphics gfx,
		                             XTextFormatter tf, string text, XFont font)
		{
			XRect rect = new XRect(leftpos, topoffset + (rowcount*height), width, height);

			gfx.DrawRectangle(XBrushes.White, rect);

			//tf.Alignment = ParagraphAlignment.Left;

			tf.DrawString(text, font, XBrushes.Black, rect, XStringFormats.TopLeft);
			//tf.DrawString("HEJ", font, XBrushes.Black, rect, XStringFormats.TopLeft);
		}
	}
}