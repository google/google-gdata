CSC1=mcs
CSC=gmcs

DEFINES=/d:TRACE

VERSION=$(shell egrep AssemblyVersion version/AssemblyVersion.cs | egrep -o \([[:digit:]]\.\)+[[:digit:]]+)
PREFIX=/usr/local
PKGCONFIGDIR = $(PREFIX)/lib/pkgconfig

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
	Google.GData.Photos.dll		\
	Google.GData.Documents.dll 	\
	Google.GData.AccessControl.dll 	\
	Google.GData.Contacts.dll	\
	Google.GData.YouTube.dll	\
	Google.GData.Health.dll \
	Google.GData.Blogger.dll \
	Google.GData.Analytics.dll \
	
UNINSTALLLIBS = $(basename $(ALLLIBS))

FRAMEWORK_REFS = -r:System.dll -r:System.Xml.dll -r:System.Configuration.dll

PKGCONFIG_FILES = $(patsubst %.pc.in,%.pc,$(wildcard misc/*.pc.in))

COREREFS = $(addprefix -r:,$(CORELIBS)) $(FRAMEWORK_REFS) 
ALLREFS = $(addprefix -r:,$(ALLLIBS)) $(FRAMEWORK_REFS)
ACLREFS = $(addprefix -r:,$(ACLLIBS)) $(FRAMEWORK_REFS)


samples =	\
	execrequest.exe			\
	gapps_appsdemo.exe \
	gapps_migration_sample.exe \
	gblogger_demo.exe \
	gcal_demo.exe \
	gspreadsheet_demo.exe \
	PhotoTool.exe \
	HealthTool.exe \

all: $(ALLLIBS) tests $(samples) $(PKGCONFIG_FILES)

core_sources = $(wildcard src/core/*.cs) $(wildcard version/*.cs)
extensions_sources = $(wildcard src/extensions/*.cs) $(wildcard version/*.cs)
gcodesearch_sources = $(wildcard src/gcodesearch/*.cs) $(wildcard version/*.cs)
gspreadsheet_sources = $(wildcard src/gspreadsheet/*.cs) $(wildcard version/*.cs)
gcalendar_sources =  $(wildcard src/gcalendar/*.cs) $(wildcard version/*.cs)
gacl_sources = $(wildcard src/gacl/*.cs) $(wildcard version/*.cs)
gapps_sources = $(wildcard src/gapps/*.cs) $(wildcard version/*.cs)
gphotos_sources = $(wildcard src/gphotos/*.cs) $(wildcard version/*.cs)
gdocuments_sources = $(wildcard src/documents3/*.cs) $(wildcard version/*.cs)
gcontact_sources = $(wildcard src/gcontacts/*.cs) $(wildcard version/*.cs)
youtube_sources = $(wildcard src/youtube/*.cs) $(wildcard version/*.cs)
health_sources = $(wildcard src/ghealth/*.cs) $(wildcard version/*.cs)
blogger_sources = $(wildcard src/blogger/*.cs) $(wildcard version/*.cs)
analytics_sources = $(wildcard src/analytics/*.cs) $(wildcard version/*.cs)
test_sources = $(wildcard src/unittests/*.cs) $(wildcard src/unittests/gapps/*.cs)\
				 $(wildcard src/unittests/spreadsheets/*.cs) $(wildcard src/unittests/youtube/*.cs)  $(wildcard src/version/*cs)\
				 $(wildcard src/unittests/core/*.cs)  $(wildcard src/unittests/picasa/*.cs)

Google.GData.Client.dll: $(core_sources) src/core/clientkey.sn
	$(CSC) $(DEFINES) -target:library -out:$@ $(core_sources) $(FRAMEWORK_REFS) -keyfile:src/core/clientkey.sn

Google.GData.Extensions.dll: Google.GData.Client.dll $(extension_sources) src/extensions/extensionkey.sn
	$(CSC) $(DEFINES) -target:library -out:$@ -r:Google.GData.Client.dll $(extensions_sources) $(FRAMEWORK_REFS) -keyfile:src/extensions/extensionkey.sn

Google.GData.Calendar.dll: $(ACLLIBS) $(gcalendar_sources) src/gcalendar/calendarkey.sn
	$(CSC) -target:library -out:$@ $(ACLREFS) $(gcalendar_sources) -keyfile:src/gcalendar/calendarkey.sn

Google.GData.CodeSearch.dll: $(CORELIBS) $(gcodesearch_sources) src/gcodesearch/codesearch.sn
	$(CSC) -target:library -out:$@ $(COREREFS) $(gcodesearch_sources) -keyfile:src/gcodesearch/codesearch.sn

Google.GData.Spreadsheets.dll: $(CORELIBS) $(gspreadsheet_sources) src/gspreadsheet/spreadsheets.sn
	$(CSC) -target:library -out:$@ $(COREREFS) $(gspreadsheet_sources) -keyfile:src/gspreadsheet/spreadsheets.sn

Google.GData.AccessControl.dll: $(CORELIBS) $(gacl_sources) src/gacl/gacl.sn
	$(CSC) -target:library -out:$@ $(COREREFS) $(gacl_sources) -keyfile:src/gacl/gacl.sn

Google.GData.Apps.dll: $(CORELIBS) $(gapps_sources) src/gapps/gapps.sn
	$(CSC) -target:library -out:$@ $(COREREFS) $(gapps_sources) -keyfile:src/gapps/gapps.sn

Google.GData.Photos.dll: $(CORELIBS) $(gphotos_sources) src/gphotos/picasa.sn
	$(CSC) $(DEFINES) -target:library -out:$@ $(COREREFS) $(gphotos_sources) -keyfile:src/gphotos/picasa.sn

Google.GData.Documents.dll: $(ACLLIBS) $(gdocuments_sources) src/documents3/documents.sn
	$(CSC) $(DEFINES) -target:library -out:$@ $(ACLREFS) $(gdocuments_sources) -keyfile:src/documents3/documents.sn

Google.GData.Contacts.dll: $(CORELIBS) $(gcontact_sources) src/gcontacts/contacts.sn
	$(CSC) $(DEFINES) -target:library -out:$@ $(COREREFS) $(gcontact_sources) -keyfile:src/gcontacts/contacts.sn

Google.GData.YouTube.dll: $(CORELIBS) $(youtube_sources) src/youtube/youtube.sn
	$(CSC) $(DEFINES) -target:library -out:$@ $(COREREFS) $(youtube_sources) -keyfile:src/youtube/youtube.sn

Google.GData.Health.dll: $(CORELIBS) $(health_sources) src/ghealth/ghealth.sn
	$(CSC) $(DEFINES) -target:library -out:$@ $(COREREFS) $(health_sources) -keyfile:src/ghealth/ghealth.sn

Google.GData.Blogger.dll: $(CORELIBS) $(blogger_sources) src/blogger/blogger.sn
	$(CSC) $(DEFINES) -target:library -out:$@ $(COREREFS) $(blogger_sources) -keyfile:src/blogger/blogger.sn

Google.GData.Analytics.dll: $(CORELIBS) $(analytics_sources) src/analytics/analytics.sn
	$(CSC) $(DEFINES) -target:library -out:$@ $(COREREFS) $(analytics_sources) -keyfile:src/analytics/analytics.sn


clean:
	-rm -f *.sn *.dll *exe $(PKGCONFIG_FILES)

install: all
	for i in $(ALLLIBS); do gacutil -i $$i -package GData-Sharp -root "$(DESTDIR)$(PREFIX)/lib"; done
	install -d "$(DESTDIR)$(PKGCONFIGDIR)"
	install -m 644 -t "$(DESTDIR)$(PKGCONFIGDIR)" $(PKGCONFIG_FILES) 

uninstall:
	for i in $(UNINSTALLLIBS); do gacutil -u $$i -package GData-Sharp -root "$(DESTDIR)$(PREFIX)/lib"; done
	for i in $(PKGCONFIG_FILES) ; do rm "$(DESTDIR)$(PKGCONFIGDIR)/$$i" ; done

tests: $(ALLLIBS)
	$(CSC) $(DEFINES) -target:library -out:unittests.dll $(ALLREFS) -r:nunit.framework.dll $(test_sources)

gapps_sample_sources = samples/appsforyourdomain/appsdemo.cs
gapps_appsdemo.exe: $(ALLLIBS) $(gapps_sample_sources)
	$(CSC) -out:$@ $(ALLREFS) $(gapps_sample_sources)

gapps_migration_sample_sources = samples/appsforyourdomain/migration/migrationsample.cs
gapps_migration_sample.exe: $(ALLLIBS) $(gapps_migration_sample_sources)
	$(CSC) -out:$@ $(ALLREFS) $(gapps_migration_sample_sources)

execrequest_sources = $(wildcard samples/execrequest/*.cs)
execrequest.exe: $(ALLLIBS) $(execrequest_sources)
	$(CSC) -out:$@ $(ALLREFS) $(execrequest_sources)

gblogger_sample_sources = samples/blogger/ConsoleSample.cs
gblogger_demo.exe: $(ALLLIBS) $(gblogger_sample_sources)
	$(CSC) -out:$@ $(ALLREFS) $(gblogger_sample_sources)

gcal_demo_sources = samples/calendar/console/CalendarDemo.cs
gcal_demo.exe: $(ALLLIBS) $(gcal_demo_sources)
	$(CSC) -out:$@ $(ALLREFS) $(gcal_demo_sources)

gspreadsheet_demo_sources = samples/spreadsheets/spreadsheetdemo.cs
gspreadsheet_demo.exe: $(ALLLIBS) $(gspreadsheet_demo_sources)
	$(CSC) -out:$@ $(ALLREFS) $(gspreadsheet_demo_sources)

PhotoTool_sources = $(wildcard samples/PhotoBrowser/*.cs)
PhotoTool.exe: $(ALLLIBS) $(PhotoTool_sources)
	$(CSC) -out:$@ $(ALLREFS) -r:System.Windows.Forms -r:System.Drawing -r:System.Data $(PhotoTool_sources)
	
healthtool_sources = $(wildcard samples/health/*.cs)
HealthTool.exe: $(ALLLIBS) $(healthtool_sources)
	$(CSC) -out:$@ $(ALLREFS) $(healthtool_sources)

test:
	nunit-console2 unittests.dll

%.pc : %.pc.in
	sed "s,\@VERSION\@,$(VERSION),g; s,\@prefix\@,$(PREFIX),g" < $< > $@
