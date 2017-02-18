@echo Off 
set config=%1 
if "%config%" == "" ( 
	set config=Release 
) 
  
set version=1.0.0 
if not "%PackageVersion%" == "" ( 
	set version=%PackageVersion% 
) 

set nuget= 
if "%nuget%" == "" ( 
	set nuget=nuget 
) 

C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\MSBuild.exe SoLib.Controls.csproj /p:Configuration="%config%"

mkdir Build 
mkdir Build\lib 
mkdir Build\lib\net40 
 
%nuget% pack "SoLib.Controls.nuspec" -Symbols