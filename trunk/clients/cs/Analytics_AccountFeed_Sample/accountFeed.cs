// Copyright 2010 Google Inc. All Rights Reserved.

/* Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
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
using Google.Analytics;
using Google.GData.Analytics;
using Google.GData.Client;
using Google.GData.Extensions;

namespace SampleAnalyticsClient
{
  public class AccountFeedExample
  {

    private static String CLIENT_USERNAME = "INSERT_LOGIN_EMAIL_HERE";
    private static String CLIENT_PASS = "INSERT_PASSWORD_HERE";

    public AccountFeed accountFeed;

    public static void Main(String[] args)
    {

      AccountFeedExample example;

      try
      {
        example = new AccountFeedExample();
      }
      catch (AuthenticationException e)
      {
        Console.Error.WriteLine("Authentication failed : " + e.Message);
        return;
      }
      catch (Exception e)
      {
        Console.Error.WriteLine("Authentication Failed : " + e.Message);
        return;
      }


      example.printFeedDetails();
      example.printAdvancedSegments();
      example.printCustomVarForOneEntry();
      example.printGoalsForOneEntry();
      example.printAccountEntries();
    }

    /**
     * Creates a new service object, attempts to authorize using the Client Login
     * authorization mechanism and requests data from the Google Analytics API.
     * @throws AuthenticationException if an error occurs with authorizing with
     *     Google Accounts.
     * @throws IOException if a network error occurs.
     * @throws ServiceException if an error occurs with the Google Analytics API.
     */
    public AccountFeedExample()
    {

      // Configure GA API.
      AnalyticsService asv = new AnalyticsService("gaExportAPI_acctSample_v2.0");

      // Client Login Authorization.
      asv.setUserCredentials(CLIENT_USERNAME, CLIENT_PASS);

      // GA Account Feed query uri.

      AccountQuery query = new AccountQuery();

      // Send our request to the Analytics API and wait for the results to
      // come back.
      accountFeed = asv.Query(query);
    }

    /**
     * Prints the important Google Analytics related data in the Account Feed.
     */
    public void printFeedDetails()
    {
      Console.WriteLine("\n-------- Important Feed Data --------");
      Console.WriteLine(
          "\nFeed Title     = " + accountFeed.Title +
          "\nTotal Results  = " + accountFeed.TotalResults +
          "\nStart Index    = " + accountFeed.StartIndex +
          "\nItems Per Page = " + accountFeed.ItemsPerPage +
          "\nFeed ID        = " + accountFeed.Id);
    }

    /**
     * Prints the advanced segments for this user.
     */
    public void printAdvancedSegments()
    {
      Console.WriteLine("\n-------- Advanced Segments --------");
      if (accountFeed.Segments.Count == 0)
      {
        Console.WriteLine("No advanced segments found");
      }
      else
      {
        foreach (Segment segment in accountFeed.Segments)
        {
          Console.WriteLine(
            "\nSegment Name       = " + segment.Name +
            "\nSegment ID         = " + segment.Id +
            "\nSegment Definition = " + segment.Definition.Value);
        }
      }
    }

    /**
     * Prints custom variable information for the first profile that has custom
     * variables configured.
     */
    public void printCustomVarForOneEntry()
    {
      Console.WriteLine("\n-------- Custom Variables --------");
      if (accountFeed.Entries.Count == 0)
      {
        Console.WriteLine("No entries found.");
      }
      else
      {
        // Go through each entry to see if any has a Custom Variable defined.
        foreach (AccountEntry entry in accountFeed.Entries)
        {
          if (entry.CustomVariables.Count > 0)
          {
            foreach (CustomVariable customVariable in entry.CustomVariables)
            {
              Console.WriteLine(
                  "\nCustom Variable Index = " + customVariable.Index +
                  "\nCustom Variable Name  = " + customVariable.Name +
                  "\nCustom Variable Scope = " + customVariable.Scope);
            }
            return;
          }
        }
        Console.WriteLine("\nNo custom variables defined for this user");
      }
    }

    /**
     * Prints all the goal information for one profile.
     */
    public void printGoalsForOneEntry()
    {
      Console.WriteLine("\n-------- Goal Configuration --------");
      if (accountFeed.Entries.Count == 0)
      {
        Console.WriteLine("No entries found.");
      }
      else
      {
        // Go through each entry to see if any have Goal information.
        foreach (AccountEntry entry in accountFeed.Entries)
        {
          if (entry.Goals.Count > 0)
          {
            foreach (Goal goal in entry.Goals)
            {
              // Print common information for all Goals in this profile.
              Console.WriteLine("\n----- Goal -----");
              Console.WriteLine(
                  "\nGoal Number = " + goal.Number +
                  "\nGoal Name   = " + goal.Name +
                  "\nGoal Value  = " + goal.Value +
                  "\nGoal Active = " + goal.Active);
              if (goal.Destination != null)
              {
                printDestinationGoal(goal.Destination);
              }
              else if (goal.Engagement != null)
              {
                printEngagementGoal(goal.Engagement);
              }
            }
            return;
          }
        }
      }
    }

    /**
     * Prints the important information for destination goals including all the
     * configured steps if they exist.
     * @param destination the destination goal configuration.
     */
    public void printDestinationGoal(Destination destination)
    {
      Console.WriteLine("\n\t----- Destination Goal -----");
      Console.WriteLine(
          "\n\tExpression      = " + destination.Expression +
          "\n\tMatch Type      = " + destination.MatchType +
          "\n\tStep 1 Required = " + destination.Step1Required +
          "\n\tCase Sensitive  = " + destination.CaseSensitive);

      // Print goal steps.
      if (destination.Steps.Count > 0)
      {
        Console.WriteLine("\n\t----- Destination Goal Steps -----");
        foreach (Step step in destination.Steps)
        {
          Console.WriteLine(
              "\n\tStep Number = " + step.Number +
              "\n\tStep Name   = " + step.Name +
              "\n\tStep Path   = " + step.Path);
        }
      }
    }


    /**
     * Prints the important information for Engagement Goals.
     * @param engagement The engagement goal configuration.
     */
    public void printEngagementGoal(Engagement engagement)
    {
      Console.WriteLine("\n\t----- Engagement Goal -----");
      Console.WriteLine(
          "\n\tGoal Type       = " + engagement.Type +
          "\n\tGoal Comparison = " + engagement.Comparison +
          "\n\tGoal Threshold  = " + engagement.Threshold);
    }

    /**
     * Prints the important Google Analytics related data in each Account Entry.
     */
    public void printAccountEntries()
    {
      Console.WriteLine("\n-------- First 1000 Profiles In Account Feed --------");
      if (accountFeed.Entries.Count == 0)
      {
        Console.WriteLine("No entries found.");
      }
      else
      {
        foreach (AccountEntry entry in accountFeed.Entries)
        {
          Console.WriteLine(
            "\nWeb Property Id = " + entry.Properties[3].Value +
            "\nAccount Name    = " + entry.Properties[1].Value +
            "\nAccount ID      = " + entry.Properties[0].Value +
            "\nProfile Name    = " + entry.Title.Text +
            "\nProfile ID      = " + entry.Properties[2].Value +
            "\nTable Id        = " + entry.ProfileId.Value +
            "\nCurrency        = " + entry.Properties[4].Value +
            "\nTimeZone        = " + entry.Properties[5].Value +
            (entry.CustomVariables.Count > 0 ? "\nThis profile has custom variables" : "") +
            (entry.Goals.Count > 0 ? "\nThis profile has goals" : ""));
        }
      }
    }
  }
}
