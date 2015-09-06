# Introduction #

When you are behind a firewall or a company proxy server that you need to use,
you need to tell your code about it, otherwise requests will fail. In .NET, any HTTP connection object can have this set as a property.

In the GData libraries you have, by default, only indirect access to this, but it's
still easy to accomplish.

# Details #
```

           CalendarService service = new CalendarService("CalendarSampleApp");
           query.Uri = new Uri(calendarURI);
           GDataRequestFactory f = (GDataRequestFactory) service.RequestFactory;
           IWebProxy iProxy = WebRequest.DefaultWebProxy;
           WebProxy myProxy = new WebProxy(iProxy.GetProxy(query.Uri));
           // potentially, setup credentials on the proxy here
           myProxy.Credentials = CredentialsCache.DefaultCredentials;
           myProxy.UseDefaultCredentials = true;
           f.Proxy = myProxy;
```

And that's all.

An alternative method is to use the app.config file, and don't modify your code at all.
```
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
 <system.net>
  <defaultProxy>
   <proxy usesystemdefault="true"/>
  </defaultProxy>
 </system.net>
</configuration>
```

The above snippet, when placed in your config file for your .NET application will make all outgoing HTTP connections use the default proxy. There is a lot more information on MSDN about the config files and how to specify proxy settings.