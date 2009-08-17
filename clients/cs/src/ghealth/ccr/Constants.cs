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
using System.Collections.Generic;
using System.Text;

namespace Google.GData.Health
{
    /// <summary>
    /// Constants used by the core library.
    /// </summary>
    public static class Constants
    {
        public static class Codes
        {
            public static Code Race = new Code() { Value = "S15814", CodingSystem = System.HL7 };
            public static class System
            {
                public const string HL7 = "HL7";
            }
        }

        public static class Types
        {
            public const string Race = "RACE";
            public const string StartDate = "Start date";
            public const string StopDate = "Stop date";
            public const string CollectionStartDate = "Collection start date";
            public const string CollectionStopDate = "Collection stop date";
        }

        public static class CareRecord
        {
            public const string CCRDocumentObjectID = "CCRDocumentObjectID";
            public const string Language = "Language";
            public const string Version = "Version";
            public const string DateTime = "DateTime";
            public const string Patient = "Patient";
            public const string Body = "Body";
            public const string Actors = "Actors";

            public static class Common
            {
                public const string Type = "Type";
                public const string Value = "Value";
                public const string Text = "Text";
                public const string Description = "Description";
                public const string Frequency = "Frequency";
            }

            public static class CodedValue
            {
                public const string Code = "Code";

                public static class CodeNode
                {
                    public const string CodingSystem = "CodingSystem";
                }
            }            

            public static class DateTimeNode
            {                
                public const string ExactDateTime = "ExactDateTime";
            }

            public static class ActingSource
            {
                public const string ActorID = "ActorID";
                public const string ActorRole = "ActorRole";
            }

            public static class BodyNode
            {
                public const string FunctionalStatus = "FunctionalStatus";
                public const string Problems = "Problems";
                public const string SocialHistory = "SocialHistory";
                public const string Alerts = "Alerts";
                public const string Medications = "Medications";
                public const string Immunizations = "Immunizations";
                public const string VitalSigns = "VitalSigns";
                public const string Results = "Results";
                public const string Procedures = "Procedures";
            }

            public static class ActiveDetail
            {
                public const string Status = "Status";
                public const string Source = "Source";
            }

            public static class ProblemNode
            {
                public const string HealthStatus = "HealthStatus";
            }

            public static class SocialHistoryNode
            {
                public const string Episodes = "Episodes";
            }

            public static class Measurement
            {
                public const string Units = "Units";
                public const string Unit = "Unit";
                public const string SequencePosition = "SequencePosition";
                public const string VariableModifier = "VariableModifier";
            }

            public static class AlertNode
            {
                public const string Reaction = "Reaction";
            }

            public static class ReactionNode
            {
                public const string Severity = "Severity";
            }
        }
    }
}
