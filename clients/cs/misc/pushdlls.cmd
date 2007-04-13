# build the whole release project, note this should RUN inside the VS dev prompt

echo "Run this inside the Visual Studio Development Prompt"
devenv /rebuild Release ..\src\VS2003\gdata.sln


# copy all exe files
xcopy /y ..\src\VS2003\Blogger\bin\Release\*.exe ..\lib\Release\*.*
xcopy /y ..\src\VS2003\Calendar\bin\Release\*.exe ..\lib\Release\*.*
xcopy /y ..\src\VS2003\CodeSearch\bin\Release\*.exe ..\lib\Release\*.*
xcopy /y ..\src\VS2003\Spreadsheet\bin\Release\*.exe ..\lib\Release\*.*
xcopy /y ..\src\VS2003\gbase_customertool\bin\Release\*.exe ..\lib\Release\*.*
xcopy /y ..\src\VS2003\gbase_demo\bin\*.exe ..\lib\Release\*.*
xcopy /y ..\src\VS2003\gbase_querytool\bin\Release\*.exe ..\lib\Release\*.*
xcopy /y ..\src\VS2003\gapps_sample\bin\Release\*.exe ..\lib\Release\*.*

# copy the DLLS
xcopy /y ..\src\VS2003\gdata\bin\Release\*.dll ..\lib\Release\*.*
xcopy /y ..\src\VS2003\gbase\bin\Release\*.dll ..\lib\Release\*.*
xcopy /y ..\src\VS2003\gextension\bin\Release\*.dll ..\lib\Release\*.*
xcopy /y ..\src\VS2003\gspreadsheets\bin\Release\*.dll ..\lib\Release\*.*
xcopy /y ..\src\VS2003\gcalendar\bin\Release\*.dll ..\lib\Release\*.*
xcopy /y ..\src\VS2003\gcodesearch\bin\Release\*.dll ..\lib\Release\*.*
xcopy /y ..\src\VS2003\gapps\bin\Release\*.dll ..\lib\Release\*.*


# copy the xml doc files
xcopy /y ..\src\VS2003\gdata\bin\*.xml ..\docs\*.*
xcopy /y ..\src\VS2003\gbase\bin\*.xml ..\docs\*.*
xcopy /y ..\src\VS2003\gextension\bin\*.xml ..\docs\*.*
xcopy /y ..\src\VS2003\gspreadsheets\bin\*.xml ..\docs\*.*
xcopy /y ..\src\VS2003\gcalendar\bin\*.xml ..\docs\*.*
xcopy /y ..\src\VS2003\gcodesearch\bin\*.xml ..\docs\*.*
xcopy /y ..\src\VS2003\gapps\bin\*.xml ..\docs\*.*

# now build the documentation
"%ProgramFiles%\NDoc 1.3\bin\net\1.1\NDocConsole.exe" -project=GDataProject.ndoc 
# the tree.cs generated is buggy, use this one
copy tree.cs doc\tree.js
copy doc\Documentation.chm ..\docs\Documentation.chm
del doc\Documentation.*
rd -s -q doc\ndoc_msdn_temp
wzzip HTMLDocumentation.zip doc/*.*
rd -s -q doc
copy HTMLDocumentation.zip ..\docs\HTMLDocumentation.zip
del HTMLDocumentation.zip 

