using TechTalk.SpecFlow;
using TechTalk.SpecRun;

namespace ExecuteAutomation.Hooks
{
    /// <summary>
    ///     Hooks into the Scenario Context.
    /// </summary>
    [Binding]
    public class ScenarioHooks
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly ScenarioContext _scenarioContext;
        private readonly FeatureContext _featureContext;

        public ScenarioHooks(ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
        }

        /// <summary>
        ///     All tests marked as pending should be marked as pending by SpecRunner.
        /// </summary>
        [BeforeScenario("@Pending", Order = HookOrdering.ScenarioHooks)]
        public void BeforePendingScenario()
        {
            SpecRunner.TestPending("Test marked as pending.");
        }

        /// <summary>
        ///     All tests marked as defect should be marked as ignored by SpecRunner.
        /// </summary>
        [BeforeScenario("@defect", Order = HookOrdering.ScenarioHooks)]
        public void BeforeDefectScenario()
        {
            SpecRunner.TestIgnored("Test is marked with a defect, see the feature file to learn more.");
        }

        [BeforeScenario(Order = HookOrdering.FeatureContextSetting)]
        public void BeforeFeature()
        {
            var featureInfoTitle = _featureContext.FeatureInfo.Title;
            var featureInfoDescription = _featureContext.FeatureInfo.Description;


            Log.Info("==================================================================================");
            Log.Info("=====      "         + featureInfoTitle + " Test Started                     =====");
            Log.Info("==================================================================================");

            Log.Info("Executing Journey : " + featureInfoTitle);
            Log.Info("Description : " + featureInfoDescription);
        }

        [BeforeStep()]
        public void BeforeStep()
        {
            var stepInfoText = _scenarioContext.StepContext.StepInfo.Text;
            var stepInfoStepDefinitionType = _scenarioContext.StepContext.StepInfo.StepDefinitionType;

            Log.Info(" Step : " + stepInfoStepDefinitionType + " " + stepInfoText);
        }
    }
}
