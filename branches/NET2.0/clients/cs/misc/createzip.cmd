@echo off
set shfb_home=%ProgramFiles%\EWSoftware\Sandcastle Help File Builder
set src_home=..\src\VS2003
set out_dir=..\lib
set doc_dir=..\docs

rem Create doc archive
del "%doc_dir%\HTMLDocumentation.zip"
del  "%doc_dir%\HTMLDocumentation\*.asp"
rd /S /Q  "%doc_dir%\HTMLDocumentation\fti"
rd /S /Q  "%doc_dir%\HTMLDocumentation\Working"
wzzip -apr "%doc_dir%\HTMLDocumentation.zip" "%doc_dir%\HTMLDocumentation\"
