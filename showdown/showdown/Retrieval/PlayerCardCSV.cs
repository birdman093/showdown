using System;

namespace showdown.Retrieval
{
	public class PlayerCardCSV
	{
		public string SetNumber { get; set; }
		public string Set { get; set; }
        public string Name { get; set; }
        public string Team { get; set; }
        public string Pts { get; set; }
        public string Yr { get; set; }
        public string OB_C { get; set; }
        public string Spd_IP { get; set; }
        public string Pos { get; set; }
        public string H { get; set; }
        public string PU { get; set; }
        public string SO { get; set; }
        public string GB { get; set; }
        public string FB { get; set; }
        public string W { get; set; }
        public string S { get; set; }
        public string SPlus { get; set; }
        public string DB { get; set; }
        public string TR { get; set; }
        public string HR { get; set; }

        public PlayerCardCSV()
        {
                
        }
        public override string ToString()
        {
            return $"SetNumber: {SetNumber}, Set: {Set}, Name: {Name}, " +
                $"Team: {Team}, Pts: {Pts}, Yr: {Yr}, OB/C: {OB_C}, " +
                $"Spd/IP: {Spd_IP}, Pos: {Pos}, H: {H}, PU: {PU}, SO: {SO}, " +
                $"GB: {GB}, FB: {FB}, W: {W}, S: {S}, SPlus: {SPlus}, DB: {DB}, " +
                $"TR: {TR}, HR: {HR}";
        }
    }
}

