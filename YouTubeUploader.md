# Introduction #

The YouTube Content Uploader is a sample application introducing you to the ResumableUploader class, and how to use it to asynchronously upload several large files with the ability to resume the upload from failure.

# Details #

The YouTube Content Uploader is a sample app showcasing the Resumable Upload functionality in the API. In the app, you can open a .CSV file containing metadata of the videos you want to upload, and then let the tool upload all these files in the background.

### CSV File gotchas ###

Note the CSV files should look like this:

```
Title,Description,Tags,Category,Private,Path
Title One,The first test video,"water",Education,TRUE,c:\videos\file2one.avi
Title Two,The second test video,"humor",Entertainment,TRUE,c:\videos\filetwo.avi
```
The columns are most self-explanatory. The Private column takes the value TRUE or FALSE. Set the column value to TRUE if you want the uploaded video to remain private.

Some spreadsheet programs might export a spreadsheet with blank column headers, like this:

```
Title,Description,Tags,Category,Private,Path,,,,,,,,,,,,,
```

The code used to parse CSVs in the YouTube Uploader will choke on this, so make sure your CSV file is done with the correct settings to create only the columns that are used.

After the upload completes, you can export a .CSV file containing the status of all the videos you've tried to upload. You can edit the .CSV file and re-open it in the app to reupload the videos that were not successfully uploaded the first time.

### Using a different developer key ###
When you compile the sample, or use the provided binary, it will use a default YouTube developer key. The default key will probably be too limited for your use case. You should replace the developer key with your own using either one of the following methods:

1. Download the source code and replace the developer key with your own, recompile, and you are good to go.

2. Use the config file to provide your own key.

### Configuration file parameters ###
.NET config files are a standard way in the .NET runtime to augment applications and provide configuration information.

To use such a file, you need to find the .EXE file you want to modify, and create a .config file in the same directory.

So for the YouTubeUploader.exe, you would create a file called YouTubeUploader.exe.config.

This is a normal text file, and it should look like this:

```
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <appSettings>
    <add key="UserName" value="username" />
    <add key="PassWord" value="password" />
    <add key="YoutubeAccount" value="accountname" />
    <add key="MaxThreads" value="2" />
    <add key="ChunkSize" value="2" />
    <add key="RetryCount" value="4" />
    <add key="CsvFile" value="pathandfilename" />
    <add key="OutputFile" value="pathandfilename" />
    <add key="DevKey" value="yourdevkey" />
  </appSettings>
</configuration>
```

You have a standard key=value section, where you need to replace the sample values above with your actual settings.

Note that all the values are optional. So it's perfectly fine to have a file that looks like this, if all you want to change is the default for the retry value:

```
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <appSettings>
    <add key="RetryCount" value="20" />
  </appSettings>
</configuration>
```

Here is an explanation of all the parameters:

**UserName** -> your google account username

**PassWord** -> the password for that account

**YoutubeAccount** -> the youtube accountname

If you provide the above 3 values, the application will, on startup, skip the login dialog and use the provided values instead.

**MaxThreads** -> this value determines how many uploads will happen at the same time.  You can try to tune this to maximize the upload bandwidth used.

**ChunkSize**  -> this value determines how many Megabytes of the files will be uploaded  at once. Let's say you have a 1GB video and this value is 4, the  application will upload 250 parts of the video before it's done. Again,  you can try to finetune this depending on bandwidth available.

**RetryCount** -> if an upload fails due to network or other conditions, it will be retried. The application will stop trying to upload a file once its retry  counter reaches this number. Note that, once ANY upload succeeds, all retry              counters will be reset to 0 and the remaining files will be tried again.

**CsvFile**    -> the filename of a CSV file to open on startup

**OutputFile** -> the filename of an output CSV file where the application should save the results of the upload to

**DevKey**     -> your YouTube developer key that you like to use for uploading to your account.

### Command line arguments ###

There is one additional parameter, that can be used on the commandline. If you start YouTubeUploader with the /autostart parameter, it will start uploading and save the output automatically once it's done.