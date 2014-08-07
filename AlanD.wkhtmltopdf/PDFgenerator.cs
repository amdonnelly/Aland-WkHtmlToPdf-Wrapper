using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.IO;

using System.Threading;
using System.Threading.Tasks;

using System.Web;

using AlanD.wkhtmltopdf.Utils;
using AlanD.EventHandlers;

namespace AlanD.wkhtmltopdf
{
    public class PdfGenerator : IDisposable
    {

        bool disposed = false;

        PdfConfig _config;
        PdfResult _result;

        List<string> _sources = new List<string>();             //list of sources
        List<string> _tempHTMLfiles = new List<string>();       //list of temporary HTML files
        string _targetFileName = String.Empty;                  //target PDF filename
        StringBuilder _progressMessage;


        public event OnStartEventHandler OnStart;
        public event OnProgressEventHandler OnProgress;
        public event OnErrorEventHandler OnError;
        public event OnEndEventHandler OnEnd;

        #region public properties
        /// <summary>
        /// Comma delimited list of HTML sources
        /// </summary>
        public string GetSourceList
        {
            get { return string.Join(" ", _sources.Select(item=> "\"" + item + "\"") ).Trim(); }
        }
        /// <summary>
        /// Target filename
        /// </summary>
        //public string GetTarget
        //{
        //    get { return _targetFileName; }
        //}
        public PdfConfig Config
        {
            get
            {
                return _config;
            }
        }
        public PdfResult Result
        {
            get
            {
                return _result;
            }
        }
        #endregion




        /// <summary>
        /// Create new PDF object using named web.config section
        /// </summary>
        /// <param name="_configSectionName">Name of the config section in web.config</param>
        public PdfGenerator(String _configSectionName)
        {
            Init(_configSectionName);
        }

        /// <summary>
        /// Create new PDF object
        /// </summary>
        public PdfGenerator() : this("") { }


        void Init(string _configSectionName)
        {
            _config = new PdfConfig(_configSectionName);

            //Setup default event actions
            OnStart += new OnStartEventHandler(OnStartAction);
            OnProgress += new OnProgressEventHandler(OnProgressEventAction);
            OnError += new OnErrorEventHandler(OnErrorEventAction);
            OnEnd += new OnEndEventHandler(OnEndEventAction);
        }


        #region Public Methods
        /// <summary>
        /// Generate PDF
        /// </summary>
        /// <param name="_fileName"></param>
        /// <returns>PDF as byte array</returns>
        public byte[] GeneratePDF(String _fileName)
        {

            GeneratePDF_Start(_fileName);

            return _result.Bytes;
        }

        /// <summary>
        /// Generate PDF 
        /// </summary>
        /// <typeparam name="T">PdfResult</typeparam>
        /// <param name="_fileName"></param>
        /// <returns>Result object</returns>
        public PdfResult GeneratePDF<T>(String _fileName) where T : PdfResult
        {

            GeneratePDF_Start(_fileName);

            return _result;
        }

        /// <summary>
        /// Add new local files or HTTP urls to be converted to PDF
        /// </summary>
        /// <param name="_value">Single or comma delimtted list of file paths/URL's</param>
        public PdfGenerator AddSource(string _value)
        {
            if (!String.IsNullOrEmpty(_value))
            {
                foreach (string _source in _value.Split(','))
                {
                    _sources.Add(_source.Trim());
                }
            }

            return this;
        }

