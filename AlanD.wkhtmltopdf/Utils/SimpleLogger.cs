using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.Collections.Specialized;

using System.IO;
using System.Timers;
using System.Web;

namespace AlanD.wkhtmltopdf.Utils
{
    /// <summary>
    /// Basic logging class, outputs to .txt file
    /// Turned off by default, it can be enabled in the web.config
    /// </summary>
    public class SimpleLogger
    {
        private static bool _logEnabled;                //web.config value
        private static string _logFileURL;              //web.config value
        private static LogLevel _logLevel;              //web.config value

        private static bool _setup;                     //Used to ensure setup is only run once
        private static List<string> _messages;          //List of messages waiting to be written to the file

        public static String _message = String.Empty;
        static Timer _timer;
        static int _timerCount = 0;

        #region Singleton setup
        private static SimpleLogger instance;
        private SimpleLogger() { }

        public static SimpleLogger Instance
        {
            get
            {

                
                if (instance == null)
                {
                    instance = new SimpleLogger();
                }
                return instance;
            }
        }
        #endregion

        private static void CheckSetup(){
            if (_setup==null || _setup==false)
            {
                _logFileURL = String.Empty;
                _logLevel = LogLevel.ERROR;
                _setup = true;
                _messages = new List<string>();
                _timerCount = 0;

                //Get configurtaion settings
                var SimpleLoggerConfig = ConfigurationManager.GetSection("AlanD-SimpleLogger") as NameValueCollection;

                if (SimpleLoggerConfig != null)
                {
                    string _strEnabled = SimpleLoggerConfig["enabled"].ToString();
                    string _strLogfile = SimpleLoggerConfig["logfile"].ToString();
                    string _strLogLevel = SimpleLoggerConfig["level"].ToString();

                   
                    if (!String.IsNullOrEmpty(_strEnabled))
                    {
                        Boolean.TryParse(_strEnabled.ToLower(), out _logEnabled);
                    }

                    //=================================== setup debug level
                    if (!String.IsNullOrEmpty(_strLogLevel))
                    {
                        switch (_strLogLevel.ToUpper())
                        {
                            case "INFO":
                                _logLevel = LogLevel.INFO;
                                break;
                            case "DEBUG":
                                _logLevel = LogLevel.DEBUG;
                                break;
                            case "WARNING":
                                _logLevel = LogLevel.WARNING;
                                break;
                            case "ERROR":
                                _logLevel = LogLevel.ERROR;
                                break;
                        }
                    }

                    if (!String.IsNullOrEmpty(_strLogfile))
                    {
                        _logFileURL = Utils.ResolveFilePath(_strLogfile);
                    }
                    else
                    {
                        _logEnabled = false;
                    }

                   
                    //=================================== Make sure the required folder/file exists
                    if (!String.IsNullOrEmpty(_logFileURL) && _logEnabled)
                    {
                        DirectoryInfo _folder = Directory.GetParent(_logFileURL);

                        
                        if (!_folder.Exists)
                        {
                            _folder.Create();
                        }

                        if (!File.Exists(_logFileURL))
                        {                           
                            var _file = File.Create(_logFileURL);
                            _file.Close();
                        }
                    }
                }    


            }
        }


        public static void Log(LogLevel _level, String _logMessage)
        {
          
            CheckSetup();
            
            if ((int)_level < (int)_logLevel)
            {
                return;
            }


            if (_logEnabled)
            {
                String _newLine = String.Format("[{0} {1}] ({2}) - {3} " + Environment.NewLine, DateTime.Now.ToString(), DateTime.Now.Ticks, _level.ToString(), _logMessage);

                _messages.Add(_newLine);

                Log_Write();
            }
        }

        private static void Log_Write()
        {
            if (_messages.Count > 0)
            {
                try
                {
                    List<string> _messageCopy = new List<string>(_messages);

                    using (StreamWriter _streamWriter = new StreamWriter(_logFileURL, true))
                    {
                        _streamWriter.AutoFlush = true;

                        if (_streamWriter != null)
                        {
                            _streamWriter.Write(string.Join("", _messageCopy));
                        }
                        _streamWriter.Close();
                    }

                    //remove written items from message list
                    foreach (string _message in _messageCopy)
                    {
                        _messages.Remove(_message);
                    }

                    //attempt new write if new messages have been added
                    if (_messages.Count > 0)
                    {
                        Log_Write();
                    }
                }
                catch (Exception)
                {
                    
                    //file may be locked, delay and retry
                    if (_timer == null)
                    {
                        _timer = new Timer(100);
                        _timer.Elapsed += new ElapsedEventHandler(Log_Write_Timer);
                        _timer.Enabled = true; // Enable it
                        _timer.Start();
                    }
                }
            }
        }

        private static void Log_Write_Timer(object sender, ElapsedEventArgs e)
        {
            _timer.Stop();
            _timer.Close();
            _timer.Dispose();
            _timer = null;

            if (_timerCount == null)
            {
                _timerCount = 0;
            }

            _timerCount++;

            if (_timerCount < 20)
            {
                Log_Write();
            }
            else
            {
                _timerCount = 0;
            }
        }
    }


    public enum LogLevel
    {
        INFO=0,
        DEBUG=1,
        WARNING=2,
        ERROR=3
    }
}
