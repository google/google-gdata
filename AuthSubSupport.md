# Introduction #

Some patches went in a couple weeks ago into SVN that fixed some issues
in GAuthSubRequestFactory and AuthSubUtil to enable use of AuthSub with
the .NET client library.  These fixes exist only in the SVN source and
are not distributed as binaries.


# Details #


I thought I'd give a very quick overview of how to use AuthSub in .NET
for those of you who are developing web applications using the .NET
client library.  Please note that you should always use AuthSub instead
of ClientLogin when developing web applications that handle other
users' credentials.

Please read over the AuthSub documentation if you're going to be using
it to authenticate users.  The guide is at:
http://code.google.com/apis/accounts/AuthForWebApps.html

Also, note that there are two types of AuthSub tokens - single-use and
session tokens.  Only single use tokens are issued by Google's
AuthSubRequest service, but if you specified 'session=1' in the URL to
AuthSubRequest, the token received can be exchanged for a session token
using the AuthSubSessionToken service.  Google Calendars can not
currently be accessed using single-use AuthSub tokens.

The typical AuthSub process goes like this:

  1. User visits your site for the first time.  They do not have an AuthSub session token with your application (usually stored as a Session variable).  They also do not have a single-use 'token' value in  the Request.QueryString
  1. Ask your user to click on a link on your site which redirects them to Google's AuthSubRequest service.
  1. The user authenticates with Google (if they haven't already), and approves your request to access their calendar by clicking on a form button
  1. The user is redirected back to your application with a query parameter called 'token' in the URL.  This is a single-use AuthSub token.
  1. Call Google's AuthSubSessionToken method to exchange the single-use token for an AuthSub session token.  Store this value in the user's session on the app server or another appropriate place.
  1. Perform the appropriate actions on the user's calendar using the session token retrieved from the AuthSubSessionToken request

For future requests, there's a decision tree to follow to determine
whether you already have a session token, etc.  I'll leave this part up
to you unless there are any questions on the matter.

So, how is this accomplished in .NET?  There are some static functions
in AuthSubUtil to do a lot of the work for you:

  * getRequestUri generates the URL to which you need to redirect the
user in step #2
  * exchangeForSessionToken exchanges the single-use token for a
session token in step #5

In order to accomplish the last step (actually calling the Calendar
data API), you need to set the RequestFactory property of the
CalendarService to be a GAuthSubRequestFactory instead of the normal
GAuthRequestFactory which is used for ClientLogin.

Here's a snippet example of using the authSubSessToken to access a
calendar:

```

GAuthSubRequestFactory authFactory = new GAuthSubRequestFactory("cl","SampleApp");
authFactory.Token = authSubSessToken;
CalendarService service = new CalendarService(authFactory.ApplicationName);
service.RequestFactory = authFactory;
EventQuery query = new EventQuery();
query.Uri = new Uri(calUri);
query.SortOrder = CalendarSortOrder.ascending;
EventFeed calFeed = service.Query(query)

```

**Note, that with version 1.0.9.0 and higher, the support for this is now part of the released binaries.**

Hope this helps!  Let me know if you have any questions.

Have a great week!

Cheers,
-Ryan
