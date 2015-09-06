# Introduction #

We will be trying to keep a list of things we currently work on for the .NET client SDK here, and we wil try to update this on a regular basis. Not all items are going to be part of the "next" release, but where this is the case (like a long term goal), we will try to indicate this as such.


# Details #

### Bug fixes ###

There are no high priority bugs right now, so those are worked on in the background.

### Contacts v2. API support ###

Contacts will be, most likely, the next major release. It will get the same kind of high level support as the YouTube library just got.


## Longer Term Items ##

### LINQ support ###

One of the more natural things to do is to make sure that we can use LINQ against our Google Data API properties. What would be more natural then to use query statements in your code to get just the subset of calendar entries that you want to work with. Now we are in the preliminary investigation phase here, and feedback on what you want to do with this is highly appreciated.

With the 1.3.1.0 release we made some progress in that area, but there is still work to be done. We are working on a direct LINQ provider and delayed evaluation to make LINQ queries more optimal.

This item is not sure for the next release.

### Astoria support ###

With Microsoft moving to support AtomPub in it's database layer, it would be nice to be able to use that database layer to talk against our Google Data API enabled properties. Again there is work to be done in the investigation phase, but we think this also has a bundle of nice use cases, and again feedback appreciated.

This item is not sure for the next release.