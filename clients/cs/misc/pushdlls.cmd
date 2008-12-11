# build the whole release project, note this should RUN inside the VS dev prompt

echo "Run this inside the Visual Studio Development Prompt"
devenv /rebuild Release "..\src\VS2005\Google Data API SDK.sln"
devenv /rebuild Debug "..\src\VS2005\Google Data API SDK.sln"
devenv /rebuild Release ..\src\VS2005.mobile\gdatamobile.sln

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
xcopy /y ..\src\VS2005\youtube\bin\Release\*.dll ..\lib\Release\*.*
xcopy /y ..\src\VS2005\ghealth\bin\Release\*.dll ..\lib\Release\*.*
xcopy /y ..\src\VS2005\blogger\bin\Release\*.dll ..\lib\Release\*.*

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
xcopy /y ..\src\VS2005\youtube\bin\Debug\*.dll ..\lib\Debug\*.*
xcopy /y ..\src\VS2005\ghealth\bin\Debug\*.dll ..\lib\Debug\*.*
xcopy /y ..\src\VS2005\unittests\bin\Debug\*.dll ..\lib\Debug\*.*
xcopy /y ..\src\VS2005\blogger\bin\Debug\*.dll ..\lib\Debug\*.*


# copy asp dlls for the youtube sample
xcopy /y ..\src\VS2005\gdata\bin\asp\*.dll ..\samples\YouTubeSample\bin\*.*
xcopy /y ..\src\VS2005\gdata\bin\asp\*.dll ..\samples\YouTubeSample\bin\*.*
xcopy /y ..\src\VS2005\youtube\bin\asp\*.dll ..\samples\YouTubeSample\bin\*.*
 


#copy the mobile DLLS
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
		

devenv /rebuild Release "..\samples\Google Data APIs Samples.sln" 

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
xcopy /y ..\samples\health\bin\Release\*.exe ..\lib\Release\*.*
xcopy /y ..\samples\YouTubeNotifier\bin\Release\*.exe ..\lib\Release\*.*



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
xcopy /y ..\src\VS2005\youtube\bin\*.xml ..\docs\*.*
xcopy /y ..\src\VS2005\ghealth\bin\*.xml ..\docs\*.*
xcopy /y ..\src\VS2005\blogger\bin\*.xml ..\docs\*.*

#copy the Merge module files

xcopy /y ..\src\VS2005\CoreMergeModule\Release\*.msm ..\lib\MergeModules\*.*
xcopy /y ..\src\VS2005\YouTubeMergeModule\Release\*.msm ..\lib\MergeModules\*.*


rem run ILMerge on PhotoBrowser.exe
cd ..\lib\release\
ilmerge Photobrowser.exe Google.GData.Client.Dll Google.GData.Extensions.Dll Google.GData.Photos.Dll /out:PhotoTool.exe
del Photobrowser.exe
del Phototool.pdb

rem run ILMerge on YouTubeNotifier.exe
ilmerge YouTubeNotifier.exe Google.GData.Client.Dll Google.GData.Extensions.Dll Google.GData.YouTube.Dll /out:Ytn.exe
del YouTubeNotifier.exe
del YouTubeNotifier.pdb

cd .\..\..\misc



