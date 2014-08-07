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
    /// - choose to create 3 PDF's or merge together
    /// - PdfGenerator objects using named web.config section
    /// </summary>
    public partial class demo2 : System.Web.UI.Page
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

                    if (!String.IsNullOrEmpty(_texBox.Text) && _texBox.Text != "http://" && _texBox.TextMode != TextBoxMode.MultiLine)
                    {
                        if (!_texBox.Text.StartsWith("http://") && !_texBox.Text.StartsWith("https://"))
                        {
                            _texBox.Text = "http://" + _texBox.Text;
                        }

                        _sources.Add(_texBox.Text);
                    }
                }
            }

            fld_results.Text = string.Empty;

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
            using (PdfGenerator _pdfGenerator = new PdfGenerator("publisher1"))
            {
                _pdfGenerator.OnEnd += pdfGenerator_OnEnd;
                
                foreach (String _source in _sources)
                {
                    _pdfGenerator.AddSource(_source);
                }

                _pdfGenerator.GeneratePDF("demo-2-single.pdf");
            }
            
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
                using (PdfGenerator _pdfGenerator = new PdfGenerator("publisher1"))
                {
                    _pdfGenerator.OnEnd += pdfGenerator_OnEnd;
                    _pdfGenerator.AddSource(_source).GeneratePDF("demo-2(" + _count + ").pdf");

                    _count++;
                }
            }
        }

        void pdfGenerator_OnEnd(PdfGenerator sender)
        {
            fld_results.Text += sender.Result.FilePath + Environment.NewLine;
            fld_results.Text += "Size:" + sender.Result.Bytes.Length + Environment.NewLine;
            fld_results.Text += "Time:" + sender.Result.Duration + Environment.NewLine + Environment.NewLine;

        }
    }
}