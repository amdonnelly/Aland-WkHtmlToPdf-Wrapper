using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

using AlanD.wkhtmltopdf.Utils;

namespace AlanD.wkhtmltopdf.Config
{
    /// <summary>
    /// List of WKHTML switches
    /// * It's stored this way to make the process of adding/removing long and short switches easier
    /// </summary>
    public class Options
    {
        private List<Option> _options = new List<Option>();

        public Options()
        {
            _options.Add(new Option() { Enabled = false, Key = "--collate" });
            _options.Add(new Option() { Enabled = false, Key = "--no-collate" });
            _options.Add(new Option() { Enabled = false, Key = "--cookie-jar" });
            _options.Add(new Option() { Enabled = false, Key = "--copies"});
            _options.Add(new Option() { Enabled = false, Key = "--dpi", ShortKey = "-d" });
            _options.Add(new Option() { Enabled = false, Key = "--extended-help", ShortKey = "-H" });
            _options.Add(new Option() { Enabled = false, Key = "--grayscale", ShortKey = "-g" });
            _options.Add(new Option() { Enabled = false, Key = "--help", ShortKey = "-h" });
            _options.Add(new Option() { Enabled = false, Key = "--htmldoc" });
            _options.Add(new Option() { Enabled = false, Key = "--image-dpi" });
            _options.Add(new Option() { Enabled = false, Key = "--image-quality" });
            _options.Add(new Option() { Enabled = false, Key = "--lowquality", ShortKey = "-l" });
            _options.Add(new Option() { Enabled = false, Key = "--manpage" });
            _options.Add(new Option() { Enabled = false, Key = "--margin-bottom", ShortKey = "-B" });
            _options.Add(new Option() { Enabled = false, Key = "--margin-left", ShortKey = "-L" });
            _options.Add(new Option() { Enabled = false, Key = "--margin-right", ShortKey = "-R" });
            _options.Add(new Option() { Enabled = false, Key = "--margin-top", ShortKey = "-T" });
            _options.Add(new Option() { Enabled = false, Key = "--orientation", ShortKey = "-O" });
            _options.Add(new Option() { Enabled = false, Key = "--output-format" });
            _options.Add(new Option() { Enabled = false, Key = "--page-height" });
            _options.Add(new Option() { Enabled = false, Key = "--page-size", ShortKey = "-s" });
            _options.Add(new Option() { Enabled = false, Key = "--page-width" });
            _options.Add(new Option() { Enabled = false, Key = "--no-pdf-compression" });
            _options.Add(new Option() { Enabled = false, Key = "--quiet", ShortKey = "-q" });
            _options.Add(new Option() { Enabled = false, Key = "--read-args-from-stdin" });
            _options.Add(new Option() { Enabled = false, Key = "--readme" });
            _options.Add(new Option() { Enabled = false, Key = "--title" });
            _options.Add(new Option() { Enabled = false, Key = "--version", ShortKey = "-V" });

            _options.Add(new Option() { Enabled = false, Key = "--dump-default-toc-xsl" });
            _options.Add(new Option() { Enabled = false, Key = "--dump-outline" });
            _options.Add(new Option() { Enabled = false, Key = "--outline" });
            _options.Add(new Option() { Enabled = false, Key = "--no-outline" });
            _options.Add(new Option() { Enabled = false, Key = "--outline-depth" });

            _options.Add(new Option() { Enabled = false, Key = "--allow" });
            _options.Add(new Option() { Enabled = false, Key = "--background" });
            _options.Add(new Option() { Enabled = false, Key = "--cache-dir" });
            _options.Add(new Option() { Enabled = false, Key = "--checkbox-checked-svg" });
            _options.Add(new Option() { Enabled = false, Key = "--checkbox-svg" });
            _options.Add(new Option() { Enabled = false, Key = "--cookie" });
            _options.Add(new Option() { Enabled = false, Key = "--custom-header" });
            _options.Add(new Option() { Enabled = false, Key = "--custom-header-propagation" });
            _options.Add(new Option() { Enabled = false, Key = "--no-custom-header-propagation" });
            _options.Add(new Option() { Enabled = false, Key = "--debug-javascript" });
            _options.Add(new Option() { Enabled = false, Key = "--no-debug-javascript" });
            _options.Add(new Option() { Enabled = false, Key = "--default-header" });
            _options.Add(new Option() { Enabled = false, Key = "--encoding" });
            _options.Add(new Option() { Enabled = false, Key = "--disable-external-links" });
            _options.Add(new Option() { Enabled = false, Key = "--enable-external-links" });
            _options.Add(new Option() { Enabled = false, Key = "--disable-forms" });
            _options.Add(new Option() { Enabled = false, Key = "--enable-forms" });
            _options.Add(new Option() { Enabled = false, Key = "--images" });
            _options.Add(new Option() { Enabled = false, Key = "--no-images" });
            _options.Add(new Option() { Enabled = false, Key = "--disable-internal-links" });
            _options.Add(new Option() { Enabled = false, Key = "--enable-internal-links" });
            _options.Add(new Option() { Enabled = false, Key = "--disable-javascript", ShortKey = "-n" });
            _options.Add(new Option() { Enabled = false, Key = "--enable-javascript" });
            _options.Add(new Option() { Enabled = false, Key = "--javascript-delay" });
            _options.Add(new Option() { Enabled = false, Key = "--load-error-handling" });
            _options.Add(new Option() { Enabled = false, Key = "--load-media-error-handling" });
            _options.Add(new Option() { Enabled = false, Key = "--disable-local-file-access" });

            _options.Add(new Option() { Enabled = false, Key = "--enable-local-file-access" });
            _options.Add(new Option() { Enabled = false, Key = "--minimum-font-size" });
            _options.Add(new Option() { Enabled = false, Key = "--exclude-from-outline" });
            _options.Add(new Option() { Enabled = false, Key = "--include-in-outline" });
            _options.Add(new Option() { Enabled = false, Key = "--page-offset" });
            _options.Add(new Option() { Enabled = false, Key = "--password" });
            _options.Add(new Option() { Enabled = false, Key = "--disable-plugins" });
            _options.Add(new Option() { Enabled = false, Key = "--enable-plugins" });
            _options.Add(new Option() { Enabled = false, Key = "--post" });
            _options.Add(new Option() { Enabled = false, Key = "--post-file" });
            _options.Add(new Option() { Enabled = false, Key = "--print-media-type" });
            _options.Add(new Option() { Enabled = false, Key = "--no-print-media-type" });
            _options.Add(new Option() { Enabled = false, Key = "--proxy", ShortKey = "-p" });
            _options.Add(new Option() { Enabled = false, Key = "--radiobutton-checked-svg" });
            _options.Add(new Option() { Enabled = false, Key = "--radiobutton-svg" });
            _options.Add(new Option() { Enabled = false, Key = "--run-script" });
            _options.Add(new Option() { Enabled = false, Key = "--disable-smart-shrinking" });
            _options.Add(new Option() { Enabled = false, Key = "--enable-smart-shrinking" });
            _options.Add(new Option() { Enabled = false, Key = "--stop-slow-scripts" });
            _options.Add(new Option() { Enabled = false, Key = "--no-stop-slow-scripts" });
            _options.Add(new Option() { Enabled = false, Key = "--disable-toc-back-links" });
            _options.Add(new Option() { Enabled = false, Key = "--enable-toc-back-links" });
            _options.Add(new Option() { Enabled = false, Key = "--user-style-sheet" });
            _options.Add(new Option() { Enabled = false, Key = "--username" });
            _options.Add(new Option() { Enabled = false, Key = "--viewport-size" });
            _options.Add(new Option() { Enabled = false, Key = "--window-status" });
            _options.Add(new Option() { Enabled = false, Key = "--zoom" });

            _options.Add(new Option() { Enabled = false, Key = "--footer-center" });
            _options.Add(new Option() { Enabled = false, Key = "--footer-font-name" });
            _options.Add(new Option() { Enabled = false, Key = "--footer-font-size" });
            _options.Add(new Option() { Enabled = false, Key = "--footer-html" });
            _options.Add(new Option() { Enabled = false, Key = "--footer-left" });
            _options.Add(new Option() { Enabled = false, Key = "--footer-line" });
            _options.Add(new Option() { Enabled = false, Key = "--no-footer-line" });
            _options.Add(new Option() { Enabled = false, Key = "--footer-right" });
            _options.Add(new Option() { Enabled = false, Key = "--footer-spacing" });
            _options.Add(new Option() { Enabled = false, Key = "--header-center" });
            _options.Add(new Option() { Enabled = false, Key = "--header-font-name" });
            _options.Add(new Option() { Enabled = false, Key = "--header-font-size" });
            _options.Add(new Option() { Enabled = false, Key = "--header-html" });
            _options.Add(new Option() { Enabled = false, Key = "--header-left" });
            _options.Add(new Option() { Enabled = false, Key = "--header-line" });
            _options.Add(new Option() { Enabled = false, Key = "--no-header-line" });
            _options.Add(new Option() { Enabled = false, Key = "--header-right" });
            _options.Add(new Option() { Enabled = false, Key = "--header-spacing" });
            _options.Add(new Option() { Enabled = false, Key = "--replace" });

            _options.Add(new Option() { Enabled = false, Key = "--disable-dotted-lines" });
            _options.Add(new Option() { Enabled = false, Key = "--toc-header-text" });
            _options.Add(new Option() { Enabled = false, Key = "--toc-level-indentation" });
            _options.Add(new Option() { Enabled = false, Key = "--disable-toc-links" });
            _options.Add(new Option() { Enabled = false, Key = "--toc-text-size-shrink" });
            _options.Add(new Option() { Enabled = false, Key = "--xsl-style-sheet" });
        }

