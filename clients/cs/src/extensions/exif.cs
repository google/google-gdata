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

using System;
using System.Xml;
//using System.Collections;
//using System.Text;
using Google.GData.Client;

namespace Google.GData.Extensions.Exif {

    /// <summary>
    /// helper to instantiate all factories defined in here and attach 
    /// them to a base object
    /// </summary>
    public class ExifExtensions
    {
        public static void AddExtension(AtomBase baseObject) 
        {
            baseObject.AddExtension(new ExifTags());
        }
    }
  
    public class ExifNameTable 
    {
          /// <summary>static string to specify the exif namespace
          /// </summary>
        public const string NSExif  = "http://schemas.google.com/photos/exif/2007";
        /// <summary>static string to specify the used exif prefix</summary>
        public const string ExifPrefix = "exif";
     }

    

    /// <summary>
    /// MediaGroup container element for the MediaRss namespace
    /// </summary>
    public class ExifTags : SimpleContainer
    {
        public ExifTags() :
            base("tags",
                 ExifNameTable.ExifPrefix,
                 ExifNameTable.NSExif)
        {
            this.ExtensionFactories.Add(new ExifDistance());
            this.ExtensionFactories.Add(new ExifExposure());
            this.ExtensionFactories.Add(new ExifFlash());
            this.ExtensionFactories.Add(new ExifFocalLength());
            this.ExtensionFactories.Add(new ExifFStop());
            this.ExtensionFactories.Add(new ExifImageUniqueID());
            this.ExtensionFactories.Add(new ExifISO());
            this.ExtensionFactories.Add(new ExifMake());
            this.ExtensionFactories.Add(new ExifModel());
            this.ExtensionFactories.Add(new ExifTime());
        }
   }

    /// <summary>
    /// ExifDistance schema extension describing an distance
    /// </summary>
    public class ExifDistance : SimpleElement
    {
        public ExifDistance()
        : base("distance", ExifNameTable.ExifPrefix, ExifNameTable.NSExif)
         {}
        public ExifDistance(string initValue)
        : base("distance", ExifNameTable.ExifPrefix, ExifNameTable.NSExif, initValue)
        {}
    }

    /// <summary>
    /// ExifExposure schema extension describing an exposure
    /// </summary>
    public class ExifExposure : SimpleElement
    {
        public ExifExposure()
        : base("exposure", ExifNameTable.ExifPrefix, ExifNameTable.NSExif)
         {}
        public ExifExposure(string initValue)
        : base("exposure", ExifNameTable.ExifPrefix, ExifNameTable.NSExif, initValue)
        {}
    }

    /// <summary>
    /// ExifFlash schema extension describing an flash
    /// </summary>
    public class ExifFlash : SimpleElement
    {
        public ExifFlash()
        : base("flash", ExifNameTable.ExifPrefix, ExifNameTable.NSExif)
         {}
        public ExifFlash(string initValue)
        : base("flash", ExifNameTable.ExifPrefix, ExifNameTable.NSExif, initValue)
        {}
    }

    /// <summary>
    /// ExifFocalLength schema extension describing an focallength
    /// </summary>
    public class ExifFocalLength : SimpleElement
    {
        public ExifFocalLength()
        : base("focallength", ExifNameTable.ExifPrefix, ExifNameTable.NSExif)
         {}
        public ExifFocalLength(string initValue)
        : base("focallength", ExifNameTable.ExifPrefix, ExifNameTable.NSExif, initValue)
        {}
    }

    /// <summary>
    /// ExifFStop schema extension describing an fstop
    /// </summary>
    public class ExifFStop : SimpleElement
    {
        public ExifFStop()
        : base("fstop", ExifNameTable.ExifPrefix, ExifNameTable.NSExif)
         {}
        public ExifFStop(string initValue)
        : base("fstop", ExifNameTable.ExifPrefix, ExifNameTable.NSExif, initValue)
        {}
    }

    /// <summary>
    /// ExifImageUniqueID schema extension describing an imageUniqueID
    /// </summary>
    public class ExifImageUniqueID : SimpleElement
    {
        public ExifImageUniqueID()
        : base("imageUniqueID", ExifNameTable.ExifPrefix, ExifNameTable.NSExif)
         {}
        public ExifImageUniqueID(string initValue)
        : base("imageUniqueID", ExifNameTable.ExifPrefix, ExifNameTable.NSExif, initValue)
        {}
    }

    /// <summary>
    /// ExifISO schema extension describing an iso
    /// </summary>
    public class ExifISO : SimpleElement
    {
        public ExifISO()
        : base("iso", ExifNameTable.ExifPrefix, ExifNameTable.NSExif)
         {}
        public ExifISO(string initValue)
        : base("iso", ExifNameTable.ExifPrefix, ExifNameTable.NSExif, initValue)
        {}
    }

    /// <summary>
    /// ExifMake schema extension describing an make
    /// </summary>
    public class ExifMake : SimpleElement
    {
        public ExifMake()
        : base("make", ExifNameTable.ExifPrefix, ExifNameTable.NSExif)
         {}
        public ExifMake(string initValue)
        : base("make", ExifNameTable.ExifPrefix, ExifNameTable.NSExif, initValue)
        {}
    }

    /// <summary>
    /// ExifModel schema extension describing an model
    /// </summary>
    public class ExifModel : SimpleElement
    {
        public ExifModel()
        : base("model", ExifNameTable.ExifPrefix, ExifNameTable.NSExif)
         {}
        public ExifModel(string initValue)
        : base("model", ExifNameTable.ExifPrefix, ExifNameTable.NSExif, initValue)
        {}
    }

    /// <summary>
    /// ExifTime schema extension describing an time
    /// </summary>
    public class ExifTime : SimpleElement
    {
        public ExifTime()
        : base("time", ExifNameTable.ExifPrefix, ExifNameTable.NSExif)
         {}
        public ExifTime(string initValue)
        : base("time", ExifNameTable.ExifPrefix, ExifNameTable.NSExif, initValue)
        {}
    }
}
