/* Copyright (c) 2007 Google Inc.
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
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting;

namespace DocListUploader
{
    /// <summary>
    /// This is a simple class to pass messages between instances of
    /// the DocList Uploader application.
    /// </summary>
    class RemoteMessage : MarshalByRefObject
    {
        private HiddenForm mainForm;

        /// <summary>
        /// Creates a new instance of the Well Known Object
        /// </summary>
        /// <param name="mainForm">A reference back to the form listening.</param>
        public RemoteMessage(HiddenForm mainForm)
        {
            this.mainForm = mainForm;
        }

        /// <summary>
        /// Passes a filename back to the listening form.
        /// </summary>
        /// <param name="file">The file to send across the wire.</param>
        public void SendMessage(string file)
        {
            mainForm.HandleUpload(file);
        }
    }
}
