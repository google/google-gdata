using Google.GData.GoogleBase;
using System;

namespace Google.GData.GoogleBase.Examples {

    //////////////////////////////////////////////////////////////////////
    /// <summary>Base class for the google base examples.
    ///
    /// This class takes gets the developer key and other parameters
    /// from the command line arguments and creates a
    /// <see cref="GBaseService">service object</see> and
    /// a <see cref="GBaseUriFactory">uri factory</see> for the
    /// subclasses, the real examples.</summary>
    //////////////////////////////////////////////////////////////////////
    public abstract class Example
    {
        /// <summary>URI factory, modified using --url.</summary>
        protected GBaseUriFactory uriFactory = GBaseUriFactory.Default;
        /// <summary>Service, initialized and authentified.</summary>
        protected GBaseService service;

        private string developerKey;

        //////////////////////////////////////////////////////////////////////
        /// <summary>Parses command-line options</summary>
        /// <param name="args">command-line arguments</summary>
        /// <param name="applicationName">name of the current application
        /// </param>
        /// <returns>the rest of the command-line argument (the first
        /// argument that's not an option</returns>
        //////////////////////////////////////////////////////////////////////
        public string[] Init(string[] args, string applicationName)
        {
            return Init(args, 0, applicationName);
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Parses command-line options</summary>
        /// <param name="args">command-line arguments</summary>
        /// <param name="argsIndex">the first argument to check in
        /// args</param>
        /// <param name="applicationName">name of the current application
        /// </param>
        /// <returns>the rest of the command-line argument (the first
        /// argument that's not an option</returns>
        //////////////////////////////////////////////////////////////////////
        public string[] Init(string[] args,
                             int argsIndex,
                             string applicationName)
        {
            while (argsIndex < args.Length && args[argsIndex].StartsWith("-"))
            {
                string arg = args[argsIndex];
                argsIndex++;
                if ( argsIndex >= args.Length)
                {
                    FatalError("Expected a parameter value " + "after " + arg);
                }
                string value = args[argsIndex];
                argsIndex++;
                if (!ParseArg(arg, value))
                {
                    FatalError("Unknown parameter: " + arg);
                }
            }

            // service.query does a GET on the url above and parses the result,
            // which is an ATOM feed with some extensions (called the Google Base
            // data API items feed).
            service = new GBaseService(applicationName, developerKey);

            // Return the rest of the arguments
            if (argsIndex > 0)
            {
                string[] newargs = new string[args.Length - argsIndex];
                System.Array.Copy(args, argsIndex, newargs, 0, newargs.Length);
                return newargs;
            }
            return args;
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Parses options</summary>
        /// <param name="argName">option name</param>
        /// <param name="argValue">option value</param>
        /// <returns>true if the option was understood and parsed, false
        /// if the option was not understood.</returns>
        //////////////////////////////////////////////////////////////////////
        protected virtual bool ParseArg(string argName, string argValue)
        {
            switch (argName)
            {
            case "--url":
                uriFactory = new GBaseUriFactory(new Uri(new Uri(argValue), "/base/"));
                return true;

            case "--key":
                developerKey = argValue;
                return true;
            }
            return false;
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Displays an error to stderr and quits.</summary>
        //////////////////////////////////////////////////////////////////////
        protected static void FatalError(string message)
        {
            System.Console.Error.WriteLine(message);
            System.Environment.Exit(1);
        }

    }

}
