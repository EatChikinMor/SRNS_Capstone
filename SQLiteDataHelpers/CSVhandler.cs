using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using LumenWorks.Framework.IO;
using LumenWorks.Framework.IO.Csv;

namespace SQLiteDataHelpers
{
    public static class CSVhandler
    {
        public static List<String> readCSV(StreamReader file, bool hasHeaders)
        {
            List<String> charts = new List<String>();

            using (CsvReader csv = new CsvReader(file, hasHeaders))
            {
                while (csv.ReadNextRecord())
                {
                    charts.Add(csv[0]);
                }
            }

            return charts;
        }
    }
}
