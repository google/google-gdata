/* Copyright (c) 2006 Google Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
*/
#define TRACE
#define USE_TRACING

using System;
using System.IO;
using System.Diagnostics;


//////////////////////////////////////////////////////////////////////
// all internal tracing uses those helpers. This makes it easy
// to change the tracing code to use log4Net or NLog or other libraries
//////////////////////////////////////////////////////////////////////
namespace Google.GData.Client
{
    //////////////////////////////////////////////////////////////////////
    /// <summary>Tracing helper class. Uses conditional compilation to 
    ///  exclude tracing code in release builds</summary> 
    //////////////////////////////////////////////////////////////////////
    public sealed class Tracing
    {
        //////////////////////////////////////////////////////////////////////
        /// <summary>default constructor does nothing</summary> 
        //////////////////////////////////////////////////////////////////////
        private Tracing()
        {
            
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>default initializer, does nothing right now</summary> 
        //////////////////////////////////////////////////////////////////////
        [Conditional("USE_TRACING")] static public void InitTracing()
        {
            return;
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>Default deinitializer, closes the listener streams</summary> 
        //////////////////////////////////////////////////////////////////////
        [Conditional("USE_TRACING")] static public void ExitTracing()
        {
            return;
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>Method to trace the current call with an additional message</summary> 
        /// <param name="msg"> msg string to display</param>
        //////////////////////////////////////////////////////////////////////
        [Conditional("USE_TRACING")] static public void TraceCall(string msg)
        {
            // puts out the callstack and the msg
            try
            {
                StackTrace trace = new StackTrace();
                if (trace != null && trace.GetFrame(0) != null)
                {
                    System.Reflection.MethodBase method = trace.GetFrame(0).GetMethod(); 
                    Trace.WriteLine(method.Name + ": " + msg); 
                }
                else
                {
                    Trace.WriteLine("Method Unknown: " + msg);
                }
                 Trace.Flush();


            } catch 
            {
                Tracing.TraceMsg(msg);
            }
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Method to trace the current call with an additional message</summary> 
        //////////////////////////////////////////////////////////////////////
        [Conditional("USE_TRACING")] static public void TraceCall()
        {
            Tracing.TraceCall("");
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Method to trace the current call with an additional message</summary> 
        /// <param name="msg"> msg string to display</param>
        //////////////////////////////////////////////////////////////////////
        [Conditional("USE_TRACING")] static public void TraceInfo(string msg)
        {
            Tracing.TraceMsg(msg);
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Method to trace the a message with timestamping</summary> 
        /// <param name="msg"> msg string to display</param>
        //////////////////////////////////////////////////////////////////////
        [Conditional("USE_TRACING")] static public void Timestamp(string msg)
        {
            DateTime now = DateTime.Now;
            msg = now.ToString("HH:mm:ss:ffff") + " - " + msg; 
            Tracing.TraceMsg(msg);
        }


        //////////////////////////////////////////////////////////////////////
        /// <summary>Method to trace a message</summary> 
        /// <param name="msg"> msg string to display</param>
        //////////////////////////////////////////////////////////////////////
        [Conditional("USE_TRACING")] static public void TraceMsg(string msg)
        {
            try
            {
                Trace.WriteLine(msg); 
                Trace.Flush();
            } catch 
            {
            }
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Method to assert + trace a message</summary> 
        /// <param name="condition"> if false, raises assert</param>
        /// <param name="msg"> msg string to display</param>
        //////////////////////////////////////////////////////////////////////
        [Conditional("USE_TRACING")] static public void Assert(bool condition, string msg)
        {
            if (condition == false)
            {
                Trace.WriteLine(msg); 
                Trace.Assert(condition, msg);
            }
        }

    }
    /////////////////////////////////////////////////////////////////////////////

}
/////////////////////////////////////////////////////////////////////////////
