# build the whole release project, note this should RUN inside the VS dev prompt

echo "Run this inside the Visual Studio Development Prompt"
devenv /rebuild Release "..\src\Google Data API SDK.sln"
if errorlevel 1 goto buildError
devenv /rebuild Debug "..\src\Google Data API SDK.sln"
if errorlevel 1 goto buildError
devenv /rebuild Release "..\src\VS2005.mobile\gdatamobile.sln"
if errorlevel 1 goto buildError
devenv /rebuild Debug "..\src\VS2005.mobile\gdatamobile.sln"
if errorlevel 1 goto buildError
devenv /rebuild Release "..\src\youtube\YouTube SDK\YouTube SDK.sln"
if errorlevel 1 goto buildError

# copy the DLLS
xcopy /y ..\src\core\bin\Release\*.dll ..\lib\Release\*.*
xcopy /y ..\src\gbase\bin\Release\*.dll ..\lib\Release\*.*
xcopy /y ..\src\extensions\bin\Release\*.dll ..\lib\Release\*.*
xcopy /y ..\src\gspreadsheet\bin\Release\*.dll ..\lib\Release\*.*
xcopy /y ..\src\gcalendar\bin\Release\*.dll ..\lib\Release\*.*
xcopy /y ..\src\gcodesearch\bin\Release\*.dll ..\lib\Release\*.*
xcopy /y ..\src\gapps\bin\Release\*.dll ..\lib\Release\*.*
xcopy /y ..\src\gacl\bin\Release\*.dll ..\lib\Release\*.*
xcopy /y ..\src\gphotos\bin\Release\*.dll ..\lib\Release\*.*
xcopy /y ..\src\documents3\bin\Release\*.dll ..\lib\Release\*.*
xcopy /y ..\src\gcontacts\bin\Release\*.dll ..\lib\Release\*.*
xcopy /y ..\src\youtube\bin\Release\*.dll ..\lib\Release\*.*
xcopy /y ..\src\ghealth\bin\Release\*.dll ..\lib\Release\*.*
xcopy /y ..\src\blogger\bin\Release\*.dll ..\lib\Release\*.*
xcopy /y ..\src\analytics\bin\Release\*.dll ..\lib\Release\*.*
xcopy /y ..\src\WebmasterTools\bin\Release\*.dll ..\lib\Release\*.*

# copy the debug DLLS
xcopy /y ..\src\core\bin\Debug\*.dll ..\lib\Debug\*.*
xcopy /y ..\src\gbase\bin\Debug\*.dll ..\lib\Debug\*.*
xcopy /y ..\src\extensions\bin\Debug\*.dll ..\lib\Debug\*.*
xcopy /y ..\src\gspreadsheet\bin\Debug\*.dll ..\lib\Debug\*.*
xcopy /y ..\src\gcalendar\bin\Debug\*.dll ..\lib\Debug\*.*
xcopy /y ..\src\gcodesearch\bin\Debug\*.dll ..\lib\Debug\*.*
xcopy /y ..\src\gapps\bin\Debug\*.dll ..\lib\Debug\*.*
xcopy /y ..\src\gacl\bin\Debug\*.dll ..\lib\Debug\*.*
xcopy /y ..\src\gphotos\bin\Debug\*.dll ..\lib\Debug\*.*
xcopy /y ..\src\documents3\bin\Debug\*.dll ..\lib\Debug\*.*
xcopy /y ..\src\gcontacts\bin\Debug\*.dll ..\lib\Debug\*.*
xcopy /y ..\src\youtube\bin\Debug\*.dll ..\lib\Debug\*.*
xcopy /y ..\src\ghealth\bin\Debug\*.dll ..\lib\Debug\*.*
xcopy /y ..\src\blogger\bin\Debug\*.dll ..\lib\Debug\*.*
xcopy /y ..\src\analytics\bin\Debug\*.dll ..\lib\Debug\*.*
xcopy /y ..\src\WebmasterTools\bin\Debug\*.dll ..\lib\Debug\*.*

# copy the YouTube setup
xcopy /y "..\src\youtube\YouTube SDK\YouTube SDK\Release\YouTube SDK.msi" ..\lib\Setup\*.*

# copy asp dlls for the youtube sample
xcopy /y ..\src\youtube\bin\asp\*.dll ..\samples\YouTubeSample\bin\*.*

# copy the mobile DLLS
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
xcopy /y ..\src\VS2005.mobile\GHealthMobile\bin\Release\*.dll ..\lib\Mobile\WindowsMobile\*.*
xcopy /y ..\src\VS2005.mobile\GYouTubeMobile\bin\Release\*.dll ..\lib\Mobile\WindowsMobile\*.*
xcopy /y ..\src\VS2005.mobile\GBloggerMobile\bin\Release\*.dll ..\lib\Mobile\WindowsMobile\*.*
xcopy /y ..\src\VS2005.mobile\GAnalyticsMobile\bin\Release\*.dll ..\lib\Mobile\WindowsMobile\*.*

