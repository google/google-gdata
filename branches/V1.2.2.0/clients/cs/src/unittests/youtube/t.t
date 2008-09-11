NUnit version 2.2.0
Copyright (C) 2002-2003 James W. Newkirk, Michael C. Two, Alexei A. Vorontsov, Charlie Poole.
Copyright (C) 2000-2003 Philip Craig.
All Rights Reserved.

OS Version: Unix 9.4.0.0    Mono Version: 2.0.50727.42

Included categories: YouTube
................F.F..F............................F.F.F.F.F.F.F.F.F.F.....F.F.F.F.F.F.F.F.F.F.F.F.F.F.F.F.F.F.F.................F.F.F.F.F.F.F...F.F.F.F.F.F.F.F..F.F.F.F.F.F.F.F.F.F.F.F..F.F.F

Tests run: 129, Failures: 62, Not run: 0, Time: 0.641833 seconds

Tests run: 129, Failures: 62, Not run: 0, Time: 0.641833 seconds

Failures:
1) Google.GData.Client.UnitTests.YouTube.FormUploadTokenTest.UrlTest : System.NullReferenceException : Object reference not set to an instance of an object
  at System.Xml.XmlInputStream.Initialize (System.IO.Stream stream) [0x0001f] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/System.XML/System.Xml/XmlInputStream.cs:362 
  at System.Xml.XmlInputStream..ctor (System.IO.Stream stream) [0x00006] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/System.XML/System.Xml/XmlInputStream.cs:341 
  at (wrapper remoting-invoke-with-check) System.Xml.XmlInputStream:.ctor (System.IO.Stream)
  at System.Xml.XmlStreamReader..ctor (System.IO.Stream input) [0x00000] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/System.XML/System.Xml/XmlInputStream.cs:50 
  at (wrapper remoting-invoke-with-check) System.Xml.XmlStreamReader:.ctor (System.IO.Stream)
  at System.Xml.XmlTextReader..ctor (System.IO.Stream input, System.Xml.XmlNameTable nt) [0x00000] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/System.XML/System.Xml/XmlTextReader2.cs:78 
  at System.Xml.XmlDocument.Load (System.IO.Stream inStream) [0x00000] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/System.XML/System.Xml/XmlDocument.cs:646 
  at Google.GData.YouTube.FormUploadToken..ctor (System.IO.Stream stream) [0x00000] 
  at Google.GData.Client.UnitTests.YouTube.FormUploadTokenTest.UrlTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

2) Google.GData.Client.UnitTests.YouTube.FormUploadTokenTest.TokenTest : System.NullReferenceException : Object reference not set to an instance of an object
  at System.Xml.XmlInputStream.Initialize (System.IO.Stream stream) [0x0001f] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/System.XML/System.Xml/XmlInputStream.cs:362 
  at System.Xml.XmlInputStream..ctor (System.IO.Stream stream) [0x00006] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/System.XML/System.Xml/XmlInputStream.cs:341 
  at (wrapper remoting-invoke-with-check) System.Xml.XmlInputStream:.ctor (System.IO.Stream)
  at System.Xml.XmlStreamReader..ctor (System.IO.Stream input) [0x00000] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/System.XML/System.Xml/XmlInputStream.cs:50 
  at (wrapper remoting-invoke-with-check) System.Xml.XmlStreamReader:.ctor (System.IO.Stream)
  at System.Xml.XmlTextReader..ctor (System.IO.Stream input, System.Xml.XmlNameTable nt) [0x00000] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/System.XML/System.Xml/XmlTextReader2.cs:78 
  at System.Xml.XmlDocument.Load (System.IO.Stream inStream) [0x00000] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/System.XML/System.Xml/XmlDocument.cs:646 
  at Google.GData.YouTube.FormUploadToken..ctor (System.IO.Stream stream) [0x00000] 
  at Google.GData.Client.UnitTests.YouTube.FormUploadTokenTest.TokenTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

