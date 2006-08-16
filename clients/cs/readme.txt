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
- install ant from ant.apache.org, if you do not have it installed already
- follow installation instructions of both products, and make sure that you can 
  access mono and ant in your shell.
- go to the clients/cs directory and just type ant, it should build


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