# build the Samples
devenv /rebuild Release "..\samples\Google Data APIs Samples.sln" 

# copy all exe files
xcopy /y ..\samples\Analytics\bin\Release\*.exe ..\lib\Release\*.*
xcopy /y ..\samples\Analytics_AccountFeed_Sample\bin\Release\*.exe ..\lib\Release\*.*
xcopy /y ..\samples\Analytics_DataFeed_Sample\bin\Release\*.exe ..\lib\Release\*.*
xcopy /y ..\samples\appsforyourdomain\bin\Release\*.exe ..\lib\Release\*.*
xcopy /y ..\samples\Blogger\bin\Release\*.exe ..\lib\Release\*.*
xcopy /y ..\samples\Calendar\bin\Release\*.exe ..\lib\Release\*.*
xcopy /y ..\samples\CodeSearch\bin\Release\*.exe ..\lib\Release\*.*
xcopy /y ..\samples\DocListExporter\bin\Release\*.exe ..\lib\Release\*.*
xcopy /y ..\samples\DocListUploader\bin\Release\*.exe ..\lib\Release\*.*
xcopy /y ..\samples\ExecRequest\bin\Release\*.exe ..\lib\Release\*.*
xcopy /y ..\samples\gapps_google_mail_settings_sample\bin\Release\*.exe ..\lib\Release\*.*
xcopy /y ..\samples\gapps_multidomain_sample\bin\Release\*.exe ..\lib\Release\*.*
xcopy /y ..\samples\gapps_orgmanagement_sample\bin\Release\*.exe ..\lib\Release\*.*
xcopy /y ..\samples\gbase\bin\*.exe ..\lib\Release\*.*
xcopy /y ..\samples\gbase\bin\Release\*.exe ..\lib\Release\*.*
xcopy /y ..\samples\health\bin\Release\*.exe ..\lib\Release\*.*
xcopy /y ..\samples\PhotoBrowser\bin\Release\*.exe ..\lib\Release\*.*
xcopy /y ..\samples\spreadsheets\bin\Release\*.exe ..\lib\Release\*.*
xcopy /y ..\samples\YouTubeNotifier\bin\Release\*.exe ..\lib\Release\*.*

# copy the xml doc files
xcopy /y ..\src\analytics\bin\*.xml ..\docs\*.*
xcopy /y ..\src\blogger\bin\*.xml ..\docs\*.*
xcopy /y ..\src\core\bin\*.xml ..\docs\*.*
xcopy /y ..\src\documents3\bin\*.xml ..\docs\*.*
xcopy /y ..\src\extensions\bin\*.xml ..\docs\*.*
xcopy /y ..\src\gacl\bin\*.xml ..\docs\*.*
xcopy /y ..\src\gapps\bin\*.xml ..\docs\*.*
xcopy /y ..\src\gbase\bin\*.xml ..\docs\*.*
xcopy /y ..\src\gcalendar\bin\*.xml ..\docs\*.*
xcopy /y ..\src\gcodesearch\bin\*.xml ..\docs\*.*
xcopy /y ..\src\gcontacts\bin\*.xml ..\docs\*.*
xcopy /y ..\src\ghealth\bin\*.xml ..\docs\*.*
xcopy /y ..\src\gphotos\bin\*.xml ..\docs\*.*
xcopy /y ..\src\gspreadsheets\bin\*.xml ..\docs\*.*
xcopy /y ..\src\webmastertools\bin\*.xml ..\docs\*.*
xcopy /y ..\src\youtube\bin\*.xml ..\docs\*.*

cd ..\lib\release\

rem run ILMerge on PhotoBrowser.exe
ilmerge Photobrowser.exe Google.GData.Client.Dll Google.GData.Extensions.Dll Google.GData.Photos.Dll /out:PhotoTool.exe
del Photobrowser.exe
del Phototool.pdb

rem run ILMerge on DocListExport.exe
ilmerge DocListExport.exe Google.GData.Client.Dll Google.GData.Extensions.Dll Google.GData.AccessControl.Dll Google.GData.Documents.dll /out:DocListExporter.exe
del DocListExport.exe

rem run ILMerge on YouTubeNotifier.exe
ilmerge NotifierForYT.exe Google.GData.Client.Dll Google.GData.Extensions.Dll Google.GData.YouTube.Dll /out:nfyt.exe
del NotifierForYT.exe
goto doneBuilding

del *.pdb

:buildError
echo "Error in building"

:doneBuilding
cd .\..\..\misc
