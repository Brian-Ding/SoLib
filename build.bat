@echo off

del ".\Packages\SoLib.Controls*.*"

set config=%1
if "%config%" == "" (
    set config=Release
)

set version = 
if not "%PackageVersion%" == "" (
    set version=-Version %PackageVersion%
)

set nuget=
if "%nuget%" == "" (
    set nuget=nuget
)

echo on
"%programfiles(x86)%\Microsoft Visual Stduio\2017\Community\MSBuild\15.0\Bin\MSBuild.exe" ".\SoLib.Controls\SoLib.Controls.csproj" /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false

mkdir Packages
call %nuget% pack ".\SoLib.Controls\SoLib.Controls.nuspec" -symbols -o Packages -p Configuration=%config% %version%

call %nuget% push ".\Packages\SoLib.Controls.*.*.*.nupkg" -Source https://www.nuget.org/api/v2/package