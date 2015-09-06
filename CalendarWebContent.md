# Introduction #

One of the new features in the Google Calendar service is the ability to embed web content
in the UI (see here: http://googleblog.blogspot.com/2006/09/google-calendar-does-something-about.html).


# Details #


Essentially, what you need to do is to create an atom:link, with a gcal:webContent
child element of the following form:

```
<atom:link rel="http://schemas.google.com/gCal/2005/webContent"
           title="Your Title"
           href="a link to an icon for the top of the day">
           type="text/html or image/*"
  <gCal:webContent url="a link to the content"
                   width="pixels"
                   height="pixels" />
</atom:link>
```
If type is text/html, the content will be displayed in an IFrame, if it's image/**, an IMG
element will be used.**

There is samplecode in cs/src/unittests/caltest.cs, here is the relevant part to show you how easy it is to set this up:

```
                    EventEntry entry = ...

                    AtomLink link = new AtomLink("image/gif", "http://schemas.google.com/gCal/2005/webContent");
                    link.Title = "Test content";
                    link.HRef = "http://www.google.com/calendar/images/google-holiday.gif";
                    WebContent content = new WebContent();
                    content.Url = "http://www.google.com/logos/july4th06.gif";
                    content.Width = 270;
                    content.Height = 130;
                    link.ExtensionElements.Add(content);
                    entry.Links.Add(link);

                    entry.Update(..

```


That's it. Play with it and enjoy the visual effects you can create for your calendar that way.

