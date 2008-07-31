@echo off
set shfb_home=%ProgramFiles%\EWSoftware\Sandcastle Help File Builder
set src_home=..\src\VS2003
set out_dir=..\lib
set doc_dir=..\docs
set target_dir=..\..\..\..\docs
set temp_dir=d:\tempHTML
time /t
rem build documentation
"%shfb_home%\SandcastleBuilderConsole.exe" GDataProject.shfb
move "%temp_dir%\Google.GData.Documentation.chm" "%doc_dir%\"
move "%temp_dir%\*.log" .

echo "%temp_dir%\*.*"
echo "%target_dir%\*.*"

move /y "%temp_dir%\*.*" "%target_dir%\"
move /y "%temp_dir%\html\*.*" "%target_dir%\html\"
move /y "%target_dir%\Index.html" "%target_dir%\orgindex.html"
move /y "%temp_dir%\AdditionalContent\*.*" "target_dir%\AdditionalContent\"
time /t


