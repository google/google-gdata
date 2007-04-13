# build the whole release project, note this should RUN inside the VS dev prompt

echo "Run this inside the Visual Studio 2005 Development Prompt"
devenv /rebuild Release ..\src\VS2005.mobile\gdatamobile.sln



#copy the DLLS
xcopy /y ..\src\VS2005.mobile\GBaseMobile\bin\Release\*.dll ..\lib\Mobile\WindowsMobile\*.*
xcopy /y ..\src\VS2005.mobile\GCalendarMobile\bin\Release\*.dll ..\lib\Mobile\WindowsMobile\*.*
xcopy /y ..\src\VS2005.mobile\GCodeSearchMobile\bin\Release\*.dll ..\lib\Mobile\WindowsMobile\*.*
xcopy /y ..\src\VS2005.mobile\GDataMobile\bin\Release\*.dll ..\lib\Mobile\WindowsMobile\*.*
xcopy /y ..\src\VS2005.mobile\GSpreadSheetsMobile\bin\Release\*.dll ..\lib\Mobile\WindowsMobile\*.*
xcopy /y ..\src\VS2005.mobile\GExtensionsMobile\bin\Release\*.dll ..\lib\Mobile\WindowsMobile\*.*
xcopy /y ..\src\VS2005.mobile\GAppsMobile\bin\Release\*.dll ..\lib\Mobile\WindowsMobile\*.*

