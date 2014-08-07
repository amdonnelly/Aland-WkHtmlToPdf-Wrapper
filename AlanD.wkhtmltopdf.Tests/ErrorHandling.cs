using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlanD.wkhtmltopdf.Utils;

namespace AlanD.wkhtmltopdf.Tests
{
    [TestClass]
    public class ErrorHandling
    {


        [TestMethod]
        [TestCategory("Error Handling")]
        public void Missing_exe_should_throw_custom_error()
        {
            string _testPage_url = Utils.Utils.ResolveFilePath("..\\..\\resources\\testpage\\index.html");
            string _message = string.Empty;

            PdfGenerator _pdfGenerator = new PdfGenerator("publisher1");
            _pdfGenerator.Config.SetErrorMode(Enumerators.ErrorMode.Default).SetExeLocation("..\\wkhtmltopdfMISSING.exe");
            _pdfGenerator.AddSource(_testPage_url);
            _pdfGenerator.OnError += delegate(PdfGenerator _o, string _m) { _message = _m; };

            _pdfGenerator.GeneratePDF("MissingExe.pdf");

            //default error message
            Assert.IsFalse(_message.Contains("The system cannot find the file specified"));

            //check custom error message is being displayed
            Assert.IsTrue(_message.Contains("Please supply path to wkhtmltopdf.exe"));
        }




        [TestMethod]
        [TestCategory("Error Handling")]
        public void Invalid_sourc_should_throw_error()
        {
            string _message = string.Empty;

            PdfGenerator _pdfGenerator = new PdfGenerator("publisher1");
            _pdfGenerator.Config.SetErrorMode(Enumerators.ErrorMode.Default);
            _pdfGenerator.AddSource("http://www.sdfjhsdflhsdfoiw.com");
            _pdfGenerator.OnError += delegate(PdfGenerator _o, string _m) { _message = _m; };

            _pdfGenerator.GeneratePDF("InvalidSource.pdf");

            Assert.IsTrue(_message.Contains("Exit with code 1 due to network error: HostNotFoundError"));
        }

        [TestMethod]
        [TestCategory("Error Handling")]
        public void Invalid_working_directory_should_throw_custom_error()
        {
            string _testPage_url = Utils.Utils.ResolveFilePath("..\\..\\resources\\testpage\\index.html");
            string _message = string.Empty;

            PdfGenerator _pdfGenerator = new PdfGenerator();
            _pdfGenerator.Config.SetErrorMode(Enumerators.ErrorMode.Default).SetExeLocation("..\\..\\..\\Binaries\\wkhtmltopdf 0.12.1\\wkhtmltopdf.exe").SetExeWorkingDirectory("fakefolder");
            _pdfGenerator.AddSource(_testPage_url);
            _pdfGenerator.OnError += delegate(PdfGenerator _o, string _m) { _message = _m; };

            _pdfGenerator.GeneratePDF("InvalidSource.pdf");


            Assert.IsFalse(_message.Contains("Please supply path to a valid temp/working directory"));


        }

        [TestMethod]
        [TestCategory("Error Handling")]
        public void Invalid_output_directory_should_throw_custom_error()
        {
            string _testPage_url = Utils.Utils.ResolveFilePath("..\\..\\resources\\testpage\\index.html");
            string _message = string.Empty;

            PdfGenerator _pdfGenerator = new PdfGenerator("publisher1");
            _pdfGenerator.Config.SetErrorMode(Enumerators.ErrorMode.Default).SetExeOutputDirectory("a:/output");
            _pdfGenerator.AddSource(_testPage_url);
            _pdfGenerator.OnError += delegate(PdfGenerator _o, string _m) { _message = _m; };

            _pdfGenerator.GeneratePDF("InvalidSource.pdf");


            Assert.IsFalse(String.IsNullOrEmpty(_message));
            Assert.IsTrue(_message.Contains("Output directory is invalid."));
        }

        [TestMethod]
        [TestCategory("Error Handling")]
        public void Empty_target_should_throw_custom_error()
        {
            string _testPage_url = Utils.Utils.ResolveFilePath("..\\..\\resources\\testpage\\index.html");
            string _message = string.Empty;

            PdfGenerator _pdfGenerator = new PdfGenerator("publisher1");
            _pdfGenerator.Config.SetErrorMode(Enumerators.ErrorMode.Default);
            _pdfGenerator.AddSource(_testPage_url);
            _pdfGenerator.OnError += delegate(PdfGenerator _o, string _m) { _message = _m; };

            _pdfGenerator.GeneratePDF("");

            Assert.IsFalse(String.IsNullOrEmpty(_message));
            Assert.IsTrue(_message.Contains("Please supply an output file name"));
        }

        [TestMethod]
        [TestCategory("Error Handling")]
        public void Missing_source_should_throw_custom_error()
        {
            string _message = string.Empty;

            PdfGenerator _pdfGenerator = new PdfGenerator("publisher1");
            _pdfGenerator.Config.SetErrorMode(Enumerators.ErrorMode.Default);
            _pdfGenerator.OnError += delegate(PdfGenerator _o, string _m) { _message = _m; };

            _pdfGenerator.GeneratePDF("");

            Assert.IsFalse(String.IsNullOrEmpty(_message));
            Assert.IsTrue(_message.Contains("You need to specify at least one input file"));
        }


        [TestMethod]
        [TestCategory("Error Handling")]
        public void Error_handling_set_to_verbose()
        {
            bool _complete = false;

            PdfGenerator _pdfGenerator = new PdfGenerator("publisher1");
            _pdfGenerator.Config.SetErrorMode(Enumerators.ErrorMode.Verbose);
            _pdfGenerator.Config.SetExeLocation("..\\..\\..\\Binaries\\wkhtmltopdf 0.12.1\\wkhtmltopdf.exe").SetExeWorkingDirectory("..\\..\\pdf-output\\temp").SetExeOutputDirectory("..\\..\\pdf-output");
            _pdfGenerator.AddHTMLSource("Test message");

            try
            {
                _pdfGenerator.GeneratePDF("");
                _complete = true;
            }
            catch (Exception _ex)
            {
                
            }

            Assert.IsFalse(_complete);
        } 
    }
}
