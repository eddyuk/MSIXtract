using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSIXtractApplication
{
    public class XtractHandler
    {
        const string CommandTemplate = "/a \"{0}\" /qb TARGETDIR=\"{1}\"";
        public string Filename { get; set; }
        public string ExtractDirectory { get; set; }
        public XtractHandler(string[] args)
        {
            if (args != null && !string.IsNullOrWhiteSpace(args[0]))
            {
                if (File.Exists(args[0]))
                {
                    Filename = args[0];
                    SetExtractFolder();
                }
                else
                {
                    throw new Exception("Requested file not found");
                }
            }
            else
            {
                throw new Exception("File was not provided");
            }
        }

        private void SetExtractFolder()
        {
            string rootFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Application.ProductName);
            if(!Directory.Exists(rootFolder))
            {
                Directory.CreateDirectory(rootFolder);
            }
            ExtractDirectory = Path.Combine(rootFolder, Path.GetFileNameWithoutExtension(Filename));
            if (Directory.Exists(ExtractDirectory))
            {
                Directory.Delete(ExtractDirectory, true);
            }
            Directory.CreateDirectory(ExtractDirectory);
        }
        /// <summary>
        /// Executes extract of MSI file and returns true if successful.
        /// </summary>
        /// <returns></returns>
        public bool Extract()
        {
            bool result = false;
            string command = string.Format(CommandTemplate, Filename, ExtractDirectory);
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "msiexec.exe");
                process.StartInfo.Arguments = command;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                process.WaitForExit();
                result = true;
            }
            catch (Exception e)
            {
                throw e;
            }
            return result;
        }
    }
}
