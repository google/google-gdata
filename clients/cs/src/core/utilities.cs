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
#region Using directives

#define USE_TRACING

using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

#endregion

namespace Google.GData.Client
{


    //////////////////////////////////////////////////////////////////////
    /// <summary>a little helper that decodes encoded string entities
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    internal class DecodingTextReader : XmlTextReader
    {

        /// <summary>constructor, calls base</summary> 
        internal DecodingTextReader(Stream streamInput, NameTable nametable) : base(streamInput, nametable)
        {
        }

        /// <summary>value uses the HtmlDecode helper</summary> 
        public override string Value
        {
            get 
            {
                string content = base.Value;
                return System.Web.HttpUtility.HtmlDecode(content);
            }
        }


        /// <summary>uses the HTML decoder helper</summary> 
        public override string ReadString()
        {
            string content = base.ReadString();
            return System.Web.HttpUtility.HtmlDecode(content);
        }
    }
    
    //////////////////////////////////////////////////////////////////////
    /// <summary>String utilities
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    public sealed class Utilities
    {
        //////////////////////////////////////////////////////////////////////
        /// <summary>private constructor to prevent the compiler from generating a default one</summary> 
        //////////////////////////////////////////////////////////////////////
        private Utilities()
        {
        }
        /////////////////////////////////////////////////////////////////////////////
        /// <summary>Little helper that checks if a string is XML persistable</summary> 
        public static bool IsPersistable(string s)
        {
            if (s != null && s.Length != 0 && s.Trim().Length != 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>Little helper that checks if a string is XML persistable</summary> 
        public static bool IsPersistable(AtomUri uriString)
        {
            return uriString == null ? false : Utilities.IsPersistable(uriString.ToString());
        }

        /// <summary>Little helper that checks if an int is XML persistable</summary> 
        public static bool IsPersistable(int iNum)
        {
            return iNum == 0 ? false : true;
        }

        /// <summary>Little helper that checks if a datevalue is XML persistable</summary> 
        public static bool IsPersistable(DateTime testDate)
        {
            return testDate == Utilities.EmptyDate ? false : true;
        }



        //////////////////////////////////////////////////////////////////////
        /// <summary>helper to read in a string and Encode it</summary> 
        /// <param name="content">the xmlreader string</param>
        /// <returns>UTF8 encoded string</returns>
        //////////////////////////////////////////////////////////////////////
        public static string EncodeString(string content)
        {
            // get the encoding
            Encoding utf8Encoder = Encoding.UTF8; 
            Encoding utf16Encoder = Encoding.Unicode;

            Byte [] bytes = utf16Encoder.GetBytes(content); 

            Byte [] utf8Bytes = Encoding.Convert(utf16Encoder, utf8Encoder, bytes); 

            char[] utf8Chars = new char[utf8Encoder.GetCharCount(utf8Bytes, 0, utf8Bytes.Length)];
            utf8Encoder.GetChars(utf8Bytes, 0, utf8Bytes.Length, utf8Chars, 0);
      
            String utf8String = new String(utf8Chars); 

            return utf8String; 
        
        }



        //////////////////////////////////////////////////////////////////////
        /// <summary>helper to read in a string and Encode it</summary> 
        /// <param name="content">the xmlreader string</param>
        /// <returns>html encoded string</returns>
        //////////////////////////////////////////////////////////////////////
        public static string EncodeHtmlString(string content)
        {
            // get the encoding
            return content; 

        }



        //////////////////////////////////////////////////////////////////////
        /// <summary>helper to read in a string and replace the reserved URI 
        /// characters with hex encoding</summary> 
        /// <param name="content">the parameter string</param>
        /// <returns>hex encoded string</returns>
        //////////////////////////////////////////////////////////////////////
        public static string UriEncodeReserved(string content) 
        {
            StringBuilder returnString = new StringBuilder(256);

            foreach (char ch in content) 
            {
                if (ch == ';' || 
                    ch == '/' ||
                    ch == '?' ||
                    ch == ':' ||
                    ch == '@' ||
                    ch == '&' ||
                    ch == '=' ||
                    ch == '+' ||
                    ch == '$' ||
                    ch == ',' )
                {
                    returnString.Append(Uri.HexEscape(ch));
                }
                else 
                {
                    returnString.Append(ch);
                }
            }

            return returnString.ToString(); 
            
        }

       

        //////////////////////////////////////////////////////////////////////
        /// <summary>Method to output just the date portion as string</summary>
        /// <param name="dateTime">the DateTime object to output as a string</param>
        /// <returns>an rfc-3339 string</returns>
        //////////////////////////////////////////////////////////////////////
        public static string LocalDateInUTC(DateTime dateTime)
        {
            // Add "full-date T partial-time"
            return dateTime.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }


        //////////////////////////////////////////////////////////////////////
        /// <summary>Method to output DateTime as string</summary>
        /// <param name="dateTime">the DateTime object to output as a string</param>
        /// <returns>an rfc-3339 string</returns>
        //////////////////////////////////////////////////////////////////////
        public static string LocalDateTimeInUTC(DateTime dateTime)
        {
            TimeSpan diffFromUtc = TimeZone.CurrentTimeZone.GetUtcOffset(dateTime);

            // Add "full-date T partial-time"
            string strOutput = dateTime.ToString("s", CultureInfo.InvariantCulture);

            // Add "time-offset"
            return strOutput + FormatTimeOffset(diffFromUtc);
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Helper method to format a TimeSpan as a string compliant with the "time-offset" format defined in RFC-3339</summary>
        /// <param name="spanFromUtc">the TimeSpan to format</param>
        /// <returns></returns>
        //////////////////////////////////////////////////////////////////////
        public static string FormatTimeOffset(TimeSpan spanFromUtc)
        {
            // Simply return "Z" if there is no offset
            if (spanFromUtc == TimeSpan.Zero)
                return "Z";

            // Return the numeric offset
            TimeSpan absoluteSpan = spanFromUtc.Duration();
            if (spanFromUtc > TimeSpan.Zero)
            {
                return "+" + FormatNumOffset(absoluteSpan);
            }
            else
            {
                return "-" + FormatNumOffset(absoluteSpan);
            }
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Helper method to format a TimeSpan to {HH}:{MM}</summary>
        /// <param name="timeSpan">the TimeSpan to format</param>
        /// <returns>a string in "hh:mm" format.</returns>
        //////////////////////////////////////////////////////////////////////
        internal static string FormatNumOffset(TimeSpan timeSpan)
        {
            return String.Format("{0:00}:{1:00}", timeSpan.Hours, timeSpan.Minutes);
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>public static string CalculateUri(string base, string inheritedBase, string local)</summary> 
        /// <param name="localBase">the baseUri from xml:base </param>
        /// <param name="inheritedBase">the pushed down baseUri from an outer element</param>
        /// <param name="localUri">the Uri value</param>
        /// <returns>the absolute Uri to use... </returns>
        //////////////////////////////////////////////////////////////////////
        internal static string CalculateUri(AtomUri localBase, AtomUri inheritedBase, string localUri)
        {
            try 
            {
                Uri uriBase = null;
                Uri uriSuperBase= null;
                Uri uriComplete = null;
    
                if (inheritedBase != null)
                {
                    uriSuperBase = new Uri(inheritedBase.ToString()); 
                }
                if (localBase != null)
                {
                    if (uriSuperBase != null)
                    {
                        uriBase = new Uri(uriSuperBase, localBase.ToString());
                    }
                    else
                    {
                        uriBase = new Uri(localBase.ToString());
                    }
                }
                else
                {
                    // if no local xml:base, take the passed down one
                    uriBase = uriSuperBase;
                }
                if (localUri != null)
                {
                    if (uriBase != null)
                    {
                        uriComplete = new Uri(uriBase, localUri.ToString());
                    }
                    else
                    {
                        uriComplete = new Uri(localUri.ToString());
                    }
                }
                else 
                {
                    uriComplete = uriBase;
                }
    
                return uriComplete != null ? uriComplete.AbsoluteUri : null;
            }
            catch (System.UriFormatException)
            {
                return "Unsupported URI format"; 
            }

        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>Sets the Atom namespace, if it's not already set.
        /// </summary> 
        /// <param name="writer"> the xmlwriter to use</param>
        /// <returns> the namespace prefix</returns>
        //////////////////////////////////////////////////////////////////////
        static public string EnsureAtomNamespace(XmlWriter writer)
        {
            Tracing.Assert(writer != null, "writer should not be null");
            if (writer == null)
            {
                throw new ArgumentNullException("writer"); 
            }
            string strPrefix = writer.LookupPrefix(BaseNameTable.NSAtom);
            if (strPrefix == null)
            {
                writer.WriteAttributeString("xmlns", null, BaseNameTable.NSAtom);
            }
            return strPrefix;
           
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>Sets the gData namespace, if it's not already set.
        /// </summary> 
        /// <param name="writer"> the xmlwriter to use</param>
        /// <returns> the namespace prefix</returns>
        //////////////////////////////////////////////////////////////////////
        static public string EnsureGDataNamespace(XmlWriter writer)
        {
            Tracing.Assert(writer != null, "writer should not be null");
            if (writer == null)
            {
                throw new ArgumentNullException("writer"); 
            }
            string strPrefix = writer.LookupPrefix(BaseNameTable.gNamespace);
            if (strPrefix == null)
            {
                writer.WriteAttributeString("xmlns", BaseNameTable.gDataPrefix, null, BaseNameTable.gNamespace);
            }
            return strPrefix;
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>Sets the gDataBatch namespace, if it's not already set.
        /// </summary> 
        /// <param name="writer"> the xmlwriter to use</param>
        /// <returns> the namespace prefix</returns>
        //////////////////////////////////////////////////////////////////////
        static public string EnsureGDataBatchNamespace(XmlWriter writer)
        {
            Tracing.Assert(writer != null, "writer should not be null");
            if (writer == null)
            {
                throw new ArgumentNullException("writer"); 
            }
            string strPrefix = writer.LookupPrefix(BaseNameTable.gBatchNamespace);
            if (strPrefix == null)
            {
                writer.WriteAttributeString("xmlns", BaseNameTable.gBatchPrefix, null, BaseNameTable.gBatchNamespace);
            }
            return strPrefix;
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>searches tokenCollection for a specific NEXT value. 
        ///  The collection is assume to be a key/value pair list, so if A,B,C,D is the list
        ///   A and C are keys, B and  D are values
        /// </summary> 
        /// <param name="tokens">the TokenCollection to search</param>
        /// <param name="key">the key to search for</param>
        /// <returns> the value string</returns>
        //////////////////////////////////////////////////////////////////////
        static public string FindToken(TokenCollection tokens,  string key)
        {
            string returnValue = null; 
            bool fNextOne=false; 

            foreach (string token in tokens )
            {
                if (fNextOne == true)
                {
                    returnValue = token; 
                    break;
                }
                if (key == token)
                {
                    // next one is it
                    fNextOne = true; 
                }
            }

            return returnValue; 
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>converts a form response stream to a TokenCollection,
        ///  by parsing the contents of the stream for newlines and equal signs
        ///  the input stream is assumed to be an ascii encoded form resonse
        /// </summary> 
        ///  <param name="inputStream">the stream to read and parse</param>
        /// <returns> the resulting TokenCollection </returns>
        //////////////////////////////////////////////////////////////////////
        static public TokenCollection ParseStreamInTokenCollection(Stream inputStream)
        {
             // get the body and parse it
            ASCIIEncoding encoder = new ASCIIEncoding();
            StreamReader readStream = new StreamReader(inputStream, encoder);
            String body = readStream.ReadToEnd(); 
            readStream.Close(); 
            Tracing.TraceMsg("got the following body back: " + body);
            // all we are interested is the token, so we break the string in parts
            TokenCollection tokens = new TokenCollection(body, '=', true, 2); 
            return tokens;
         }
        /////////////////////////////////////////////////////////////////////////////

          //////////////////////////////////////////////////////////////////////
        /// <summary>parses a form response stream in token form for a specific value
        /// </summary> 
        /// <param name="inputStream">the stream to read and parse</param>
        /// <param name="key">the key to search for</param>
        /// <returns> the string in the tokenized stream </returns>
        //////////////////////////////////////////////////////////////////////
        static public string ParseValueFormStream(Stream inputStream,  string key)
        {
            TokenCollection tokens = ParseStreamInTokenCollection(inputStream);
            return FindToken(tokens, key);
         }
        /////////////////////////////////////////////////////////////////////////////



        //////////////////////////////////////////////////////////////////////
        /// <summary>returns a blank emptyDate. That's the default for an empty string date</summary> 
        //////////////////////////////////////////////////////////////////////
        public static DateTime EmptyDate
        {
            get {
                // that's the blank value you get when setting a DateTime to an empty string inthe property browswer
                return new DateTime(1,1,1);
            }

        }
        /////////////////////////////////////////////////////////////////////////////
        
    }
    /////////////////////////////////////////////////////////////////////////////

    //////////////////////////////////////////////////////////////////////
    /// <summary>standard string tokenizer class. Pretty much cut/copy/paste out of 
    /// MSDN. 
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    public class TokenCollection : IEnumerable
    {
       private string[] elements;
  
       /// <summary>Constructor, takes a string and a delimiter set</summary> 
       public TokenCollection(string source, char[] delimiters)
       {

           if (source != null)
           {
               this.elements = source.Split(delimiters);
           }
       }

         /// <summary>Constructor, takes a string and a delimiter set</summary> 
       public TokenCollection(string source, char delimiter, 
                              bool seperateLines, int resultsPerLine)
       {
           if (source != null)
           {
               if (seperateLines == true)
               {
                   // first split the source into a line array
                   string [] lines = source.Split(new char[] {'\n'});
                   int size = lines.Length * resultsPerLine;
                   this.elements = new string[size]; 
                   size = 0; 
                   foreach (String s in lines)
                   {
                       // do not use Split(char,int) as that one
                       // does not exist on .NET CF
                       string []temp = s.Split(delimiter);
                       int counter = temp.Length < resultsPerLine ? temp.Length : resultsPerLine;
                    
                       for (int i = 0; i <counter; i++) 
                       {
                           this.elements[size++] = temp[i];
                       }
                       for (int i = resultsPerLine; i < temp.Length; i++) 
                       {
                           this.elements[size-1] += delimiter + temp[i];
                       }
             
                   }
               } 
               else 
               {
                   string[] temp = source.Split(delimiter);
                   resultsPerLine = temp.Length < resultsPerLine ? temp.Length : resultsPerLine;
                   this.elements = new string[resultsPerLine];

                   for (int i = 0; i <resultsPerLine; i++) 
                   {
                       this.elements[i] = temp[i];
                   }
                   for (int i = resultsPerLine; i < temp.Length; i++) 
                   {
                       this.elements[resultsPerLine-1] += delimiter + temp[i];
                   }
             
             
               } 
           }
       }

       /// <summary>IEnumerable Interface Implementation, for the noninterface</summary> 
       public TokenEnumerator GetEnumerator() // non-IEnumerable version
       {
          return new TokenEnumerator(this);
       }
       /// <summary>IEnumerable Interface Implementation</summary> 
       IEnumerator IEnumerable.GetEnumerator() 
       {
          return (IEnumerator) new TokenEnumerator(this);
       }
    
       /// <summary>Inner class implements IEnumerator interface</summary> 
       public class TokenEnumerator: IEnumerator
       {
          private int position = -1;
          private TokenCollection tokens;

          /// <summary>Standard constructor</summary> 
          public TokenEnumerator(TokenCollection tokens)
          {
             this.tokens = tokens;
          }

          /// <summary>IEnumerable::MoveNext implementation</summary> 
          public bool MoveNext()
          {
             if (this.tokens.elements != null && position < this.tokens.elements.Length - 1)
             {
                position++;
                return true;
             }
             else
             {
                return false;
             }
          }

          /// <summary>IEnumerable::Reset implementation</summary> 
          public void Reset()
          {
             position = -1;
          }

          /// <summary>Current implementation, non interface, type-safe</summary> 
          public string Current
          {
             get
             {
                return this.tokens.elements != null ? this.tokens.elements[position] : null;
             }
          }

          /// <summary>Current implementation, interface, not type-safe</summary> 
          object IEnumerator.Current 
          {
             get
             {
                return this.tokens.elements != null ? this.tokens.elements[position] : null;
             }
          }
       }
    }
    /////////////////////////////////////////////////////////////////////////////

}   /////////////////////////////////////////////////////////////////////////////
