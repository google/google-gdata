# Introduction #

Part of the .NET release is a handy utility, called ExecRequest. It allows you to query, update, delete and insert items into a feed, using programmatic login or web application login. ExecRequest, including Web Application Login, is part of the .NET library with version 1.1.0.0.

# Details #

ExecRequest.exe is located in the same directory where the other sample applications and libraries are, normally in the clients/cs/lib/release directory.

If you start ExecRequest with no parameters it will give you a short help description on how it should get started. Let's look at this in more detail.

Programmatic login

usage: ExecRequest 

&lt;service&gt;

 

&lt;cmd&gt;

 

&lt;uri&gt;

 

&lt;username&gt;

 

&lt;password&gt;

, where cmd is QUERY, UPDATE, INSERT, DELETE

Example:

ExecRequest cl QUERY http://www.google.com/calendar/feeds/private/default joe@gmail.com mypassword

This would query the default feed for user Joe, given his password and output the resulting XML to the console.

ExecRequest cl INSERT http://www.google.com/calendar/feeds/private/default joe@gmail.com mypassword < myentry.xml

This would try to insert the XML in file myentry.xml into the default feed for user Joe.

Web application login

ExecRequest 

&lt;service&gt;

 

&lt;cmd&gt;

 

&lt;uri&gt;

 /a 

&lt;sessiontoken&gt;



This form, similiar to programmatic login, would use an already existing session token for Web Application login instead of username and password.


ExecRequest 

&lt;service&gt;

 

&lt;cmd&gt;

 

&lt;uri&gt;

 /e 

&lt;onetimetoken&gt;



This form, would use a one time token, exchange it for a session token (while echoing that back to the screen for savekeeping) and use that session token for Web Application login instead of username and password.

