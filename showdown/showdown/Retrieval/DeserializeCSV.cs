using System;
using showdown.Utility;
using CsvHelper;
using System.Globalization;

namespace showdown.Retrieval
{
	public class DeserializeCSV
	{
		private Dictionary<CardSetVersion, string> FileNames =
			new Dictionary<CardSetVersion, string> {
				{CardSetVersion.Base00, "MLB_Showdown.xls - 2000.csv"},
				{CardSetVersion.PR00, "MLB_Showdown.xls - 2000.csv" }
            };

        private string DataFilePath = "Data/";

		public DeserializeCSV()
		{

		}

		public List<PlayerCardCSV> Deserialize(CardSetVersion cardSetVersion)
		{
            string fileName = FileNames[cardSetVersion];
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DataFilePath, fileName);

            Console.WriteLine(filePath);
            Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory);
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file {fileName} was not found.", fileName);
            }

            var reader = new StreamReader(filePath);
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<PlayerCardCSV>().ToList();
                return records;
            }
        }

	}
}

