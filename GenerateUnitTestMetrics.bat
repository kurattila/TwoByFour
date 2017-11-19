@echo off
REM ================================================================================
REM                    Visualize Code Coverage + Risk Hotspots
REM                       using OpenCover + ReportGenerator
REM ================================================================================
REM If you get errors from OpenCover like this...
REM 
REM        No results, this could be for a number of reasons. The most common reasons are:
REM            1) missing PDBs for the assemblies that match the filter please review the
REM            output file and refer to the Usage guide (Usage.rtf) about filters.
REM            2) the profiler may not be registered correctly, please refer to the Usage
REM            guide and the -register switch.
REM 
REM ...then make sure your .NET Core projects has 'DebugType' set to 'full' inside your .csproj file:
REM        <PropertyGroup>
REM          <TargetFramework>netcoreapp2.0</TargetFramework>
REM      	 <DebugType>full</DebugType>
REM        </PropertyGroup>
REM 
REM (for details, see https://blog.markvincze.com/setting-up-coveralls-for-a-net-core-project/)
REM 

SET VSProjectContainingTests=%~dp0TwoByFour.Tests\TwoByFour.Tests.csproj
SET TestRunnerExe=%ProgramW6432%\dotnet\dotnet.exe
for /R "%USERPROFILE%\.nuget\packages\opencover" %%a in (*) do if /I "%%~nxa"=="OpenCover.Console.exe" SET OpenCoverExe=%%~dpnxa
for /R "%USERPROFILE%\.nuget\packages\reportgenerator" %%a in (*) do if /I "%%~nxa"=="ReportGenerator.exe" SET ReportGeneratorExe=%%~dpnxa

echo VS Test Project: %VSProjectContainingTests%
echo Test Runner:     %TestRunnerExe%
echo OpenCover:       %OpenCoverExe%
echo ReportGenerator: %ReportGeneratorExe%

REM Create a 'GeneratedReports' folder if it does not exist
if not exist "%~dp0GeneratedReports" mkdir "%~dp0GeneratedReports"

REM RunOpenCoverUnitTestMetrics 
"%OpenCoverExe%" ^
 -register:user ^
 -oldStyle ^
 -target:"%TestRunnerExe%" ^
 -targetargs:" test \"%VSProjectContainingTests%\"" ^
 -filter:"+[*]* -[*.Tests]* -[xunit*]* -[Moq*]*" ^
 -output:"%~dp0GeneratedReports\CoverageReport.xml"

 REM RunReportGeneratorOutput
 "%ReportGeneratorExe%" ^
 -reports:"%~dp0\GeneratedReports\CoverageReport.xml" ^
 -targetdir:"%~dp0\GeneratedReports\ReportGenerator Output"

REM RunLaunchReport
start "report" "%~dp0\GeneratedReports\ReportGenerator Output\index.htm"
