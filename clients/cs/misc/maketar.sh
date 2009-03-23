#!/bin/bash
# 
# uses the .NET client subversion repository to create a tar ball for redistribution

# globals 

declare -rx SCRIPT=${O##*/}
declare -rx SVNLOCATION="http://google-gdata.googlecode.com/svn/trunk/clients/cs/"
declare -rx SVN="/usr/local/bin/svn"
declare -rx OUTPUTDIR="libgoogle-data-mono-1.4.0.2"
declare -rx EXPORTDIR="$OUTPUTDIR"

# sanity checks

if test -z "$BASH" ; then
  printf "$SCRIPT:$LINENO: please run this script with the bash shell\n" >&2
  exit 192
fi 


if test ! -x "$SVN" ; then
  printf "$SCRIPT:$LINENO: the $SVN command to run subversion could not be found\n" >&2
  exit 192
fi 

# if the output dir already exists, delete the contents in there
if test -x "$OUTPUTDIR" ; then
  printf "Removing old output directory $OUTPUTDIR\n" >&1
  rm -r -f -d "$OUTPUTDIR"
fi


printf "Starting to export to: $EXPORTDIR\n" >&1
svn export $SVNLOCATION $EXPORTDIR

# now remove all NON unix related dlls etc

rm -r -f -d $EXPORTDIR/lib
rm -r -f -d $EXPORTDIR/docs
rm -r -f -d $EXPORTDIR/fxcop
rm -r -f -d $EXPORTDIR/setup
rm -r -f -d $EXPORTDIR/samples/YouTubeSample/Bin
find $EXPORTDIR -iname *.dll -type f -delete

tar czf "$OUTPUTDIR.tar.gz" "$OUTPUTDIR"

printf "$SCRIPT:$LINENO: done\n" >&1



