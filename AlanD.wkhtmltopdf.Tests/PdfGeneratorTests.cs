using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlanD.wkhtmltopdf.Tests
{
    [TestClass]
    public class PdfGeneratorTests
    {
        [TestMethod]
        [TestCategory("PDF Generator")]
        public void Add_then_remove_source()
        {
            PdfGenerator _pdfGenerator = new PdfGenerator();
            _pdfGenerator.AddSource("c:/source1.txt");

            Assert.AreEqual("\"c:/source1.txt\"", _pdfGenerator.GetSourceList);

            _pdfGenerator.RemoveSource("c:/source1.txt").AddSource("http://www.test.com/source1.html, http://www.test.com/source2.html").AddSource("c:/source2.txt");

            Assert.AreEqual("\"http://www.test.com/source1.html\" \"http://www.test.com/source2.html\" \"c:/source2.txt\"", _pdfGenerator.GetSourceList);
        }
    }
}
