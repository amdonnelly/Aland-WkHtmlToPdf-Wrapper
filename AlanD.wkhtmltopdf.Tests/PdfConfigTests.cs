using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using AlanD.wkhtmltopdf;
using AlanD.wkhtmltopdf.Utils;
using AlanD.EventHandlers;

namespace AlanD.wkhtmltopdf.Tests
{
    [TestClass]
    public class PdfConfigTests
    {


        [TestMethod]
        [TestCategory("PDF Config")]
        public void Basic_PdfGenerator_should_use_default_webConfig_section()
        {
            string _outcome1 = "--collate --copies \"1\"";

            PdfGenerator _pdfGenerator = new PdfGenerator();

            Assert.AreEqual(_outcome1, _pdfGenerator.Config.GetCommandArguments());
            Assert.AreEqual(Enumerators.ErrorMode.Default, _pdfGenerator.Config.GetExeErrorMode);
            Assert.AreEqual("\\bin\\wkhtmltopdf.exe", _pdfGenerator.Config.GetExeLocation);
            Assert.AreEqual("\\pdf-output\\", _pdfGenerator.Config.GetExeOutputDirectory);
            Assert.AreEqual("\\pdf-temp\\", _pdfGenerator.Config.GetExeWorkingDirectory);
                
        }

        [TestMethod]
        [TestCategory("PDF Config")]
        public void NamedConfig_PdfGenerator_should_use_provided_webConfig_section()
        {
            string _outcome1 = "--collate --copies \"1\" -s \"A4\" --background";

            PdfGenerator _pdfGenerator = new PdfGenerator("publisher1");
            Assert.AreEqual(_outcome1, _pdfGenerator.Config.GetCommandArguments());


            Assert.AreEqual(Enumerators.ErrorMode.Verbose, _pdfGenerator.Config.GetExeErrorMode);
            Assert.AreEqual("../../../Binaries/wkhtmltopdf 0.12.1/wkhtmltopdf.exe", _pdfGenerator.Config.GetExeLocation);
            Assert.AreEqual("../../pdf-output", _pdfGenerator.Config.GetExeOutputDirectory);
            Assert.AreEqual("../../pdf-output/temp", _pdfGenerator.Config.GetExeWorkingDirectory);
        }


        [TestMethod]
        [TestCategory("PDF Config")]
        public void Single_options_and_grouped_options_should_be_merged()
        {

            string _outcome1 = "--collate -s \"A4\" --background -n";
            string _outcome2 = "--page-width \"100\" --title";

            PdfGenerator _pdfGenerator = new PdfGenerator("publisher1");

            _pdfGenerator.Config.AddOption("--disable-javascript").RemoveOption("--copies");
            Assert.AreEqual( _outcome1, _pdfGenerator.Config.GetCommandArguments());

            _pdfGenerator.Config.SetOptions("--page-width 100 --title");
            Assert.AreEqual( _outcome2, _pdfGenerator.Config.GetCommandArguments());
        }
    }
}
