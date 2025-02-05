using AventStack.ExtentReports;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAutomation_RestSharp.Base
{
    public class ParallelConfig
    {  public ChromeOptions croptions { get; set; }
        public string RandomString { get; set; }
        public string FinalRandommString { get; set; }
        public Dictionary<string, string> GlobalVariables { get; set; }
        public LogHelpers LogHelper { get; set; }

        public string GetCurrentProjectDirectory()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string uriString = baseDirectory.Substring(0, baseDirectory.LastIndexOf("bin", StringComparison.Ordinal));
            return new Uri(uriString).LocalPath;
        }
        


    }
}
