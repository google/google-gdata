CSC1=gmcs
CSC=mcs

DEFINES=/d:TRACE

CORELIBS=\
	Google.GData.Client.dll		\
	Google.GData.Extensions.dll 	

ACLLIBS=\
	Google.GData.Client.dll		\
	Google.GData.Extensions.dll 	\
	Google.GData.AccessControl.dll


ALLLIBS = \
	Google.GData.Calendar.dll	\
	Google.GData.Client.dll		\
	Google.GData.CodeSearch.dll	\
	Google.GData.Extensions.dll	\
	Google.GData.GoogleBase.dll	\
	Google.GData.Spreadsheets.dll	\
	Google.GData.Apps.dll		\
	Google.GData.AccessControl.dll 

FRAMEWORK_REFS = -r:System.dll -r:System.Xml.dll -r:System.Web.dll

COREREFS = $(addprefix -r:,$(CORELIBS)) $(FRAMEWORK_REFS)
ALLREFS = $(addprefix -r:,$(ALLLIBS)) $(FRAMEWORK_REFS)
ACLREFS = $(addprefix -r:,$(ACLLIBS)) $(FRAMEWORK_REFS)


samples =	\
	gbase_queryexample.exe 		\
	gbase_customertool.exe		\
	insertsample.exe		\
	updatesample.exe		\
	readsample.exe			\
	execrequest.exe			\
	gapps_appsdemo.exe \
	gblogger_demo.exe \
	gspreadsheet_demo.exe

all: $(ALL_LIBS) tests $(samples)

core_sources = $(wildcard src/core/*.cs) $(wildcard src/version/*.cs)
extensions_sources = $(wildcard src/extensions/*.cs) $(wildcard src/version/*.cs)
gbase_sources = $(wildcard src/gbase/*.cs) $(wildcard src/version/*.cs)
gcodesearch_sources = $(wildcard src/gcodesearch/*.cs) $(wildcard src/version/*.cs)
gspreadsheet_sources = $(wildcard src/gspreadsheet/*.cs) $(wildcard src/version/*.cs)
gcalendar_sources =  $(wildcard src/gcalendar/*.cs) $(wildcard src/version/*.cs)
gacl_sources = $(wildcard src/gacl/*.cs) $(wildcard src/version/*.cs)
gapps_sources = $(wildcard src/gapps/*.cs) $(wildcard src/version/*cs.cs)
test_sources = $(wildcard src/unittests/*.cs) $(wildcard src/unittests/gbase/*.cs) $(wildcard src/unittests/gapps/*.cs) $(wildcard src/version/*cs)

Google.GData.Client.dll: $(core_sources) clientkey.sn
	$(CSC) $(DEFINES) -target:library -out:$@ $(core_sources) $(FRAMEWORK_REFS)

Google.GData.Extensions.dll: Google.GData.Client.dll $(extension_sources) extensionkey.sn
	$(CSC) -target:library -out:$@ -r:Google.GData.Client.dll $(extensions_sources) $(FRAMEWORK_REFS)

Google.GData.Calendar.dll: $(ACLLIBS) $(gcalendar_sources) calendarkey.sn
	$(CSC) -target:library -out:$@ $(ACLREFS) $(gcalendar_sources)

Google.GData.CodeSearch.dll: $(CORELIBS) $(gcodesearch_sources) codesearch.sn
	$(CSC) -target:library -out:$@ $(COREREFS) $(gcodesearch_sources)

Google.GData.GoogleBase.dll: $(CORELIBS) $(gbase_sources) gbase.sn
	$(CSC) -target:library -out:$@ $(COREREFS) $(gbase_sources)

Google.GData.Spreadsheets.dll: $(CORELIBS) $(gspreadsheet_sources) spreadsheets.sn
	$(CSC) -target:library -out:$@ $(COREREFS) $(gspreadsheet_sources)

Google.GData.AccessControl.dll: $(CORELIBS) $(gacl_sources) gacl.sn
	$(CSC) -target:library -out:$@ $(COREREFS) $(gacl_sources)


Google.GData.Apps.dll: $(CORELIBS) $(gapps_sources) gapps.sn
	$(CSC) -target:library -out:$@ $(COREREFS) $(gapps_sources)

%.sn: 
	cp src/signing/$@ .

clean:
	-rm *.sn *.dll *exe

install: all
	for i in $(ALL_LIBS); do gacutil -i $$i; done

uninstall:
	for i in $(ALL_LIBS); do gacutil -u $$i; done

tests: $(ALLLIBS)
	$(CSC) $(DEFINES) -target:library -out:unittests.dll $(ALLREFS) -r:nunit.framework.dll $(test_sources)

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

gapps_sample_sources = samples/appsforyourdomain/appsdemo.cs
gapps_appsdemo.exe: $(ALLLIBS) $(gapps_sample_sources)
	$(CSC) -out:$@ $(ALLREFS) $(gapps_sample_sources)

execrequest_sources = $(wildcard samples/execrequest/*.cs)
execrequest.exe: $(ALLLIBS) $(execrequest_sources)
	$(CSC) -out:$@ $(ALLREFS) $(execrequest_sources)

gblogger_sample_sources = samples/blogger/ConsoleSample.cs
gblogger_demo.exe: $(ALLLIBS) $(gblogger_sample_sources)
	$(CSC) -out:$@ $(ALLREFS) $(gblogger_sample_sources)

gspreadsheet_demo_sources = samples/spreadsheets/spreadsheetdemo.cs
gspreadsheet_demo.exe: $(ALLLIBS) $(gspreadsheet_demo_sources)
	$(CSC) -out:$@ $(ALLREFS) $(gspreadsheet_demo_sources)

test:
	nunit-console2 unittests.dll
