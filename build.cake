#tool "nuget:?package=NUnit.ConsoleRunner"

var target = Argument("target", "Default");

Task("Default")
    .Does(() =>
{
    Information("Hello,world!");
});

Task("Build")
	.Does(() => 
{
	DotNetBuild("./Tumblin.sln");
});

Task("Test") 
	.IsDependentOn("Build")
	.Does(() =>
{
	NUnit3(new []{"./Tumblin.Web.Test/bin/Debug/Tumblin.Web.Test.dll"}, new NUnit3Settings {
		ResultFormat = "AppVayor"
	});
});

RunTarget(target);