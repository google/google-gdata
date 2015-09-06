# Introduction #


It was brought to my attention that this problem still manifests itself once in a while, especially in ASP.NET apps. So I researched it some more and came across a few articles explaining this (read: http://www.simple-talk.com/community/blogs/dana/archive/2006/07/28/1484.aspx for an explanation). I leave it to the audience to decide if that behavior change in the runtime was a good thing or not...

# Details #

A simple selfrefreshing ASP page doing a calendar query would not create that problem for me, if i set the refresh time to 0.5 sec, or 2 sec. I ran this for an hour, and got no failures. But when the refresh time was extended to 15 sec, failures started to happen.

Now what is a one to do about this. I introduced a KeepAlive property on the RequestFactory object, which is set to TRUE by default. Reasoning for this is that this is the default behavior, and it works perfect in .NET 1.1.

If you are running into this issue, you need to set:

((GDataRequestFactory)yourService.RequestFactory).KeepAlive = false;

before you start making calls.

This seems to have fixed the issue for me in an overnight test. Note, that setting KeepAlive to false will, in high volume/high frequence settings slow down your performance. In that case, you would need to leave the setting alone, and catch the exception and retry.

Note that this a common issue for .NET 2.0 developers, and we can only hope that a service pack will restore the .NET 1.1 behavior.

You can get new binaries (1.0.9.1) with that behavior by syncing to the subversion repository.