using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AlanD.wkhtmltopdf.Config;
using AlanD.wkhtmltopdf.Utils;

namespace AlanD.wkhtmltopdf
{
    public class PdfConfig
    {
        string _exeLocation = "\\bin\\wkhtmltopdf.exe";
        string _exeArguments = String.Empty;
        string _workingDirectory = "\\pdf-temp\\";
        string _outputDirectory = "\\pdf-output\\";
        Enumerators.ErrorMode _errorMode;
        

        StringBuilder _arguments = new StringBuilder();

        protected Options Options;

        #region public properties
        /// <summary>
        /// Path to WKHTML exe
        /// </summary>
        public string GetExeLocation
        {
            get { return _exeLocation; }
        }
        /// <summary>
        /// Path to WKHTML working directory
        /// </summary>
        public string GetExeWorkingDirectory
        {
            get { return _workingDirectory; }
        }
        /// <summary>
        /// Path to PDF output folder
        /// </summary>
        public string GetExeOutputDirectory
        {
            get { return _outputDirectory; }
        }
        public Enumerators.ErrorMode GetExeErrorMode
        {
            get { return _errorMode; }
        }
        #endregion


        internal PdfConfig(String _configSectionName)
        {
            Init(_configSectionName);
        }

        internal PdfConfig() : this("") { }






        private void Init(String _configSectionName)
        {

            Options = new Options();

            _errorMode = Enumerators.ErrorMode.Default;

            
            Read_ConfigSection("default");                  //Read values from "default" section in web from web.config
            
            Options.ParseExeArguments(_exeArguments);

            if (!String.IsNullOrEmpty(_configSectionName))
            {
                Read_ConfigSection(_configSectionName);     
                Options.ParseExeArguments(_exeArguments);
            }            
        }

        /// <summary>
        /// Read selected section from web.config
        /// </summary>
        /// <param name="_configSectionName">Section Name</param>
        private void Read_ConfigSection(String _configSectionName)
        {
            if (!String.IsNullOrEmpty(_configSectionName))
            {
                AlanD.wkhtmltopdf.Config.ConfigurationSectionHandler _configSection = (AlanD.wkhtmltopdf.Config.ConfigurationSectionHandler)System.Configuration.ConfigurationManager.GetSection("AlanD-PDF");

                if (_configSection != null)
                {
                    foreach (AlanD.wkhtmltopdf.Config.PublisherGroupElement _publisherGroupCollection in _configSection.Items)
                    {
                        if (_publisherGroupCollection.Name == _configSectionName)
                        {
                            foreach (AlanD.wkhtmltopdf.Config.PublisherValueElement _publisherValueElement in _publisherGroupCollection.Values)
                            {
                                switch (_publisherValueElement.name)
                                {
                                    case "exeLocation":
                                        if (!String.IsNullOrEmpty(_publisherValueElement.value))
                                        {
                                            _exeLocation = _publisherValueElement.value;
                                        }
                                        break;
                                    case "exeArguments":
                                        if (!String.IsNullOrEmpty(_publisherValueElement.value))
                                        {
                                            _exeArguments = _publisherValueElement.value;
                                        }
                                        break;
                                    case "workingDirectory":
                                        if (!String.IsNullOrEmpty(_publisherValueElement.value))
                                        {
                                            _workingDirectory = _publisherValueElement.value;
                                        }
                                        break;
                                    case "outputDirectory":
                                        if (!String.IsNullOrEmpty(_publisherValueElement.value))
                                        {
                                            _outputDirectory = _publisherValueElement.value;
                                        }
                                        break;
                                    case "errorMode":
                                        if (!String.IsNullOrEmpty(_publisherValueElement.value))
                                        {
                                            string _value = _publisherValueElement.value.Trim().ToLower();

                                            _errorMode = (_value == "verbose") ? Enumerators.ErrorMode.Verbose : Enumerators.ErrorMode.Default;                                            
                                        }

                                        break;
                                }
                            }
                        }
                    }
                }
            }
        }



        #region Public methods
        /// <summary>
        /// Add new command line argument
        /// </summary>
        public PdfConfig AddOption(string _key)
        {
            return AddOption(_key, null);
        }
        /// <summary>
        /// Add new command line argument with value
        /// </summary>
        public PdfConfig AddOption(string _key, string _value)
        {
            if (!String.IsNullOrEmpty(_key))
            {
                Options.AddOption(_key, _value);
            }

            return this;
        }

        /// <summary>
        /// Overwrite current options with new set of command line arguments
        /// </summary>
        /// <param name="_arguments">string containing one or more switch/values</param>
        /// <returns></returns>
        public PdfConfig SetOptions(string _arguments)
        {
            Options.SetOptions(_arguments);

            return this;
        }
        /// <summary>
        /// Remove command line argument
        /// </summary>
        public PdfConfig RemoveOption(string _key)
        {
            if (!String.IsNullOrEmpty(_key))
            {
                Options.RemoveOption(_key);
            }

            return this;
        }

        /// <summary>
        /// Return command line arguments in a format ready to be passed to the exe
        /// </summary>
        /// <returns></returns>
        public string GetCommandArguments()
        {
            return Options.BuildCommandArguments().Trim();
        }

        /// <summary>
        /// Set WKHTML exe location
        /// </summary>
        public PdfConfig SetExeLocation(string _value)
        {
            if (!String.IsNullOrEmpty(_value))
            {
                _exeLocation = _value;
            }

            return this;
        }
        /// <summary>
        /// Set WKHTML working directory
        /// </summary>
        public PdfConfig SetExeWorkingDirectory(string _value)
        {
            if (!String.IsNullOrEmpty(_value))
            {
                _workingDirectory = _value;
            }

            return this;
        }
        /// <summary>
        /// Set PDF output directory
        /// </summary>
        public PdfConfig SetExeOutputDirectory(string _value)
        {
            if (!String.IsNullOrEmpty(_value))
            {
                _outputDirectory = _value;
            }

            return this;
        }
        public PdfConfig SetErrorMode(Enumerators.ErrorMode _value)
        {
            if (_value!=null)
            {
                _errorMode = _value;
            }

            return this;
        }

       
        #endregion

    }
}
