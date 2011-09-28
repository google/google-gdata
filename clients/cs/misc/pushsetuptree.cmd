cd ./..
rd /S /Q exporttree
rd /S /Q setuptree

svn export http://google-gdata.googlecode.com/svn/trunk/clients/cs/ exporttree\
mkdir setuptree
xcopy /E exporttree\src\*.* setuptree\Sources\Library\*.* 
xcopy /E exporttree\samples\*.* setuptree\Sources\Samples\*.*
xcopy /E exporttree\version\*.* setuptree\Sources\version\*.*
xcopy exporttree\docs\*.chm setuptree\Documentation\*.*
xcopy exporttree\*.txt setuptree\Documentation\*.*
xcopy exporttree\*.html setuptree\Documentation\*.*
xcopy exporttree\*.url setuptree\*.*

xcopy /E exporttree\lib\Release\*.dll setuptree\Redist\*.*
xcopy /E exporttree\lib\Release\*.exe setuptree\Samples\*.exe
xcopy /E exporttree\lib\ASP.NET\*.* setuptree\Redist\ASP.NET\*.*
xcopy /E exporttree\misc\icons\*.* setuptree\Icons\*.* 
rd /S /Q exporttree

