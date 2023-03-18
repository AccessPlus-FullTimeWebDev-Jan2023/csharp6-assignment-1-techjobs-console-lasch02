using System;
using System.Data.Common;
using System.Text;

namespace TechJobsConsoleAutograded6
{
	public class JobData
	{
        static List<Dictionary<string, string>> AllJobs = new List<Dictionary<string, string>>();
        static bool IsDataLoaded = false;

        public static List<Dictionary<string, string>> FindAll() 
        {
            LoadData();
            return AllJobs;
        }

        /* Returns a list of all values contained in a given column,
         * without duplicates. */
        public static List<string> FindAll(string column) //this is find by column; user inputs a column header (list, then select column header) and this matches the column header with the column value; this prints all jobs values for that column.
        {
            LoadData();

            List<string> values = new List<string>();

            foreach (Dictionary<string, string> job in AllJobs)
            {
                string aValue = job[column];

                if (!values.Contains(aValue)) //if a aValue isn't already in the values list, add it to the values list
                {
                    values.Add(aValue);  //this is where values are added 
                }
            }

            return values;
        }
        /* Search all columns for the given term */

        //TODO: Complete the FindByValue method
        public static List<Dictionary<string, string>> FindByValue(string value)
        {
            // load data, if not already loaded
            LoadData();
            //type here
            List<Dictionary<string, string>> userInputValue = new List<Dictionary<string, string>>(); //this should be a list of Dictionary not just a list of string.

            foreach (Dictionary<string, string> job in AllJobs) //match this
            {
                string aValue = job[value];  //match this but instead of column use value
                if (!value.Contains(aValue)) //if a aValue isn't already in the values list, add it to the values list
                {
                    userInputValue.Add(job);  //this is where values are added 
                }
                //    //hint: I have figured out how to print each field in each column, so I want to do something similar to search every field in every row.
              }
                            return null; ///this was here already
        }

        /**
         * Returns results of search the jobs data by key/value, using
         * inclusion of the search term.
         *
         * For example, searching for employer "Enterprise" will include results
         * with "Enterprise Holdings, Inc".
         */
        public static List<Dictionary<string, string>> FindByColumnAndValue(string column, string value) //this is taking user input (from Search), by which column they chose and matching the values
        {
            // load data, if not already loaded
            LoadData();

            List<Dictionary<string, string>> jobs = new List<Dictionary<string, string>>(); //match this //this is empty dictionary that we are storing our results in

            foreach (Dictionary<string, string> row in AllJobs) //match this// iterating thru all the rows
            {
                string aValue = row[column];  //match this but instead of column use value; user is entering a value; this searches 


                //TODO: Make search case-insensitive
                if (aValue.Contains(value)) //if the value that the user enters matches the search criteria value (ex: location), add it to the row
                {
                    jobs.Add(row);  
                }
            }

            return jobs;
        }

        /*
         * Load and parse data from job_data.csv
         */
        private static void LoadData()
        {

            if (IsDataLoaded)
            {
                return;
            }

            List<string[]> rows = new List<string[]>();

            using (StreamReader reader = File.OpenText("job_data.csv"))
            {
                while (reader.Peek() >= 0)
                {
                    string line = reader.ReadLine();
                    string[] rowArrray = CSVRowToStringArray(line);
                    if (rowArrray.Length > 0)
                    {
                        rows.Add(rowArrray);
                    }
                }
            }

            string[] headers = rows[0];
            rows.Remove(headers);

            // Parse each row array into a more friendly Dictionary
            foreach (string[] row in rows)
            {
                Dictionary<string, string> rowDict = new Dictionary<string, string>();

                for (int i = 0; i < headers.Length; i++)
                {
                    rowDict.Add(headers[i], row[i]);
                }
                AllJobs.Add(rowDict);
            }

            IsDataLoaded = true;
        }

        /*
         * Parse a single line of a CSV file into a string array
         */
        private static string[] CSVRowToStringArray(string row, char fieldSeparator = ',', char stringSeparator = '\"')
        {
            bool isBetweenQuotes = false;
            StringBuilder valueBuilder = new StringBuilder();
            List<string> rowValues = new List<string>();

            // Loop through the row string one char at a time
            foreach (char c in row.ToCharArray())
            {
                if ((c == fieldSeparator && !isBetweenQuotes))
                {
                    rowValues.Add(valueBuilder.ToString());
                    valueBuilder.Clear();
                }
                else
                {
                    if (c == stringSeparator)
                    {
                        isBetweenQuotes = !isBetweenQuotes;
                    }
                    else
                    {
                        valueBuilder.Append(c);
                    }
                }
            }

            // Add the final value
            rowValues.Add(valueBuilder.ToString());
            valueBuilder.Clear();

            return rowValues.ToArray();
        }
    }
}

