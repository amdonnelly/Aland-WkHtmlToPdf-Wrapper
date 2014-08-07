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
    /// Advanced example using URL source
    /// - choose to create 1 PDF's or merge together
    /// - PdfGenerator objects using named web.config section
    /// - 1 URL source & 1 HMTML source
    /// </summary>
    public partial class demo3 : System.Web.UI.Page
    {
        #region not important
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                fld_source_1.Attributes.Add("placeholder", "http://");
            }
        }

        public void Btn_Submit_Click(object sender, EventArgs e)
        {

            foreach (Control _control in form1.Controls)
            {
                if (_control is TextBox)
                {
                    TextBox _texBox = (TextBox)_control;

                    if (!String.IsNullOrEmpty(_texBox.Text) && _texBox.Text != "http://" && _texBox.TextMode != TextBoxMode.MultiLine)
                    {
                        if (!_texBox.Text.StartsWith("http://") && !_texBox.Text.StartsWith("https://"))
                        {
                            _texBox.Text = "http://" + _texBox.Text;
                        }


                    }
                }
            }

            fld_results.Text = string.Empty;

            if (chk_single.Checked)
            {
                CreatePDF();
            }
            else
            {
                CreatePDFs();
            }
        }
        #endregion

        /// <summary>
        /// Generate single PDF
        ///  - PdfResult object returned
        /// </summary>
        void CreatePDF()
        {
            using (PdfGenerator _pdfGenerator = new PdfGenerator("publisher1"))
            {
                if (!String.IsNullOrEmpty(fld_source_1.Text))
                {
                    _pdfGenerator.AddSource(fld_source_1.Text);
                }
                if (!String.IsNullOrEmpty(fld_source_2.Text))
                {
                    _pdfGenerator.AddHTMLSource(fld_source_2.Text);
                }

                PdfResult _result = _pdfGenerator.GeneratePDF<PdfResult>("demo-3-single.pdf");

                if (_result != null)
                {
                    fld_results.Text += _result.FilePath + Environment.NewLine;
                    fld_results.Text += "Size:" + _result.Bytes.Length + Environment.NewLine;
                    fld_results.Text += "Time:" + _result.Duration + Environment.NewLine + Environment.NewLine;
                }
            }
            
        }

        /// <summary>
        /// Generate multiple PDFs
        ///  - with method chaining
        ///  - PdfResult object returned
        /// </summary>
        void CreatePDFs()
        {
            if (!String.IsNullOrEmpty(fld_source_1.Text))
            {
                using (PdfGenerator _pdfGenerator = new PdfGenerator("publisher1"))
                {
                    PdfResult _result = _pdfGenerator.AddSource(fld_source_1.Text).GeneratePDF<PdfResult>("demo-3(1).pdf");

                    if (_result != null)
                    {
                        fld_results.Text += _result.FilePath + Environment.NewLine;
                        fld_results.Text += "Size:" + _result.Bytes.Length + Environment.NewLine;
                        fld_results.Text += "Time:" + _result.Duration + Environment.NewLine + Environment.NewLine;
                    }
                }
            }


            if (!String.IsNullOrEmpty(fld_source_2.Text))
            {
                

                using (PdfGenerator _pdfGenerator = new PdfGenerator("publisher1"))
                {
                    PdfResult _result = _pdfGenerator.AddHTMLSource(fld_source_2.Text).GeneratePDF<PdfResult>("demo-3(2).pdf");

                    if (_result != null)
                    {
                        fld_results.Text += _result.FilePath + Environment.NewLine;
                        fld_results.Text += "Size:" + _result.Bytes.Length + Environment.NewLine;
                        fld_results.Text += "Time:" + _result.Duration + Environment.NewLine + Environment.NewLine;
                    }
                }
            }
        }


    }
}