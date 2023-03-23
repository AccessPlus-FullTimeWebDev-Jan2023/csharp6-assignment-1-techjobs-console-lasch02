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
        public static List<Dictionary<string, string>> FindByValue(string value) //do I put 2 arguments here? User is selecting "Search" first, then by value; i kept getting error until I added.
        {
            // load data, if not already loaded
            LoadData();
            //type here
            List<Dictionary<string, string>> jobs = new List<Dictionary<string, string>>(); //this is creating a list of dictionary entries

            foreach (Dictionary<string, string> row in AllJobs) //this is iterating through all jobs and pulling 1 row(job) at a time; each row is one combined string
            {
                //fix if it isn't to if it "contains"
                //fix what I am storing in another search term
                //create another loop
                foreach (string key in row.Keys)//this one is iterating thru each cell (separating the row which was one whole string and breaking each stri by it's value), checking the user input to see if it matches each cell.
                {
                    string userValue = row[key]; //this gives me a place to hold the userValue

                    if (userValue.ToLower().Contains(value.ToLower()))

                    {
                        jobs.Add(row); //this is where values are added 
                        break; //once a value is found in the row, it will stop iterating that row. This prevents it from iterating thru the rest of the same row and finding the same value in that row
                    }
                }
            }
                            return jobs; ///this was here already //I need to change out null to something
            }

        /**Returns results of search the jobs data by key/value, using inclusion of the search term. *
         * For example, searching for employer "Enterprise" will include results with "Enterprise Holdings, Inc".*/

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

