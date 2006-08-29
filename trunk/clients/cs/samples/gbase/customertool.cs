using Google.GData.GoogleBase;
using System;
using System.Net;
using System.IO;
using Google.GData.Client;

namespace Google.GData.GoogleBase.Examples {

    //////////////////////////////////////////////////////////////////////
    /// <summary>Query/Add/Remove items from the items feed.</summary>
    //////////////////////////////////////////////////////////////////////
    public class CustomerTool : Example
    {
        private string user;
        private string password;
        private Uri authUri = null;

        public static void Main(string[] args)
        {
            CustomerTool customerTool = new CustomerTool();
            customerTool.Execute(args);
        }

        private void Execute(string[] args)
        {
            if (args.Length == 0)
            {
                FatalError("Usage: customertool.exe <command> --key <key> " +
                           "--user <user> --password <passwerd> [options] " +
                           "<command parameters>");
            }

            string commandName = args[0];
            args = Init(args, 1, "Google-CsharpCustomerTool-1.0");
            try
            {
                CreateCommand(commandName, args).Execute();
            }
            catch (GDataRequestException e)
            {
                Console.Error.WriteLine("Error: " + e.Message);
                PrintErrorResponse(e.Response);
            }
            Console.WriteLine("");
        }

        /// <summary>Print the error message as it came from the server.</summary>
        private void PrintErrorResponse(WebResponse response)
        {
            if (response == null)
            {
                return;
            }

            Stream responseStream = response.GetResponseStream();
            try
            {
                Stream errorStream = Console.OpenStandardError();
                try
                {
                    Copy(responseStream, errorStream);
                }
                finally
                {
                    errorStream.Close();
                }
            }
            finally
            {
                responseStream.Close();
            }
        }

        /// <summary>Copy data from one stream to another</summary>
        private static void Copy(Stream instream, Stream outstream)
        {
            byte[] buffer = new byte[1024];
            int l;

            while ( (l = instream.Read(buffer, 0, buffer.Length)) > 0)
            {
                outstream.Write(buffer, 0, l);
            }
        }

        /// <summary>Parses the extra arguments supported by this tool</summary>
        protected override bool ParseArg(string argName, string argValue)
        {
            switch (argName)
            {
            case "--user":
                user = argValue;
                return true;

            case "--password":
                password = argValue;
                return true;

            case "--auth":
                authUri = new Uri(new Uri(argValue), "/accounts/ClientLogin");
                return true;

            default:
                return base.ParseArg(argName, argValue);
            }
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Creates the command to be executed</summary>
        /// <param name="name">command name</param>
        /// <param name="args">command arguments</param>
        /// <returns>a command to execute or null</returns>
        //////////////////////////////////////////////////////////////////////
        private ICommand CreateCommand(string name, string[] args)
        {
            if (user == null || password == null)
            {
                FatalError("Missing parameters: --user, --password");
            }
            service.setUserCredentials(user, password, authUri);

            switch(name)
            {
            case "query":
                return new QueryCommand(service,
                                        uriFactory,
                                        ExpectAtMostOneArgument(args));

            case "delete":
                return new DeleteCommand(service,
                                         uriFactory,
                                         ExpectOneArgument(args));

            case "insert":
                ExpectNoArguments(args);
                return new InsertCommand(service, uriFactory);

            case "update":
                ExpectNoArguments(args);
                return new UpdateCommand(service, uriFactory);

            case "get":
                return new GetCommand(service,
                                      uriFactory,
                                      ExpectOneArgument(args));

            case "batch":
                ExpectNoArguments(args);
                return new BatchCommand(service,
                                        uriFactory);

            default:
                FatalError("Unknown command : " + name + ". " +
                           "Available commands: query, get, insert, update, delete, batch");
                break;
            }
            return null;
        }

        private static void ExpectNoArguments(String[] args)
        {
            if (args.Length > 0)
            {
                FatalError("Unexpected argument: " + args[0]);
            }
        }

        private static String ExpectAtMostOneArgument(String[] args)
        {
            if (args.Length > 1)
            {
                FatalError("Command expected at least one argument, got " + args.Length);
            }
            else if (args.Length == 1)
            {
                return args[0];
            }
            return null;
        }

        private static String ExpectOneArgument(String[] args)
        {
            if (args.Length != 1)
            {
                FatalError("Command expected exactly one argument, got " + args.Length);
            }
            return args[0];
        }

    }

}
