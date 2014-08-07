using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlanD.wkhtmltopdf.Utils;

namespace AlanD.wkhtmltopdf.Tests
{
    [TestClass]
    public class Output
    {
        /// <summary>
        /// These test classes generate PDF's and compare them to pre-made examples
        /// </summary>

        [TestMethod]
        [TestCategory("PDF Output")]
        public void Output_1_standard()
        {
            string _pdfname = "test1-standard";

            string _testPage_url = Utils.Utils.ResolveFilePath("..\\..\\resources\\testpage\\index.html");

            string _comparisonPDF_url = Utils.Utils.ResolveFilePath("..\\..\\resources\\" + _pdfname + ".pdf");
            byte[] _comparisonPDF = System.IO.File.ReadAllBytes(_comparisonPDF_url);

            PdfGenerator _pdfGenerator = new PdfGenerator();
            _pdfGenerator.Config.SetExeLocation("..\\..\\..\\Binaries\\wkhtmltopdf 0.12.1\\wkhtmltopdf.exe").SetExeWorkingDirectory("..\\..\\pdf-output\\temp").SetExeOutputDirectory("..\\..\\pdf-output");
            _pdfGenerator.AddSource(_testPage_url);

            byte[] _result = _pdfGenerator.GeneratePDF(_pdfname + ".pdf");

            Assert.IsTrue(_comparisonPDF.Length > 0);
            Assert.AreEqual(_comparisonPDF.Length, _result.Length);

        }


        [TestMethod]
        [TestCategory("PDF Output")]
        public void Output_2_standard_grey()
        {
            string _pdfname = "test2-standard-grey";

            string _testPage_url = Utils.Utils.ResolveFilePath("..\\..\\resources\\testpage\\index.html");

            string _comparisonPDF_url = Utils.Utils.ResolveFilePath("..\\..\\resources\\" + _pdfname + ".pdf");
            byte[] _comparisonPDF = System.IO.File.ReadAllBytes(_comparisonPDF_url);

            PdfGenerator _pdfGenerator = new PdfGenerator();
            _pdfGenerator.Config.AddOption("--grayscale").SetExeLocation("..\\..\\..\\Binaries\\wkhtmltopdf 0.12.1\\wkhtmltopdf.exe").SetExeWorkingDirectory("..\\..\\pdf-output\\temp").SetExeOutputDirectory("..\\..\\pdf-output");
            _pdfGenerator.AddSource(_testPage_url);

            byte[] _result = _pdfGenerator.GeneratePDF(_pdfname + ".pdf");

            Assert.IsTrue(_comparisonPDF.Length > 0);
            Assert.AreEqual(_comparisonPDF.Length, _result.Length);

        }

        [TestMethod]
        [TestCategory("PDF Output")]
        public void Output_3_new_title()
        {
            string _pdfname = "test3-new-title";

            string _testPage_url = Utils.Utils.ResolveFilePath("..\\..\\resources\\testpage\\index.html");

            string _comparisonPDF_url = Utils.Utils.ResolveFilePath("..\\..\\resources\\" + _pdfname + ".pdf");
            byte[] _comparisonPDF = System.IO.File.ReadAllBytes(_comparisonPDF_url);

            PdfGenerator _pdfGenerator = new PdfGenerator();
            _pdfGenerator.Config.AddOption("--title", "new page title").SetExeLocation("..\\..\\..\\Binaries\\wkhtmltopdf 0.12.1\\wkhtmltopdf.exe").SetExeWorkingDirectory("..\\..\\pdf-output\\temp").SetExeOutputDirectory("..\\..\\pdf-output");
            _pdfGenerator.AddSource(_testPage_url);

            byte[] _result = _pdfGenerator.GeneratePDF(_pdfname + ".pdf");

            Assert.IsTrue(_comparisonPDF.Length > 0);
            Assert.AreEqual(_comparisonPDF.Length, _result.Length);

        }