        /// <summary>
        /// Add exe arguments from a config section
        /// </summary>
        /// <param name="_exeArguments"></param>
        public void ParseExeArguments(String _exeArguments)
        {
            if (!String.IsNullOrEmpty(_exeArguments))
            {

                Regex regex = new Regex(@"(?<switch>-{1,2}\S*)(?:[=:]?|\s+)(?<value>[^-\s].*?)?(?=\s+[-\/]|$)");

                List<KeyValuePair<string, string>> _matches = (from match in regex.Matches(_exeArguments).Cast<Match>()
                                                          select new KeyValuePair<string, string>(match.Groups["switch"].Value, match.Groups["value"].Value)).ToList();

                foreach (KeyValuePair<string, string> _match in _matches)
                {
                    AddOption(_match.Key, _match.Value);
                }
            }
        }

        public void AddOption(string _key)
        {
            AddOption(_key, null);
        }

        public void AddOption(string _key, string _value)
        {
            if (!String.IsNullOrEmpty(_key))
            {
                Option _option = FindOption(_key); ;

                _option.Enabled = true;

                if (!String.IsNullOrEmpty(_value))
                {
                    _option.Value = _value.Trim();
                }
            }
        }



        public void RemoveOption(string _key)
        {
            if (!String.IsNullOrEmpty(_key))
            {
                Option _option = FindOption(_key); ;

                _option.Enabled = false;
            }
        }

