/* Copyright (c) 2006 Google Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
*/

The C# code is developed and tested using .NET 1.1, and Mono version
1.1.13.2, on the Macintosh OS X. It should build and run on any platform that has
Mono available.

To build:

Using Mono.

- install Mono from www.mono-project.com. Choose a runtime at least as new 
   as the one this code was developed with. Mono does include NUnit, which 
   is used as a test framework for some of the sample code
- go to the clients/cs directory and just type make, it should build


Using Visual Studio, .NET 1.1

- there is 1 solution file. It is in clients/cs/src/VS2003 called gdata.sln. This 
  builds the core library, the extensions and the calendar specifics. 
- you will need to install NUNIT from www.nunit.org, if you plan to run 
  unittests. The unittests are in clients/cs/misc/unittests. They have a seperate 
  project file that is not part of above solution (as that would require everyone
  to install nunit). This project file is located in the clients/cs/src/VS2003 
  directory.
  
The unit tests

If you are interested to run unittests, or just inspect the code in general, 
the servers spoken to are described in the unittest.dll.config file, which 
you can find in the clients/cs/src/unittests directory. It's a standard 
.NET config file, and the important parameters are: 
  - defHost. This string identifies the read/write tests host. 
  - defRemoteHost. This string identifies the default remote host for tests
  - The Host1 to HostN identify further remote hosts used to pull data from
  - calendarURI specifies the URI to your calendar feed to test
  - userName is the username to use against the CalendarURI
  - passWord is the password to use against the CalendarURI
  
Documentation
You find compiler generated .XML documentation in the clients/cs/docs directory.
There is also an NDocs generated compiled help file. You can of course use 
NDocs to generated different documentation at your leisure. 

The calendar specific extensions

To work with the calendar, take a look at the clients/cs/samples directory
and load main.cs into your editor of choice. 

using Google.GData.Client;
using Google.GData.Extensions;
using Google.GData.Calendar;

the Extensions namespace provides extensions to use when dealing with elements
out of the GData namespace. The Calendar namespace provides a special 
service implementation to deal with the calendar feed.



The batch support

Release 1.0.5 includes support for the GoogleBatch protocol extensions. Refer to 
the code.google.com documentation for details on this in general. 

In the C# libraries this is implemented as basic support on the AtomFeed and AtomEntry.
Those objects have a new member, called BatchData. Setting this data controls the 
operations executed on the batchfeed, and this object also holds the return values 
for from the server. 

To create a feed useful to talk to the batch service, you need to know the service URI 
for this. Here is a code snippet that retrieves that URI.

    FeedQuery query = new FeedQuery();
    Service service = new Service("gbase", "mytestapplication"); 
    NetworkCredential nc = new NetworkCredential(userName, passWord); 
    service.Credentials = nc;

    // setup the google web key
    GDataGAuthRequestFactory authFactory = service.RequestFactor as GDataGAuthRequestFactory; 
    authFactory.GoogleWebKey = "yourkey"; 

    query.Uri = new Uri("http://base.google.com/base/feeds/items");
    AtomFeed baseFeed = service.Query(query);

    // this should have a batch URI
    if (baseFeed.Batch != null)  {
    .... 


Note, that to talk to GoogleBase, you also need a web developer key, you can see above
that, once you have that key, you only need to set the GoogleWebKey property on the service
to use it. 

Now to set the default operation you want the batchfeed to do, you use code similiar to this:

    batchFeed.BatchData = new GDataBatchFeedData();
    batchFeed.BatchData.Type = GDataBatchOperationType.delete; 

If you do not set this, the feed will default to insert as it's operation type.

You would then go and add entries to your feed. If you want the entry to behave differently
than the feed itself, you set the BatchData object on the entry. 

    entry.BatchData = new GDataBatchEntryData();
    entry.BatchData.Type = GDataBatchOperationType.insert; 
    entry.BatchData.Id = "some id"; 

To finally do the batch, you just call the new service method for this purpose:

    AtomFeed resultFeed = service.Batch(batchFeed, new Uri(baseFeed.Batch));

To verify that the operations were successfull, you need to iterate over the returned entries:

        foreach (AtomEntry resultEntry in resultFeed.Entries )
        {
            GDataBatchEntryData data = resultEntry.BatchData;
            switch (data.Stutus.Code) {
               case 200:....
            }
        }

For more details check the online documentation for batch and look into the unittests/gbase.cs file. 

        
