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
            var records = new List<PlayerCardCSV>();
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                records = csv.GetRecords<PlayerCardCSV>().ToList();
            }

            //NotDuplicateRecord(records);
            records = records.Where(r => IsPlayerInSet(r, cardSetVersion)).ToList();

            return records;
        }

        public void NotDuplicateRecord(List<PlayerCardCSV> playerCards)
        {
            HashSet<string> nameNumberHash = new HashSet<string>();
            List<PlayerCardCSV> toBeRemoved = new List<PlayerCardCSV>();
            foreach(PlayerCardCSV player in playerCards)
            {
                string nameNumber = player.Name + player.SetNumber;
                if (nameNumberHash.Contains(nameNumber))
                {
                    toBeRemoved.Add(player);
                } else
                {
                    nameNumberHash.Add(nameNumber);
                }
            }

            foreach (PlayerCardCSV removePlayer in toBeRemoved)
            {
                playerCards.Remove(removePlayer);
            }
        }

        public bool IsPlayerInSet(PlayerCardCSV playerCard, CardSetVersion cardSetVersion)
        {
            if (cardSetVersion == CardSetVersion.Base00)
            {
                if (!string.IsNullOrEmpty(playerCard.Name) && playerCard.Name[0] == '*')
                {
                    return false;
                }
            }
            else if (cardSetVersion == CardSetVersion.PR00)
            {
                if (!string.IsNullOrEmpty(playerCard.Name) && playerCard.Name[0] != '*')
                {
                    return false;
                }
                playerCard.Name = playerCard.Name.Substring(1);
            }
            else
            {
                return false;
            }

            if (playerCard.Set.Trim() != "1st")
            {
                return false;
            }

            return true;

        }

	}
}

