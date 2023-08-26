// Testing Console
using showdown.Retrieval;
using showdown.Utility;

Console.WriteLine("Hello, World!");

DeserializeCSV deserializeCSV = new DeserializeCSV();
List<PlayerCardCSV> playercards = deserializeCSV.Deserialize(CardSetVersion.Base00);

foreach (PlayerCardCSV playerCardCSV in playercards)
{
    Console.WriteLine(playerCardCSV.ToString());
}

Thread.Sleep(5000);



