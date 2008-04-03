# build the whole release project, note this should RUN inside the VS dev prompt

echo "Run this inside the Visual Studio Development Prompt"
devenv /rebuild Release "..\src\VS2005\Google Data API SDK.sln"
devenv /rebuild Debug "..\src\VS2005\Google Data API SDK.sln"
devenv /rebuild ASP "..\src\VS2005\Google Data API SDK.sln"
devenv /rebuild Release "..\samples\Google Data APIs Samples.sln" 
devenv /rebuild Release ..\src\VS2005.mobile\gdatamobile.sln
		

# copy all exe files
xcopy /y ..\samples\Blogger\bin\Release\*.exe ..\lib\Release\*.*
xcopy /y ..\samples\Calendar\bin\Release\*.exe ..\lib\Release\*.*
xcopy /y ..\samples\CodeSearch\bin\Release\*.exe ..\lib\Release\*.*
xcopy /y ..\samples\spreadsheets\bin\Release\*.exe ..\lib\Release\*.*
xcopy /y ..\samples\gbase\bin\Release\*.exe ..\lib\Release\*.*
xcopy /y ..\samples\gbase\*.exe ..\lib\Release\*.*
xcopy /y ..\samples\\appsforyourdomain\bin\Release\*.exe ..\lib\Release\*.*
xcopy /y ..\samples\ExecRequest\bin\Release\*.exe ..\lib\Release\*.*
xcopy /y ..\samples\PhotoBrowser\bin\Release\*.exe ..\lib\Release\*.*
xcopy /y ..\samples\DocListUploader\bin\Release\*.exe ..\lib\Release\*.*

# copy the DLLS
xcopy /y ..\src\VS2005\gdata\bin\Release\*.dll ..\lib\Release\*.*
xcopy /y ..\src\VS2005\gbase\bin\Release\*.dll ..\lib\Release\*.*
xcopy /y ..\src\VS2005\gextension\bin\Release\*.dll ..\lib\Release\*.*
xcopy /y ..\src\VS2005\gspreadsheets\bin\Release\*.dll ..\lib\Release\*.*
xcopy /y ..\src\VS2005\gcalendar\bin\Release\*.dll ..\lib\Release\*.*
xcopy /y ..\src\VS2005\gcodesearch\bin\Release\*.dll ..\lib\Release\*.*
xcopy /y ..\src\VS2005\gapps\bin\Release\*.dll ..\lib\Release\*.*
xcopy /y ..\src\VS2005\gacl\bin\Release\*.dll ..\lib\Release\*.*
xcopy /y ..\src\VS2005\gphotos\bin\Release\*.dll ..\lib\Release\*.*
xcopy /y ..\src\VS2005\gdoclist\bin\Release\*.dll ..\lib\Release\*.*
xcopy /y ..\src\VS2005\gcontacts\bin\Release\*.dll ..\lib\Release\*.*
xcopy /y ..\src\VS2005\unittests\bin\Release\*.dll ..\lib\Release\*.*

# copy the debug DLLS
xcopy /y ..\src\VS2005\gdata\bin\Debug\*.dll ..\lib\Debug\*.*
xcopy /y ..\src\VS2005\gbase\bin\Debug\*.dll ..\lib\Debug\*.*
xcopy /y ..\src\VS2005\gextension\bin\Debug\*.dll ..\lib\Debug\*.*
xcopy /y ..\src\VS2005\gspreadsheets\bin\Debug\*.dll ..\lib\Debug\*.*
xcopy /y ..\src\VS2005\gcalendar\bin\Debug\*.dll ..\lib\Debug\*.*
xcopy /y ..\src\VS2005\gcodesearch\bin\Debug\*.dll ..\lib\Debug\*.*
xcopy /y ..\src\VS2005\gapps\bin\Debug\*.dll ..\lib\Debug\*.*
xcopy /y ..\src\VS2005\gacl\bin\Debug\*.dll ..\lib\Debug\*.*
xcopy /y ..\src\VS2005\gphotos\bin\Debug\*.dll ..\lib\Debug\*.*
xcopy /y ..\src\VS2005\gdoclist\bin\Debug\*.dll ..\lib\Debug\*.*
xcopy /y ..\src\VS2005\gcontacts\bin\Debug\*.dll ..\lib\Debug\*.*
xcopy /y ..\src\VS2005\unittests\bin\Debug\*.dll ..\lib\Debug\*.*

