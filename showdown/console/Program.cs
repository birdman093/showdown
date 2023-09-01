// Testing Console
using showdown.Retrieval;
using showdown.Utility;
using showdown.Player;

Console.WriteLine("Hello, World!");

DeserializeCSV deserializeCSV = new DeserializeCSV();
List<PlayerCardCSV> playercards = deserializeCSV.Deserialize(CardSetVersion.Base00);

foreach (PlayerCardCSV playerCardCSV in playercards)
{
    IPlayer player = PlayerFactory.CreatePlayer(playerCardCSV, CardSetVersion.Base00);
    //Console.WriteLine(player.ToString());
}

Thread.Sleep(5000);



