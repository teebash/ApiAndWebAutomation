using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using NUnit.Framework;

[assembly: AssemblyTitle("ExecuteAutomation")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("ExecuteAutomation")]
[assembly: AssemblyCopyright("Copyright ©  2018")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

[assembly: ComVisible(false)]

[assembly: Guid("5e843065-5036-457a-87ca-f6db0e66596a")]

// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

//Parallel attribute
[assembly: Parallelizable(ParallelScope.All)]
//[assembly: Parallelizable(ParallelScope.Fixtures)]

// log4net setting
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
