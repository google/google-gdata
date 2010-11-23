#@echo off
set out_dir=..\lib
set doc_dir=..\docs
set target_dir=..\..\..\..\docs
set temp_dir=c:\gdata.doc
time /t
rem build documentation
msbuild.exe gdatadocumentation.shfbproj
move "%temp_dir%\Google.GData.Documentation.chm" "%doc_dir%\"
move "%temp_dir%\*.log" .

echo "%temp_dir%\*.*"
echo "%target_dir%\*.*"

mkdir "%target_dir%\html"

copy /y "%temp_dir%\*.*" "%target_dir%\*.*"
copy /y "%temp_dir%\html\*.*" "%target_dir%\html\"
copy /y "%target_dir%\Index.html" "%target_dir%\orgindex.html"
copy /y "%temp_dir%\AdditionalContent\*.*" "%target_dir%\AdditionalContent\"
time /t