        private Option FindOption(string _key)
        {
            Option _option=null;

            if (_key.Trim().StartsWith("--"))
            {
                _option = _options.Where(x => x.Key == _key).SingleOrDefault();
            }
            else if (_key.Trim().StartsWith("-"))
            {
                _option = _options.Where(x => x.ShortKey == _key).SingleOrDefault();
            }

            //if option is not found then return a new one
            if (_option == null)
            {
                _option = new Option();
                if (_key.Trim().StartsWith("--"))
                {
                    _option.Key = _key.Trim();
                }
                else if (_key.Trim().StartsWith("-"))
                {
                    _option.ShortKey = _key.Trim();
                }

                _options.Add(_option);
            }


            return _option;
        }


        /// <summary>
        /// reset the current options list, set all to enabled=false
        /// parse new command arguments and replace old values
        /// </summary>
        /// <param name="_arguments"></param>
        /// <returns></returns>
        public void SetOptions(string _arguments)
        {
            foreach (Option _option in _options.Where(x => x.Enabled == true))
            {
                _option.Enabled = false;
            }

            if (!String.IsNullOrEmpty(_arguments))
            {
                ParseExeArguments(_arguments);
            }
        }

        /// <summary>
        /// Build string with all switches and values to be passed to exe call
        /// </summary>
        /// <returns></returns>
        public string BuildCommandArguments()
        {
            StringBuilder _arguments = new StringBuilder();

            foreach (Option _option in _options.Where(x => x.Enabled == true))
            {
                if (!String.IsNullOrEmpty(_option.ShortKey))
                {
                    _arguments.Append(" " + _option.ShortKey);
                }
                else
                {
                    _arguments.Append(" " + _option.Key);
                }

                if (!String.IsNullOrEmpty(_option.Value) && !String.IsNullOrEmpty(_option.Value.Trim()))
                {
                    _arguments.Append(" \"" + _option.Value.Trim() + "\"");
                }
            }

            return _arguments.ToString();
        }
    }
}
