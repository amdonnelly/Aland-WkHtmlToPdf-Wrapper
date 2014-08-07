using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using AlanD;
using AlanD.wkhtmltopdf.Utils;

namespace AlanD.wkhtmltopdf.TestSite
{
    /// <summary>
    /// Basic example using URL source
    /// - choose to create 3 PDF's or merge together
    /// </summary>
    public partial class demo1 : System.Web.UI.Page
    {
        #region not important
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                fld_source_1.Attributes.Add("placeholder", "http://");
                fld_source_2.Attributes.Add("placeholder", "http://");
                fld_source_3.Attributes.Add("placeholder", "http://");
            }
        }

        public void Btn_Submit_Click(object sender, EventArgs e)
        {
            List<string> _sources = new List<string>();

            foreach (Control _control in form1.Controls)
            {
                if (_control is TextBox)
                {
                    TextBox _texBox = (TextBox)_control;

                    if (!String.IsNullOrEmpty(_texBox.Text) && _texBox.Text != "http://")
                    {
                        if (!_texBox.Text.StartsWith("http://") && !_texBox.Text.StartsWith("https://"))
                        {
                            _texBox.Text = "http://" + _texBox.Text;
                        }

                        _sources.Add(_texBox.Text);
                    }
                }
            }

            if (chk_single.Checked)
            {
                CreatePDF(_sources);
            }
            else
            {
                CreatePDFs(_sources);
            }
        }
        #endregion

        /// <summary>
        /// Generate single PDF from a list of URL's
        /// </summary>
        /// <param name="_sources">List or URL's</param>
        void CreatePDF(List<string> _sources)
        {
            PdfGenerator _pdfGenerator = new PdfGenerator();
            _pdfGenerator.Config.SetExeLocation("../Binaries/wkhtmltopdf 0.12.1/wkhtmltopdf.exe");
            _pdfGenerator.Config.SetExeWorkingDirectory("~/pdf-output/temp");
            _pdfGenerator.Config.SetExeOutputDirectory("~/pdf-output");
            foreach (String _source in _sources)
            {
                _pdfGenerator.AddSource(_source);
            }

            byte[] _result = _pdfGenerator.GeneratePDF("demo-1-single.pdf");
            
        }

        /// <summary>
        /// Generate multiple PDF from a list of URL's
        ///  - with method chaining
        /// </summary>
        /// <param name="_sources">List or URL's</param>
        void CreatePDFs(List<string> _sources)
        {
            int _count = 1;

            foreach (String _source in _sources)
            {
                PdfGenerator _pdfGenerator = new PdfGenerator();
                _pdfGenerator.Config.SetExeLocation("../Binaries/wkhtmltopdf 0.12.1/wkhtmltopdf.exe").SetExeWorkingDirectory("~/pdf-output/temp").SetExeOutputDirectory("~/pdf-output");

                byte[] _result = _pdfGenerator.AddSource(_source).GeneratePDF("demo-1(" + _count + ").pdf");

                _count++;
            }
        }

    }
}