3) Google.GData.Client.UnitTests.YouTube.FormUploadTokenTest.FormUploadTokenConstructorTest : System.NullReferenceException : Object reference not set to an instance of an object
  at System.Xml.XmlInputStream.Initialize (System.IO.Stream stream) [0x0001f] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/System.XML/System.Xml/XmlInputStream.cs:362 
  at System.Xml.XmlInputStream..ctor (System.IO.Stream stream) [0x00006] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/System.XML/System.Xml/XmlInputStream.cs:341 
  at (wrapper remoting-invoke-with-check) System.Xml.XmlInputStream:.ctor (System.IO.Stream)
  at System.Xml.XmlStreamReader..ctor (System.IO.Stream input) [0x00000] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/System.XML/System.Xml/XmlInputStream.cs:50 
  at (wrapper remoting-invoke-with-check) System.Xml.XmlStreamReader:.ctor (System.IO.Stream)
  at System.Xml.XmlTextReader..ctor (System.IO.Stream input, System.Xml.XmlNameTable nt) [0x00000] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/System.XML/System.Xml/XmlTextReader2.cs:78 
  at System.Xml.XmlDocument.Load (System.IO.Stream inStream) [0x00000] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/System.XML/System.Xml/XmlDocument.cs:646 
  at Google.GData.YouTube.FormUploadToken..ctor (System.IO.Stream stream) [0x00000] 
  at Google.GData.Client.UnitTests.YouTube.FormUploadTokenTest.FormUploadTokenConstructorTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

4) Google.GData.Client.UnitTests.YouTube.PlaylistEntryTest.PositionTest : Verify the correctness of this test method.
  at Google.GData.Client.UnitTests.YouTube.PlaylistEntryTest.PositionTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

5) Google.GData.Client.UnitTests.YouTube.PlaylistEntryTest.DescriptionTest : Verify the correctness of this test method.
  at Google.GData.Client.UnitTests.YouTube.PlaylistEntryTest.DescriptionTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

6) Google.GData.Client.UnitTests.YouTube.PlaylistEntryTest.PlaylistEntryConstructorTest : TODO: Implement code to verify target
  at Google.GData.Client.UnitTests.YouTube.PlaylistEntryTest.PlaylistEntryConstructorTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

7) Google.GData.Client.UnitTests.YouTube.PlaylistFeedTest.CreateFeedEntryTest : 
	expected:<(null)>
	 but was:<Google.GData.YouTube.PlaylistEntry>
  at Google.GData.Client.UnitTests.YouTube.PlaylistFeedTest.CreateFeedEntryTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

8) Google.GData.Client.UnitTests.YouTube.PlaylistFeedTest.PlaylistFeedConstructorTest : TODO: Implement code to verify target
  at Google.GData.Client.UnitTests.YouTube.PlaylistFeedTest.PlaylistFeedConstructorTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

9) Google.GData.Client.UnitTests.YouTube.PlaylistsEntryTest.FeedLinkTest : Verify the correctness of this test method.
  at Google.GData.Client.UnitTests.YouTube.PlaylistsEntryTest.FeedLinkTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

10) Google.GData.Client.UnitTests.YouTube.PlaylistsEntryTest.DescriptionTest : Verify the correctness of this test method.
  at Google.GData.Client.UnitTests.YouTube.PlaylistsEntryTest.DescriptionTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

11) Google.GData.Client.UnitTests.YouTube.PlaylistsEntryTest.PlaylistsEntryConstructorTest : TODO: Implement code to verify target
  at Google.GData.Client.UnitTests.YouTube.PlaylistsEntryTest.PlaylistsEntryConstructorTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

12) Google.GData.Client.UnitTests.YouTube.PlaylistsFeedTest.CreateFeedEntryTest : 
	expected:<(null)>
	 but was:<Google.GData.YouTube.PlaylistsEntry>
  at Google.GData.Client.UnitTests.YouTube.PlaylistsFeedTest.CreateFeedEntryTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

13) Google.GData.Client.UnitTests.YouTube.PlaylistsFeedTest.PlaylistsFeedConstructorTest : TODO: Implement code to verify target
  at Google.GData.Client.UnitTests.YouTube.PlaylistsFeedTest.PlaylistsFeedConstructorTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

14) Google.GData.Client.UnitTests.YouTube.ProfileEntryTest.UserNameTest : Verify the correctness of this test method.
  at Google.GData.Client.UnitTests.YouTube.ProfileEntryTest.UserNameTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

15) Google.GData.Client.UnitTests.YouTube.ProfileEntryTest.StatisticsTest : Verify the correctness of this test method.
  at Google.GData.Client.UnitTests.YouTube.ProfileEntryTest.StatisticsTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

