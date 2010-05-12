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


  public class DataFeedExample
  {

    private const String CLIENT_USERNAME = "INSERT_LOGIN_EMAIL_HERE";
    private const String CLIENT_PASS = "INSERT_PASSWORD_HERE";
    private const String TABLE_ID = "INSERT_TABLE_ID_HERE";

    public DataFeed feed;
    public DataFeed feed2;

    public static void Main(String[] args)
    {
      DataFeedExample example;

      try
      {
        example = new DataFeedExample();
      }
      catch (AuthenticationException e)
      {
        Console.Error.WriteLine("Authentication failed : " + e.Message);
        return;
      }
      catch (Google.GData.Client.GDataRequestException e)
      {
        Console.Error.WriteLine("Authentication failed : " + e.Message);
        return;
      }

      example.printFeedData();
      example.printFeedDataSources();
      example.printFeedAggregates();
      example.printSegmentInfo();
      example.printDataForOneEntry();

      Console.WriteLine(example.getEntriesAsTable());
    }

    /**
     * Creates a new service object, attempts to authorize using the Client Login
     * authorization mechanism and requests data from the Google Analytics API.
     */
    public DataFeedExample()
    {

      // Configure GA API.
      AnalyticsService asv = new AnalyticsService("gaExportAPI_acctSample_v2.0");

      // Client Login Authorization.
      asv.setUserCredentials(CLIENT_USERNAME, CLIENT_PASS);

      // GA Data Feed query uri.
      String baseUrl = "https://www.google.com/analytics/feeds/data";

      DataQuery query = new DataQuery(baseUrl);
      query.Ids = TABLE_ID;
      query.Dimensions = "ga:source,ga:medium";
      query.Metrics = "ga:visits,ga:bounces";
      query.Segment = "gaid::-11";
      query.Filters = "ga:medium==referral";
      query.Sort = "-ga:visits";
      query.NumberToRetrieve = 5;
      query.GAStartDate = "2010-03-01";
      query.GAEndDate = "2010-03-15";
      Uri url = query.Uri;
      Console.WriteLine("URL: " + url.ToString());


      // Send our request to the Analytics API and wait for the results to
      // come back.

      feed = asv.Query(query);


    }

    /**
     * Prints the important Google Analytics relates data in the Data Feed.
     */
    public void printFeedData()
    {
      Console.WriteLine("\n-------- Important Feed Information --------");
      Console.WriteLine(
        "\nFeed Title      = " + feed.Title.Text +
        "\nFeed ID         = " + feed.Id.Uri +
        "\nTotal Results   = " + feed.TotalResults +
        "\nSart Index      = " + feed.StartIndex +
        "\nItems Per Page  = " + feed.ItemsPerPage
        );
    }

    /**
     * Prints the important information about the data sources in the feed.
     * Note: the GA Export API currently has exactly one data source.
     */
    public void printFeedDataSources()
    {

      DataSource gaDataSource = feed.DataSource;
      Console.WriteLine("\n-------- Data Source Information --------");
      Console.WriteLine(
        "\nTable Name      = " + gaDataSource.TableName +
        "\nTable ID        = " + gaDataSource.TableId +
        "\nWeb Property Id = " + gaDataSource.WebPropertyId +
        "\nProfile Id      = " + gaDataSource.ProfileId +
        "\nAccount Name    = " + gaDataSource.AccountName);
    }

    /**
     * Prints all the metric names and values of the aggregate data. The
     * aggregate metrics represent the sum of the requested metrics across all
     * of the entries selected by the query and not just the rows returned.
     */
    public void printFeedAggregates()
    {
      Console.WriteLine("\n-------- Aggregate Metric Values --------");
      Aggregates aggregates = feed.Aggregates;

      foreach (Metric metric in aggregates.Metrics)
      {
        Console.WriteLine(
          "\nMetric Name  = " + metric.Name +
          "\nMetric Value = " + metric.Value +
          "\nMetric Type  = " + metric.Type +
          "\nMetric CI    = " + metric.ConfidenceInterval);
      }
    }

    /**
     * Prints segment information if the query has an advanced segment defined.
     */
    public void printSegmentInfo()
    {
      if (feed.Segments.Count > 0)
      {
        Console.WriteLine("\n-------- Advanced Segments Information --------");
        foreach (Segment segment in feed.Segments)
        {
          Console.WriteLine(
              "\nSegment Name       = " + segment.Name +
              "\nSegment ID         = " + segment.Id +
              "\nSegment Definition = " + segment.Definition.Value);
        }
      }
    }

    /**
     * Prints all the important information from the first entry in the
     * data feed.
     */
    public void printDataForOneEntry()
    {
      Console.WriteLine("\n-------- Important Entry Information --------\n");
      if (feed.Entries.Count == 0)
      {
        Console.WriteLine("No entries found");
      }
      else
      {
        DataEntry singleEntry = feed.Entries[0] as DataEntry;

        // Properties specific to all the entries returned in the feed.
        Console.WriteLine("Entry ID    = " + singleEntry.Id.Uri);
        Console.WriteLine("Entry Title = " + singleEntry.Title.Text);

        // Iterate through all the dimensions.
        foreach (Dimension dimension in singleEntry.Dimensions)
        {
          Console.WriteLine("Dimension Name  = " + dimension.Name);
          Console.WriteLine("Dimension Value = " + dimension.Value);
        }

        // Iterate through all the metrics.
        foreach (Metric metric in singleEntry.Metrics)
        {
          Console.WriteLine("Metric Name  = " + metric.Name);
          Console.WriteLine("Metric Value = " + metric.Value);
          Console.WriteLine("Metric Type  = " + metric.Type);
          Console.WriteLine("Metric CI    = " + metric.ConfidenceInterval);
        }
      }
    }

    /**
     * Get the data feed values in the feed as a string.
     * @return {String} This returns the contents of the feed.
     */
    public String getEntriesAsTable()
    {
      if (feed.Entries.Count == 0)
      {
        return "No entries found";
      }
      DataEntry singleEntry = feed.Entries[0] as DataEntry;

      StringBuilder feedDataLines = new StringBuilder("\n-------- All Entries In A Table --------\n");

      // Put all the dimension and metric names into an array.
      foreach (Dimension dimension in singleEntry.Dimensions)
      {
        String[] args = { dimension.Name, dimension.Value };
        feedDataLines.AppendLine(String.Format("\n{0} \t= {1}", args));
      }
      foreach (Metric metric in singleEntry.Metrics)
      {
        String[] args = { metric.Name, metric.Value };
        feedDataLines.AppendLine(String.Format("\n{0} \t= {1}", args));
      }

      feedDataLines.Append("\n");
      return feedDataLines.ToString();
    }
  }
}
