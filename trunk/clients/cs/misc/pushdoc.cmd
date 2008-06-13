@echo off
set shfb_home=%ProgramFiles%\EWSoftware\Sandcastle Help File Builder
set src_home=..\src\VS2003
set out_dir=..\lib
set doc_dir=..\docs
set target_dir=..\docs\generated
set temp_dir=..\docs\TempHTML
time /t
rem build documentation
"%shfb_home%\SandcastleBuilderConsole.exe" GDataProject.shfb
move "%temp_dir%\Google.GData.Documentation.chm" "%doc_dir%\"
move "%temp_dir%\TempHTML\*.log" .

echo "%temp_dir%\*.*"
echo "%target_dir%\*.*"
move /y "%temp_dir%\*.*" "%target_dir%\"
move /y "%temp_dir%\html\*.*" "%target_dir%\html\"
time /t


