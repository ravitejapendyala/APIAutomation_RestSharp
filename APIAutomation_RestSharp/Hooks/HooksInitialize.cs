using APIAutomation_RestSharp.Base;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.MarkupUtils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace APIAutomation_RestSharp.Hooks
{
    [Binding]
    public class HooksInitialize : TestInitializeHookcs
    {
        private readonly FeatureContext _featureContext;
        private readonly ScenarioContext _scenarioContext;
        private static ExtentTest _currentScenarioName;
        private static ExtentTest _featureName;
        private static ExtentReports _extent;
        public static string executionAgent, reportPath;

        public HooksInitialize(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            _featureContext = featureContext;
            _scenarioContext = scenarioContext;
        }

        public static void CreateExtentReport(string featureName)
        {
            var extentreporter = CreateExtentReportInstance(featureName);
            _extent = new ExtentReports();
            _extent.AttachReporter(extentreporter);
        }

        [BeforeTestRun]
        public static void TestInitialize()
        {
            executionAgent = TestContext.Parameters["executionAgent"];
            string relativePath = TestContext.Parameters["AccessibilityReportPath"];
            bool archive = Convert.ToBoolean(TestContext.Parameters["ArchiveReport"]);
        }

        [BeforeFeature]
        public static void InitiallizeFeature(FeatureContext featureContext)
        {
            CreateExtentReport(featureContext.FeatureInfo.Title);
            _featureName = _extent.CreateTest<Feature>(featureContext.FeatureInfo.Title);
        }

        [BeforeScenario]
        public void Initialize()
        {
            _currentScenarioName = _featureName.CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title);
        }

        [AfterStep]
        public void CaptureScreenshots()
        {
            var stepType = _scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
            var tags = _scenarioContext.ScenarioInfo.Tags.ToString();
            if (_scenarioContext.TestError == null)
            {
                switch (stepType)
                {
                    case "Given":
                        _currentScenarioName.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text).Pass(_scenarioContext.CurrentScenarioBlock.ToString());
                        break;
                    case "When":
                        _currentScenarioName.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text).Pass(_scenarioContext.CurrentScenarioBlock.ToString());
                        break;
                    case "Then":
                        _currentScenarioName.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text).Pass(_scenarioContext.CurrentScenarioBlock.ToString());
                        break;
                    case "And":
                        _currentScenarioName.CreateNode<And>(_scenarioContext.StepContext.StepInfo.Text).Pass(_scenarioContext.CurrentScenarioBlock.ToString());
                        break;
                }
            }
            else if (_scenarioContext.TestError != null)
            {
                switch (stepType)
                {
                    case "Given":
                        _currentScenarioName.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.Message);
                        break;
                    case "When":
                        _currentScenarioName.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.Message);
                        break;
                    case "Then":
                        _currentScenarioName.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.Message);
                        break;
                    case "And":
                        _currentScenarioName.CreateNode<And>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.Message);
                        break;
                }
            }
            else if (_scenarioContext.ScenarioExecutionStatus.ToString() == "StepDefinitionPending")
            {
                switch (stepType)
                {
                    case "Given":
                        _currentScenarioName.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                        break;
                    case "When":
                        _currentScenarioName.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                        break;
                    case "Then":
                        _currentScenarioName.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                        break;
                    case "And":
                        _currentScenarioName.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                        break;
                }
            }
            _extent.Flush();
        }

        [AfterScenario]
        public void TerminateScenarioExecution()
        {
            var dirpath = GetCurrentProjectDirectory();
            var filePath = Path.Combine(dirpath, "TestData", "DataFiles", _scenarioContext.ScenarioInfo.Title + ".json");
            if (Directory.Exists(filePath))
            {
                var jsonData = File.ReadAllText(filePath);
                _currentScenarioName.CreateNode<And>("json file used for execution").Info(MarkupHelper.CreateCodeBlock(jsonData, CodeLanguage.Json));
                _currentScenarioName.Pass("");
                _extent.Flush();
            }
            else
            {
                _extent.Flush();
            }
        }

        [AfterTestRun]
        public static void TerminateExecution()
        {
            _extent.Flush();
        }

        private static string GetCurrentProjectDirectory()
        {
            // Implement logic to get the current project directory
            return AppDomain.CurrentDomain.BaseDirectory;
        }
    }
}