        [TestMethod]
        [TestCategory("PDF Output")]
        public void Output_4_noScript()
        {
            string _pdfname = "test4-noscript";

            string _testPage_url = Utils.Utils.ResolveFilePath("..\\..\\resources\\testpage\\index.html");

            string _comparisonPDF_url = Utils.Utils.ResolveFilePath("..\\..\\resources\\" + _pdfname + ".pdf");
            byte[] _comparisonPDF = System.IO.File.ReadAllBytes(_comparisonPDF_url);

            PdfGenerator _pdfGenerator = new PdfGenerator();
            _pdfGenerator.Config.AddOption("-n").SetExeLocation("..\\..\\..\\Binaries\\wkhtmltopdf 0.12.1\\wkhtmltopdf.exe").SetExeWorkingDirectory("..\\..\\pdf-output\\temp").SetExeOutputDirectory("..\\..\\pdf-output");
            _pdfGenerator.AddSource(_testPage_url);

            byte[] _result = _pdfGenerator.GeneratePDF(_pdfname + ".pdf");

            Assert.IsTrue(_comparisonPDF.Length > 0);
            Assert.AreEqual(_comparisonPDF.Length, _result.Length);

        }

        [TestMethod]
        [TestCategory("PDF Output")]
        public void Output_5_printMedia()
        {
            string _pdfname = "test5-printmedia";

            string _testPage_url = Utils.Utils.ResolveFilePath("..\\..\\resources\\testpage\\index.html");

            string _comparisonPDF_url = Utils.Utils.ResolveFilePath("..\\..\\resources\\" + _pdfname + ".pdf");
            byte[] _comparisonPDF = System.IO.File.ReadAllBytes(_comparisonPDF_url);

            PdfGenerator _pdfGenerator = new PdfGenerator();
            _pdfGenerator.Config.AddOption("--print-media-type").SetExeLocation("..\\..\\..\\Binaries\\wkhtmltopdf 0.12.1\\wkhtmltopdf.exe").SetExeWorkingDirectory("..\\..\\pdf-output\\temp").SetExeOutputDirectory("..\\..\\pdf-output");
            _pdfGenerator.AddSource(_testPage_url);

            byte[] _result = _pdfGenerator.GeneratePDF(_pdfname + ".pdf");

            Assert.IsTrue(_comparisonPDF.Length > 0);
            Assert.AreEqual(_comparisonPDF.Length, _result.Length);

        }

        [TestMethod]
        [TestCategory("PDF Output")]
        public void Output_6_multiplePages()
        {
            string _pdfname = "test6-multiple-pages";

            string _testPage_url = Utils.Utils.ResolveFilePath("..\\..\\resources\\testpage\\index.html");
            string _testPage_url2 = Utils.Utils.ResolveFilePath("..\\..\\resources\\testpage\\index2.html");

            string _comparisonPDF_url = Utils.Utils.ResolveFilePath("..\\..\\resources\\" + _pdfname + ".pdf");
            byte[] _comparisonPDF = System.IO.File.ReadAllBytes(_comparisonPDF_url);

            PdfGenerator _pdfGenerator = new PdfGenerator("publisher1");
            _pdfGenerator.AddSource(_testPage_url).AddSource(_testPage_url2);

            

            byte[] _result = _pdfGenerator.GeneratePDF(_pdfname + ".pdf");

            Assert.IsTrue(_comparisonPDF.Length > 0);

            Assert.AreEqual(_comparisonPDF.Length, _result.Length);

        }


        /// <summary>
        /// Test generator with HTML string as source
        /// </summary>
        [TestMethod]
        [TestCategory("PDF Output")]
        public void Output_7_multipleHTML_source()
        {
            string _pdfname = "test7-multiple-html";

            string _testPage1 = System.IO.File.ReadAllText("..\\..\\resources\\testpage\\index3.html");

            string _comparisonPDF_url = Utils.Utils.ResolveFilePath("..\\..\\resources\\" + _pdfname + ".pdf");
            byte[] _comparisonPDF = System.IO.File.ReadAllBytes(_comparisonPDF_url);

            using (PdfGenerator _pdfGenerator = new PdfGenerator())
            {
                _pdfGenerator.Config.SetExeLocation("..\\..\\..\\Binaries\\wkhtmltopdf 0.12.1\\wkhtmltopdf.exe").SetExeWorkingDirectory("..\\..\\pdf-output\\temp").SetExeOutputDirectory("..\\..\\pdf-output");
                _pdfGenerator.AddHTMLSource(_testPage1).AddHTMLSource(_testPage1);

                byte[] _result = _pdfGenerator.GeneratePDF(_pdfname + ".pdf");

                Assert.IsTrue(_comparisonPDF.Length > 0);
                Assert.AreEqual(_comparisonPDF.Length, _result.Length);
            }
        }
    }
}
