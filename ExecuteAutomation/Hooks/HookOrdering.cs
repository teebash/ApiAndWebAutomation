namespace ExecuteAutomation.Hooks
{
    /// <summary>
    ///     Defines the order the hook should be run in, BE CAREFUL WHEN CHANGING.
    /// 
    ///     This is a static class to avoid having to cast into an int for the hooks.
    /// </summary>
    public class HookOrdering
    {
        public const int FeatureContextSetting = 5;
        public const int ScenarioHooks = 10;
        public const int UseChromeDriver = 20;
        public const int UseFireFoxDriver = 30;
        public const int LoggedIn = 70;
        public const int LoggedOut = int.MaxValue;
    }
}