# copy the ASP DLLS
xcopy /y ..\src\VS2005\gdata\bin\asp\*.dll ..\lib\ASP.NET\*.*
xcopy /y ..\src\VS2005\gbase\bin\asp\*.dll ..\lib\ASP.NET\*.*
xcopy /y ..\src\VS2005\gextension\bin\asp\*.dll ..\lib\ASP.NET\*.*
xcopy /y ..\src\VS2005\gspreadsheets\bin\asp\*.dll ..\lib\ASP.NET\*.*
xcopy /y ..\src\VS2005\gcalendar\bin\asp\*.dll ..\lib\ASP.NET\*.*
xcopy /y ..\src\VS2005\gcodesearch\bin\asp\*.dll ..\lib\ASP.NET\*.*
xcopy /y ..\src\VS2005\gapps\bin\asp\*.dll ..\lib\ASP.NET\*.*
xcopy /y ..\src\VS2005\gacl\bin\asp\*.dll ..\lib\ASP.NET\*.*
xcopy /y ..\src\VS2005\gphotos\bin\asp\*.dll ..\lib\ASP.NET\*.*
xcopy /y ..\src\VS2005\gdoclist\bin\asp\*.dll ..\lib\ASP.NET\*.*
xcopy /y ..\src\VS2005\gcontacts\bin\asp\*.dll ..\lib\ASP.NET\*.*


#copy the modile DLLS
xcopy /y ..\src\VS2005.mobile\GBaseMobile\bin\Release\*.dll ..\lib\Mobile\WindowsMobile\*.*
xcopy /y ..\src\VS2005.mobile\GCalendarMobile\bin\Release\*.dll ..\lib\Mobile\WindowsMobile\*.*
xcopy /y ..\src\VS2005.mobile\GCodeSearchMobile\bin\Release\*.dll ..\lib\Mobile\WindowsMobile\*.*
xcopy /y ..\src\VS2005.mobile\GDataMobile\bin\Release\*.dll ..\lib\Mobile\WindowsMobile\*.*
xcopy /y ..\src\VS2005.mobile\GSpreadSheetsMobile\bin\Release\*.dll ..\lib\Mobile\WindowsMobile\*.*
xcopy /y ..\src\VS2005.mobile\GExtensionsMobile\bin\Release\*.dll ..\lib\Mobile\WindowsMobile\*.*
xcopy /y ..\src\VS2005.mobile\GAppsMobile\bin\Release\*.dll ..\lib\Mobile\WindowsMobile\*.*
xcopy /y ..\src\VS2005.mobile\GAclMobile\bin\Release\*.dll ..\lib\Mobile\WindowsMobile\*.*
xcopy /y ..\src\VS2005.mobile\GPhotosMobile\bin\Release\*.dll ..\lib\Mobile\WindowsMobile\*.*
xcopy /y ..\src\VS2005.mobile\GDocListMobile\bin\Release\*.dll ..\lib\Mobile\WindowsMobile\*.*
xcopy /y ..\src\VS2005.mobile\GContactsMobile\bin\Release\*.dll ..\lib\Mobile\WindowsMobile\*.*

# copy the xml doc files
xcopy /y ..\src\VS2005\gdata\bin\*.xml ..\docs\*.*
xcopy /y ..\src\VS2005\gbase\bin\*.xml ..\docs\*.*
xcopy /y ..\src\VS2005\gextension\bin\*.xml ..\docs\*.*
xcopy /y ..\src\VS2005\gspreadsheets\bin\*.xml ..\docs\*.*
xcopy /y ..\src\VS2005\gcalendar\bin\*.xml ..\docs\*.*
xcopy /y ..\src\VS2005\gcodesearch\bin\*.xml ..\docs\*.*
xcopy /y ..\src\VS2005\gapps\bin\*.xml ..\docs\*.*
xcopy /y ..\src\VS2005\gacl\bin\*.xml ..\docs\*.*
xcopy /y ..\src\VS2005\gphotos\bin\*.xml ..\docs\*.*
xcopy /y ..\src\VS2005\gdoclist\bin\*.xml ..\docs\*.*
xcopy /y ..\src\VS2005\gcontacts\bin\*.xml ..\docs\*.*

rem run ILMerge on PhotoBrowser.exe
cd ..\lib\release\
ilmerge Photobrowser.exe Google.GData.Client.Dll Google.GData.Extensions.Dll Google.GData.Photos.Dll /out:PhotoTool.exe
del Photobrowser.exe
del Phototool.pdb
cd .\..\..\misc