16) Google.GData.Client.UnitTests.YouTube.ProfileEntryTest.SchoolTest : Verify the correctness of this test method.
  at Google.GData.Client.UnitTests.YouTube.ProfileEntryTest.SchoolTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

17) Google.GData.Client.UnitTests.YouTube.ProfileEntryTest.OccupationTest : Verify the correctness of this test method.
  at Google.GData.Client.UnitTests.YouTube.ProfileEntryTest.OccupationTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

18) Google.GData.Client.UnitTests.YouTube.ProfileEntryTest.MusicTest : Verify the correctness of this test method.
  at Google.GData.Client.UnitTests.YouTube.ProfileEntryTest.MusicTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

19) Google.GData.Client.UnitTests.YouTube.ProfileEntryTest.MoviesTest : Verify the correctness of this test method.
  at Google.GData.Client.UnitTests.YouTube.ProfileEntryTest.MoviesTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

20) Google.GData.Client.UnitTests.YouTube.ProfileEntryTest.MediaThumbnailTest : Verify the correctness of this test method.
  at Google.GData.Client.UnitTests.YouTube.ProfileEntryTest.MediaThumbnailTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

21) Google.GData.Client.UnitTests.YouTube.ProfileEntryTest.LocationTest : Verify the correctness of this test method.
  at Google.GData.Client.UnitTests.YouTube.ProfileEntryTest.LocationTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

22) Google.GData.Client.UnitTests.YouTube.ProfileEntryTest.LastnameTest : Verify the correctness of this test method.
  at Google.GData.Client.UnitTests.YouTube.ProfileEntryTest.LastnameTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

23) Google.GData.Client.UnitTests.YouTube.ProfileEntryTest.HobbiesTest : Verify the correctness of this test method.
  at Google.GData.Client.UnitTests.YouTube.ProfileEntryTest.HobbiesTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

24) Google.GData.Client.UnitTests.YouTube.ProfileEntryTest.GenderTest : Verify the correctness of this test method.
  at Google.GData.Client.UnitTests.YouTube.ProfileEntryTest.GenderTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

25) Google.GData.Client.UnitTests.YouTube.ProfileEntryTest.FirstnameTest : Verify the correctness of this test method.
  at Google.GData.Client.UnitTests.YouTube.ProfileEntryTest.FirstnameTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

26) Google.GData.Client.UnitTests.YouTube.ProfileEntryTest.FeedLinksTest : Verify the correctness of this test method.
  at Google.GData.Client.UnitTests.YouTube.ProfileEntryTest.FeedLinksTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

27) Google.GData.Client.UnitTests.YouTube.ProfileEntryTest.CompanyTest : Verify the correctness of this test method.
  at Google.GData.Client.UnitTests.YouTube.ProfileEntryTest.CompanyTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

28) Google.GData.Client.UnitTests.YouTube.ProfileEntryTest.BooksTest : Verify the correctness of this test method.
  at Google.GData.Client.UnitTests.YouTube.ProfileEntryTest.BooksTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

29) Google.GData.Client.UnitTests.YouTube.ProfileEntryTest.AgeTest : Verify the correctness of this test method.
  at Google.GData.Client.UnitTests.YouTube.ProfileEntryTest.AgeTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

30) Google.GData.Client.UnitTests.YouTube.ProfileEntryTest.ProfileEntryConstructorTest : TODO: Implement code to verify target
  at Google.GData.Client.UnitTests.YouTube.ProfileEntryTest.ProfileEntryConstructorTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

31) Google.GData.Client.UnitTests.YouTube.ProfileFeedTest.CreateFeedEntryTest : 
	expected:<(null)>
	 but was:<Google.GData.YouTube.ProfileEntry>
  at Google.GData.Client.UnitTests.YouTube.ProfileFeedTest.CreateFeedEntryTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

32) Google.GData.Client.UnitTests.YouTube.ProfileFeedTest.ProfileFeedConstructorTest : TODO: Implement code to verify target
  at Google.GData.Client.UnitTests.YouTube.ProfileFeedTest.ProfileFeedConstructorTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

33) Google.GData.Client.UnitTests.YouTube.SubscriptionEntryTest.UserNameTest : Verify the correctness of this test method.
  at Google.GData.Client.UnitTests.YouTube.SubscriptionEntryTest.UserNameTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

