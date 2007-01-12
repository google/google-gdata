CSC1=gmcs
CSC=mcs

CORELIBS=\
	Google.GData.Client.dll		\
	Google.GData.Extensions.dll 

ALLLIBS = \
	Google.GData.Calendar.dll	\
	Google.GData.Client.dll		\
	Google.GData.CodeSearch.dll	\
	Google.GData.Extensions.dll	\
	Google.GData.GoogleBase.dll	\
	Google.GData.Spreadsheets.dll

FRAMEWORK_REFS = -r:System.dll -r:System.Xml.dll -r:System.Web.dll

COREREFS = $(addprefix -r:,$(CORELIBS)) $(FRAMEWORK_REFS)
ALLREFS = $(addprefix -r:,$(ALLLIBS)) $(FRAMEWORK_REFS)

samples =	\
	gbase_queryexample.exe 		\
	gbase_customertool.exe		\
	insertsample.exe		\
	updatesample.exe		\
	readsample.exe

all: $(ALL_LIBS) tests $(samples)

core_sources = $(wildcard src/core/*.cs) $(wildcard src/version/*cs.cs)
extensions_sources = $(wildcard src/extensions/*.cs) $(wildcard src/version/*cs.cs)
gbase_sources = $(wildcard src/gbase/*.cs) $(wildcard src/version/*cs.cs)
gcodesearch_sources = $(wildcard src/gcodesearch/*.cs) $(wildcard src/version/*cs.cs)
gspreadsheet_sources = $(wildcard src/gspreadsheet/*.cs) $(wildcard src/version/*cs.cs)
gcalendar_sources =  $(wildcard src/gcalendar/*.cs) $(wildcard src/version/*cs.cs)
test_sources = $(wildcard src/unittests/*.cs) $(wildcard src/unittests/gbase/*.cs) $(wildcard src/version/*cs)

Google.GData.Client.dll: $(core_sources) clientkey.sn
	$(CSC) -target:library -out:$@ $(core_sources) $(FRAMEWORK_REFS)

Google.GData.Extensions.dll: Google.GData.Client.dll $(extension_sources) extensionkey.sn
	$(CSC) -target:library -out:$@ -r:Google.GData.Client.dll $(extensions_sources) $(FRAMEWORK_REFS)

Google.GData.Calendar.dll: $(CORELIBS) $(gcalendar_sources) calendarkey.sn
	$(CSC) -target:library -out:$@ $(COREREFS) $(gcalendar_sources)

Google.GData.CodeSearch.dll: $(CORELIBS) $(gcodesearch_sources) codesearch.sn
	$(CSC) -target:library -out:$@ $(COREREFS) $(gcodesearch_sources)

Google.GData.GoogleBase.dll: $(CORELIBS) $(gbase_sources) gbase.sn
	$(CSC) -target:library -out:$@ $(COREREFS) $(gbase_sources)

Google.GData.Spreadsheets.dll: $(CORELIBS) $(gspreadsheet_sources) spreadsheets.sn
	$(CSC) -target:library -out:$@ $(COREREFS) $(gspreadsheet_sources)

%.sn: 
	cp src/signing/$@ .

clean:
	-rm *.sn *.dll *exe

install: all
	for i in $(ALL_LIBS); do gacutil -i $$i; done

uninstall:
	for i in $(ALL_LIBS); do gacutil -u $$i; done

tests: $(ALLLIBS)
	$(CSC) -target:library -out:unittests.dll $(ALLREFS) -r:nunit.framework.dll $(test_sources)

readsample_sources = $(wildcard samples/read/*.cs)
readsample.exe: $(ALLLIBS) $(readsample_sources)
	$(CSC) -out:$@ $(ALLREFS) $(readsample_sources)

updatesample_sources = $(wildcard samples/update/*.cs)
updatesample.exe: $(ALLLIBS) $(updatesample_sources)
	$(CSC) -out:$@ $(ALLREFS) $(updatesample_sources)

insertsample_sources = $(wildcard samples/read/*.cs)
insertsample.exe: $(ALLLIBS) $(insertsample_sources)
	$(CSC) -out:$@ $(ALLREFS) $(insertsample_sources)

gbase_customertool_sources = samples/gbase/example.cs \
	samples/gbase/customertool.cs	\
	samples/gbase/commands.cs

gbase_customertool.exe: $(ALLLIBS) $(gbase_customertool_sources)
	$(CSC) -out:$@ $(ALLREFS) $(gbase_customertool_sources)

gbase_queryexample_sources = samples/gbase/example.cs samples/gbase/queryexample.cs
gbase_queryexample.exe: $(ALLLIBS) $(gbase_queryexample_sources)
	$(CSC) -out:$@ $(ALLREFS) $(gbase_queryexample_sources)

test:
	nunit-console2 unittests.dll