        /// <summary>
        /// Add HTML source as a new page
        /// </summary>
        /// <param name="_value"></param>
        /// <returns>HTML source</returns>
        public PdfGenerator AddHTMLSource(string _value)
        {
            //Save HTML in a temporary file and add path to sources list
            //Keep a note of the temp file to be disposed of later

            if (!String.IsNullOrEmpty(_value))
            {
                //====================================================== Check working directory exists
                if (!Check_ExeWorkingDirectory(Utils.Utils.ResolveFilePath(_config.GetExeWorkingDirectory)))
                {
                    OnError.Invoke(this, "Please supply path to a valid temp/working directory");
                    return null;
                }

                string _newFilePath = (_config.GetExeWorkingDirectory.EndsWith("\\")) ? _config.GetExeWorkingDirectory : _config.GetExeWorkingDirectory + @"\";
                _newFilePath = Utils.Utils.ResolveFilePath(_newFilePath + _value.GetHashCode() + ".html");

                File.WriteAllText(@_newFilePath, _value);

                _sources.Add(_newFilePath);

                if (!_tempHTMLfiles.Contains(_newFilePath))
                {
                    _tempHTMLfiles.Add(_newFilePath);
                }
            }

            return this;
        }

        /// <summary>
        /// Remove local file path or URL from list of sources
        /// </summary>
        /// <param name="_value">Single or comma delimtted list of file paths/URL's</param>
        public PdfGenerator RemoveSource(string _value)
        {
            if (!String.IsNullOrEmpty(_value))
            {
                foreach (string _source in _value.Split(','))
                {
                    if (_sources.Contains(_source.Trim()))
                    {
                        _sources.Remove(_source.Trim());
                    }
                }
            }

            return this;
        }




        #endregion


        #region Main Task
        void GeneratePDF_Start(String _fileName)
        {
            _result = new PdfResult();

            if (!String.IsNullOrEmpty(_fileName))
            {
                _targetFileName = _fileName.Trim();
            }

            CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
            CancellationToken _cancellationToken = _cancellationTokenSource.Token;

            Task<byte[]> _task = Task<byte[]>.Factory.StartNew(() => GeneratePDF_Task(_fileName), _cancellationToken);

            Task.WaitAll(_task);
        }

        byte[] GeneratePDF_Task(String _fileName)
        {
            _result.StartTime = DateTime.Now;

            OnStart.Invoke(this);

            _progressMessage = new StringBuilder();


            string _targetFilePath = Utils.Utils.ResolveFilePath(_config.GetExeOutputDirectory);
            string _exeLocation = Utils.Utils.ResolveFilePath(_config.GetExeLocation);
            string _workingDirectory = Utils.Utils.ResolveFilePath(_config.GetExeWorkingDirectory);

            

            //====================================================== Check path to exe exists 
            if (!Check_ExeLocation(_exeLocation))
            {
                String _error = "Please supply path to wkhtmltopdf.exe ";
                if (!String.IsNullOrEmpty(_error))
                {
                    _error = _error + Environment.NewLine + "Currently set to " + _config.GetExeLocation;
                }

                OnError.Invoke(this, _error);
                return null;
            }

            

            //====================================================== Check working directory exists
            if (!Check_ExeWorkingDirectory(_workingDirectory))
            {
                OnError.Invoke(this, "Please supply path to a valid temp/working directory");
                return null;
            }

            //====================================================== check source/input
            if (!Check_Sources(GetSourceList))
            {
                OnError.Invoke(this, "You need to specify at least one input file");
                return null;
            }

            //====================================================== Check output directory exists
            if (!Check_OutputDirectory(_targetFilePath))
            {
                OnError.Invoke(this, "Output directory is invalid." + Environment.NewLine + "Currently set to " + _targetFilePath);
                return null;
            }

            

            if (String.IsNullOrEmpty(_targetFileName))
            {
                OnError.Invoke(this, "Please supply an output file name");
                return null;
            }

            


            _targetFilePath = (_targetFilePath.EndsWith("\\")) ? _targetFilePath + _targetFileName : _targetFilePath + "\\" + _targetFileName;

            //build command arguments
            StringBuilder _arguments = new StringBuilder();
            _arguments.Append(" " + _config.GetCommandArguments());
            _arguments.Append(" " + GetSourceList);
            _arguments.Append(" \"" + _targetFilePath + "\"");

            using (var process = new System.Diagnostics.Process()
            {
                StartInfo =
                {
                    FileName = _exeLocation,
                    Arguments = _arguments.ToString(),
                    UseShellExecute = false, 
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    RedirectStandardInput = true,
                    WorkingDirectory = _workingDirectory
                }
            })
            {


                try
                {
                    process.EnableRaisingEvents = true;
                    process.ErrorDataReceived += (o, e) => GeneratePDF_Task_Progress(e.Data) ;

                    process.Start();

                    process.BeginErrorReadLine();
                    process.WaitForExit();


                    if (process.ExitCode != 0)
                    {
                        if (!String.IsNullOrEmpty(_progressMessage.ToString()))
                        {
                            OnError.Invoke(this, _progressMessage.ToString());
                        }
                    }
                    else
                    {
                        

                        _result.EndTime = DateTime.Now;
                        _result.Bytes = System.IO.File.ReadAllBytes(_targetFilePath);
                        _result.FilePath = _targetFilePath;
                        _result.Duration = _result.EndTime - _result.StartTime;

                        OnEnd.Invoke(this);
                    }
                }
                catch (Exception ex)
                {
                    OnError.Invoke(this, ex.Message);
                }

                

                process.Close();
                process.Dispose();

            }


            return _result.Bytes;
        }