34) Google.GData.Client.UnitTests.YouTube.SubscriptionEntryTest.TypeTest : Verify the correctness of this test method.
  at Google.GData.Client.UnitTests.YouTube.SubscriptionEntryTest.TypeTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

35) Google.GData.Client.UnitTests.YouTube.SubscriptionEntryTest.QueryStringTest : Verify the correctness of this test method.
  at Google.GData.Client.UnitTests.YouTube.SubscriptionEntryTest.QueryStringTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

36) Google.GData.Client.UnitTests.YouTube.SubscriptionEntryTest.FeedLinkTest : Verify the correctness of this test method.
  at Google.GData.Client.UnitTests.YouTube.SubscriptionEntryTest.FeedLinkTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

37) Google.GData.Client.UnitTests.YouTube.SubscriptionEntryTest.SubscriptionEntryConstructorTest : TODO: Implement code to verify target
  at Google.GData.Client.UnitTests.YouTube.SubscriptionEntryTest.SubscriptionEntryConstructorTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

38) Google.GData.Client.UnitTests.YouTube.SubscriptionFeedTest.CreateFeedEntryTest : 
	expected:<(null)>
	 but was:<Google.GData.YouTube.SubscriptionEntry>
  at Google.GData.Client.UnitTests.YouTube.SubscriptionFeedTest.CreateFeedEntryTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

39) Google.GData.Client.UnitTests.YouTube.SubscriptionFeedTest.SubscriptionFeedConstructorTest : TODO: Implement code to verify target
  at Google.GData.Client.UnitTests.YouTube.SubscriptionFeedTest.SubscriptionFeedConstructorTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

40) Google.GData.Client.UnitTests.YouTube.YouTubeBaseEntryTest.setYouTubeExtensionTest : System.NullReferenceException : Object reference not set to an instance of an object
  at Google.GData.Client.UnitTests.YouTube.YouTubeBaseEntryTest.setYouTubeExtensionTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

41) Google.GData.Client.UnitTests.YouTube.YouTubeBaseEntryTest.getYouTubeExtensionValueTest : System.NullReferenceException : Object reference not set to an instance of an object
  at Google.GData.Client.UnitTests.YouTube.YouTubeBaseEntryTest.getYouTubeExtensionValueTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

42) Google.GData.Client.UnitTests.YouTube.YouTubeBaseEntryTest.getYouTubeExtensionTest : System.NullReferenceException : Object reference not set to an instance of an object
  at Google.GData.Client.UnitTests.YouTube.YouTubeBaseEntryTest.getYouTubeExtensionTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

43) Google.GData.Client.UnitTests.YouTube.YouTubeEntryTest.MediaTest : Verify the correctness of this test method.
  at Google.GData.Client.UnitTests.YouTube.YouTubeEntryTest.MediaTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

44) Google.GData.Client.UnitTests.YouTube.YouTubeEntryTest.LocationTest : Verify the correctness of this test method.
  at Google.GData.Client.UnitTests.YouTube.YouTubeEntryTest.LocationTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

45) Google.GData.Client.UnitTests.YouTube.YouTubeEntryTest.setYouTubeExtensionTest : Verify the correctness of this test method.
  at Google.GData.Client.UnitTests.YouTube.YouTubeEntryTest.setYouTubeExtensionTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

46) Google.GData.Client.UnitTests.YouTube.YouTubeEntryTest.getYouTubeExtensionValueTest : 
	expected:<"secret text string">
	 but was:<(null)>
  at Google.GData.Client.UnitTests.YouTube.YouTubeEntryTest.getYouTubeExtensionValueTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

47) Google.GData.Client.UnitTests.YouTube.YouTubeEntryTest.getYouTubeExtensionTest : Verify the correctness of this test method.
  at Google.GData.Client.UnitTests.YouTube.YouTubeEntryTest.getYouTubeExtensionTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

48) Google.GData.Client.UnitTests.YouTube.YouTubeFeedTest.CreateFeedEntryTest : 
	expected:<(null)>
	 but was:<Google.GData.YouTube.YouTubeEntry>
  at Google.GData.Client.UnitTests.YouTube.YouTubeFeedTest.CreateFeedEntryTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

