# build the whole release project, note this should RUN inside the VS dev prompt

echo "Run this inside the Visual Studio Development Prompt"
devenv /rebuild Release "..\src\VS2003\Google Data API SDK.sln"
devenv /rebuild ASP "..\src\VS2003\Google Data API SDK.sln"
		

# copy all exe files
xcopy /y ..\src\VS2003\Blogger\bin\Release\*.exe ..\lib\Release\*.*
xcopy /y ..\src\VS2003\Calendar\bin\Release\*.exe ..\lib\Release\*.*
xcopy /y ..\src\VS2003\CodeSearch\bin\Release\*.exe ..\lib\Release\*.*
xcopy /y ..\src\VS2003\Spreadsheet\bin\Release\*.exe ..\lib\Release\*.*
xcopy /y ..\src\VS2003\gbase_customertool\bin\Release\*.exe ..\lib\Release\*.*
xcopy /y ..\src\VS2003\gapps_customertool\bin\Release\*.exe ..\lib\Release\*.*
xcopy /y ..\src\VS2003\gbase_demo\bin\*.exe ..\lib\Release\*.*
xcopy /y ..\src\VS2003\gbase_querytool\bin\Release\*.exe ..\lib\Release\*.*
xcopy /y ..\src\VS2003\gapps_migration_sample\bin\Release\*.exe ..\lib\Release\*.*
xcopy /y  ..\src\VS2003\ExecRequest\bin\Release\*.exe ..\lib\Release\*.*
xcopy /y  ..\src\VS2003\PhotoBrowser\bin\Release\*.exe ..\lib\Release\*.*

# copy the DLLS
xcopy /y ..\src\VS2003\gdata\bin\Release\*.dll ..\lib\Release\*.*
xcopy /y ..\src\VS2003\gbase\bin\Release\*.dll ..\lib\Release\*.*
xcopy /y ..\src\VS2003\gextension\bin\Release\*.dll ..\lib\Release\*.*
xcopy /y ..\src\VS2003\gspreadsheets\bin\Release\*.dll ..\lib\Release\*.*
xcopy /y ..\src\VS2003\gcalendar\bin\Release\*.dll ..\lib\Release\*.*
xcopy /y ..\src\VS2003\gcodesearch\bin\Release\*.dll ..\lib\Release\*.*
xcopy /y ..\src\VS2003\gapps\bin\Release\*.dll ..\lib\Release\*.*
xcopy /y ..\src\VS2003\gacl\bin\Release\*.dll ..\lib\Release\*.*
xcopy /y ..\src\VS2003\gphotos\bin\Release\*.dll ..\lib\Release\*.*

# copy the ASP DLLS
xcopy /y ..\src\VS2003\gdata\bin\asp\*.dll ..\lib\ASP.NET\*.*
xcopy /y ..\src\VS2003\gbase\bin\asp\*.dll ..\lib\ASP.NET\*.*
xcopy /y ..\src\VS2003\gextension\bin\asp\*.dll ..\lib\ASP.NET\*.*
xcopy /y ..\src\VS2003\gspreadsheets\bin\asp\*.dll ..\lib\ASP.NET\*.*
xcopy /y ..\src\VS2003\gcalendar\bin\asp\*.dll ..\lib\ASP.NET\*.*
xcopy /y ..\src\VS2003\gcodesearch\bin\asp\*.dll ..\lib\ASP.NET\*.*
xcopy /y ..\src\VS2003\gapps\bin\asp\*.dll ..\lib\ASP.NET\*.*
xcopy /y ..\src\VS2003\gacl\bin\asp\*.dll ..\lib\ASP.NET\*.*
xcopy /y ..\src\VS2003\gphotos\bin\asp\*.dll ..\lib\ASP.NET\*.*
xcopy /y ..\src\VS2003\gphotos\bin\asp\*.dll ..\lib\ASP.NET\*.*



# copy the xml doc files
xcopy /y ..\src\VS2003\gdata\bin\*.xml ..\docs\*.*
xcopy /y ..\src\VS2003\gbase\bin\*.xml ..\docs\*.*
xcopy /y ..\src\VS2003\gextension\bin\*.xml ..\docs\*.*
xcopy /y ..\src\VS2003\gspreadsheets\bin\*.xml ..\docs\*.*
xcopy /y ..\src\VS2003\gcalendar\bin\*.xml ..\docs\*.*
xcopy /y ..\src\VS2003\gcodesearch\bin\*.xml ..\docs\*.*
xcopy /y ..\src\VS2003\gapps\bin\*.xml ..\docs\*.*
xcopy /y ..\src\VS2003\gacl\bin\*.xml ..\docs\*.*
xcopy /y ..\src\VS2003\gphotos\bin\*.xml ..\docs\*.*
xcopy /y ..\src\VS2003\ExecRequest\bin\*.xml ..\docs\*.*

rem run ILMerge on PhotoBrowser.exe
cd ..\lib\release\
ilmerge Photobrowser.exe Google.GData.Client.Dll Google.GData.Extensions.Dll Google.GData.Photos.Dll /out:PhotoTool.exe
del Photobrowser.exe
del Phototool.pdb
cd .\..\..\misc



