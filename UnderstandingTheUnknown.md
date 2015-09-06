# Introduction #

As you might have noticed, there are always new elements appearing in the atom
streams of properties, extensions that are useful to the individual property,
and therefore, useful to at least some of the developers working with those
properties.


# Details #

One example for this is the draft element in the beta.blogger.com feeds. The
draft element is a subelement of an atom:entry, and looks pretty much like this:

```
<entry xmlns:app='http://purl.org/atom/app#'>
  ...
  <app:control>
    <app:draft>yes</app:draft>
  </app:control>
</entry>
```

What this does, for blogger, is, it marks this entry as a draft, as not yet
finished.

Now this is all well, but, if you are using a premade library to deal with
the content, how do you suddenly insert a new element? You can obviously wait
for an updated release, that supports this new element. Or, you can do it
yourself.

For this, you need to understand a little bit about the way the current parser
works. The parser is based on XmlReader, not the XmlDom, and parses the stream
using the XmlReader interfaces to encounter all the Atom/GData elements. For
things the parser understands, he creates the corresponding objects, like
AtomEntry, and fills the properties of that object.

But what happens with things that are not understood? They end up as an element
of the ExtensionElements collection, that is a member of all classes inherited
from AtomBase, like AtomFeed, AtomEntry, EventEntry etc...

That means for something like a blogger entry that would have an app:control
element in it, that this xml snippet is not lost. It's only not directly exposed
as a property, but, instead, converted into an XmlNode and then put into
the ExtensionElements collection.

This can actually be intercepted, by hooking the FeedParserEventHandler (which
fires events when new elements are created) and the ExtensionElementEventHandler
(which fires events when unknown entities are encountered). By intercepting and
overriding the default behaviour you are able to substitute your own objects
for the default objects created. This is the way the CalendarFeed/EventEntry
are implemented, and all other extensions as well.

But, this is a more advanced topic for another time. Back to the ExtensionElments
collection. Everything in that collection is potentially persisted. is inheriting from IExtensionElement, it will be saved into the xml stream when it's owner is persisted.

Hence to figure out if a certain attribute is set you can do something like this:

```
    foreach (Object obj in entry.ExtensionElements) 
      if obj is XmlExtension
         ... check whatever it is you are looking for
      
```

If also means that the following code:

```
      XmlDocument doc = new XmlDocument();

     doc. LoadXml( "<app:control xmlns:app='http://purl.org/atom/app#>
                <app:draft>yes</app:draft>
              </app:control>");

     XmlNode node = doc.DocumentElement;
     XmlExtension x = new XmlExtension(node);
      AtomEntry entry = .... 
      entry.ExtensionElements.Add(x);
      
      entry.Update();
   
```

will create the required xml during the update/insert process to indicate this to be a draft element.