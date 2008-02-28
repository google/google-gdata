cd ./..
rd /S /Q exporttree
svn export . exporttree\
rd /S /Q setuptree
mkdir setuptree
xcopy /E exporttree\src\*.* setuptree\Sources\Library\*.* 
xcopy /E exporttree\samples\*.* setuptree\Sources\Samples\*.*
xcopy exporttree\docs\*.chm setuptree\Documentation\*.*
xcopy /E exporttree\lib\Release\*.dll setuptree\Redist\*.*
xcopy /E exporttree\lib\Release\*.exe setuptree\Samples\*.exe
xcopy /E exporttree\lib\ASP.NET\*.* setuptree\Redist\ASP.NET\*.*
xcopy /E exporttree\lib\Mobile\*.* setuptree\Redist\Mobile\*.*
xcopy /E exporttree\misc\icons\*.* setuptree\Icons\*.* 