49) Google.GData.Client.UnitTests.YouTube.YouTubeFeedTest.YouTubeFeedConstructorTest : Object value should be identical after construction
  at Google.GData.Client.UnitTests.YouTube.YouTubeFeedTest.YouTubeFeedConstructorTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

50) Google.GData.Client.UnitTests.YouTube.YouTubeNameTableTest.YouTubeNameTableConstructorTest : Object value should be identical after construction
  at Google.GData.Client.UnitTests.YouTube.YouTubeNameTableTest.YouTubeNameTableConstructorTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

51) Google.GData.Client.UnitTests.YouTube.YouTubeQueryTest.VQTest : Verify the correctness of this test method.
  at Google.GData.Client.UnitTests.YouTube.YouTubeQueryTest.VQTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

52) Google.GData.Client.UnitTests.YouTube.YouTubeQueryTest.TimeTest : Verify the correctness of this test method.
  at Google.GData.Client.UnitTests.YouTube.YouTubeQueryTest.TimeTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

53) Google.GData.Client.UnitTests.YouTube.YouTubeQueryTest.RestrictionTest : Verify the correctness of this test method.
  at Google.GData.Client.UnitTests.YouTube.YouTubeQueryTest.RestrictionTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

54) Google.GData.Client.UnitTests.YouTube.YouTubeQueryTest.RacyTest : Verify the correctness of this test method.
  at Google.GData.Client.UnitTests.YouTube.YouTubeQueryTest.RacyTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

55) Google.GData.Client.UnitTests.YouTube.YouTubeQueryTest.OrderByTest : Verify the correctness of this test method.
  at Google.GData.Client.UnitTests.YouTube.YouTubeQueryTest.OrderByTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

56) Google.GData.Client.UnitTests.YouTube.YouTubeQueryTest.LRTest : Verify the correctness of this test method.
  at Google.GData.Client.UnitTests.YouTube.YouTubeQueryTest.LRTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

57) Google.GData.Client.UnitTests.YouTube.YouTubeQueryTest.FormatsTest : Verify the correctness of this test method.
  at Google.GData.Client.UnitTests.YouTube.YouTubeQueryTest.FormatsTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

58) Google.GData.Client.UnitTests.YouTube.YouTubeQueryTest.ClientTest : Verify the correctness of this test method.
  at Google.GData.Client.UnitTests.YouTube.YouTubeQueryTest.ClientTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

59) Google.GData.Client.UnitTests.YouTube.YouTubeQueryTest.YouTubeQueryConstructorTest1 : Object value should be identical after construction
  at Google.GData.Client.UnitTests.YouTube.YouTubeQueryTest.YouTubeQueryConstructorTest1 () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

60) Google.GData.Client.UnitTests.YouTube.YouTubeServiceTest.QueryTest : System.ArgumentNullException : The query argument MUST not be null
Parameter name: feedQuery
  at Google.GData.Client.Service.Query (Google.GData.Client.FeedQuery feedQuery, DateTime ifModifiedSince) [0x00000] 
  at Google.GData.Client.Service.Query (Google.GData.Client.FeedQuery feedQuery) [0x00000] 
  at Google.GData.YouTube.YouTubeService.Query (Google.GData.YouTube.YouTubeQuery feedQuery) [0x00000] 
  at Google.GData.Client.UnitTests.YouTube.YouTubeServiceTest.QueryTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

61) Google.GData.Client.UnitTests.YouTube.YouTubeServiceTest.OnRequestFactoryChangedTest : A method that does not return a value cannot be verified.
  at Google.GData.Client.UnitTests.YouTube.YouTubeServiceTest.OnRequestFactoryChangedTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 

62) Google.GData.Client.UnitTests.YouTube.YouTubeServiceTest.YouTubeServiceConstructorTest : Object value should be identical after construction
  at Google.GData.Client.UnitTests.YouTube.YouTubeServiceTest.YouTubeServiceConstructorTest () [0x00000] 
  at (wrapper managed-to-native) System.Reflection.MonoMethod:InternalInvoke (object,object[])
  at System.Reflection.MonoMethod.Invoke (System.Object obj, BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00055] in /private/tmp/monobuild/build/BUILD/mono-1.9/mcs/class/corlib/System.Reflection/MonoMethod.cs:149 


