using AventStack.ExtentReports.Reporter.Configuration;
using AventStack.ExtentReports.Reporter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAutomation_RestSharp.Base
{
    public class TestInitializeHookcs
    {
        //public static Dictionary<string, string> customchromeoptions;

        public TestInitializeHookcs()
        {
            // Constructor logic if needed
        }

        public static ExtentHtmlReporter CreateExtentReportInstance(String featureName)
        {
            string text = DateTime.Now.ToString("dd-MM-yyyy-HHmmss");
            string currentProjectDirectory = GetCurrentProjectDirectory();
            string reportPath = Path.Combine(currentProjectDirectory, "TestResults", "ExtentReports_" + text, featureName);
            if (!Directory.Exists(reportPath))
            {
                Directory.CreateDirectory(reportPath);
            }
            ExtentHtmlReporter extentHtmlReporter = new ExtentHtmlReporter(reportPath);
            extentHtmlReporter.Config.Theme = Theme.Dark;
            extentHtmlReporter.Config.DocumentTitle = "Test Automation Report";
            extentHtmlReporter.Config.ReportName = "Test Automation Report";
            extentHtmlReporter.Config.EnableTimeline = true;
            return extentHtmlReporter;
        }

        private static string GetCurrentProjectDirectory()
        {
            // Get the current project directory by navigating up from the bin directory
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var projectDirectory = Directory.GetParent(baseDirectory).Parent.Parent.FullName;
            return projectDirectory;
        }
    }
}
