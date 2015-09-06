# Introduction #

One of the things that allways show up here on the newsgroups, is the problem to
figure out why an execution to the server failed. In general, this is rather
simplistic to deal with on the C# side, but as not everyone is familiar with the
process, here is the scoop.


# Details #

When the webserver returns an error, an exception will be thrown. The C# client
code will catch this, package it into a GDataRequestException and throw that
one.

In the application code, therefore, when you do something like:

`myFeed.Insert(myNewEntry);`


what you should be doing is:

```
try { 
  myFeed.Insert(myNewEntry); 
} catch  (GDataRequestException e )  { 
// your error code here 
}

```

In general, the GDataRequestException class is just a  wrapper around the real
exception. It has the innerException, and it exposes the WebResponse, the object
that captures the server response, as a property named Response.

A WebResponse now has a bundle of properties, including a method
GetResponseStream, which you can use to get the real HTTP/Text that the server
returned. If you cast the WebResponse to an HttpWebResponse, you can get the
StatusCode and other, more HTTP specific information as well.

To make life easier, i just added a ResponseString property to the
GDataRequestException class, so that you can easier glance at additonal
information returned by the server. For those not at ease with the repository,
here is code that does the same in an exception handler:

```
catch (GDataRequestException e) { 
  Stream receiver = e.Response.GetResponseStream(); 
  if (receiver != null) { 
  // Pipe the stream to ahigher level stream reader with the default encoding 
  // format. which is UTF8 
  StreamReader readStream = new StreamReader(receiver);

  // Read 256 charcters at a time. 
  char []buffer = new char[256]; 
  StringBuilder builder = new StringBuilder(1024); 
  int count = readStream.Read( buffer, 0, 256); 
  while (count > 0) { 
    string onto the console. builder.Append(buffer); 
    count = readStream.Read(buffer,0, 256);
  }

  // Release the resources of stream object. 
  readStream.Close(); 
  receiver.Close();
  responseText = builder.ToString(); 
  }
}

```
At the end of the code, responseText will hold the complete HTTP/Text response
of the server. It might be safer to even check of the contenttype, before using
that string AS a string, but for debugging purposes this will do just fine.