        void GeneratePDF_Task_Progress(string _message)
        {
            if (!String.IsNullOrEmpty(_message))
            {
                OnProgress.Invoke(this, _message);

                _progressMessage.Append(_message + Environment.NewLine);
            }
        }
        #endregion


        #region Main task checks
        bool Check_ExeLocation(string _exeLocation)
        {
            Boolean _isValid = false;

            if (!String.IsNullOrEmpty(_exeLocation))
            {
                if (File.Exists(_exeLocation))
                {
                    _isValid = true;
                }
            }

            return _isValid;
        }

        bool Check_ExeWorkingDirectory(string _directory)
        {
            Boolean _isValid = false;

            if (!String.IsNullOrEmpty(_directory))
            {
                if (!Directory.Exists(_directory))
                {//If directory doesn't exist, try and create then check again
                    try
                    {
                        Directory.CreateDirectory(_directory);
                    }
                    catch (Exception _ex)
                    {

                    }
                }
                else
                {
                    _isValid = true;
                }

                if (_isValid == false && Directory.Exists(_directory))
                {
                    _isValid = true;
                }
            }

            return _isValid;
        }

        bool Check_Sources(string _source)
        {
            Boolean _isValid = false;

            if (!string.IsNullOrEmpty(_source.Trim()))
            {
                _isValid = true;
            }

            return _isValid;
        }

        bool Check_OutputDirectory(string _targetFilePath)
        {
            Boolean _isValid = false;

            if (!String.IsNullOrEmpty(_targetFilePath))
            {
                if (!Directory.Exists(_targetFilePath))
                {//If directory doesn't exist, try and create then check again
                    try
                    {
                        Directory.CreateDirectory(_targetFilePath);
                    }
                    catch (Exception _ex)
                    {

                    }
                }
                else
                {
                    _isValid = true;
                }

                if (_isValid == false && Directory.Exists(_targetFilePath))
                {
                    _isValid = true;
                }
            }


            return _isValid;
        }
        #endregion


        #region Event Actions
        void OnStartAction(PdfGenerator _obj)
        {
        }
        void OnProgressEventAction(PdfGenerator _obj, string _message)
        {
        }
        void OnErrorEventAction(PdfGenerator _obj, string _message)
        {

            if (_config.GetExeErrorMode == Enumerators.ErrorMode.Verbose)
            {
                throw new Exception(_message);
            }
        }
        void OnEndEventAction(PdfGenerator _obj)
        {
        }

        #endregion


        #region dispose
 
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern. 
        protected void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                _sources = null;
                _targetFileName = null;
                _progressMessage = null;
            }


            //delete any temp HTML files
            foreach (string _file in _tempHTMLfiles)
            {
                File.Delete(_file);
            }

            disposed = true;
        }

        ~PdfGenerator()
           {
              Dispose(false);
           }
        #endregion
    }
}
