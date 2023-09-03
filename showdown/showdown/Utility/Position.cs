using System;
using System.Runtime.InteropServices;

namespace showdown.Utility
{
	public enum Position{
        DH,
        C,
		_1B,
		_2B,
		SS,
		_3B,
		LF,
		RF,
		CF,
		P
	}

    public class PositionInfo
    {

        public static Dictionary<Position, string> PositionToString =
        new Dictionary<Position, string> {
            { Position.C, "C"},
            { Position._1B, "1B"},
            { Position._2B, "2B"},
            { Position.SS, "SS"},
            { Position._3B, "3B"},
            { Position.LF, "LF"},
            { Position.RF, "RF"},
            { Position.CF, "CF"},
            { Position.DH, "DH"},
            { Position.P, "P"}
        };

        public static Dictionary<Position, string> PositionToStringLong =
            new Dictionary<Position, string> {
            { Position.C, "Catcher"},
            { Position._1B, "First Base"},
            { Position._2B, "Second Base"},
            { Position.SS, "Shortstop"},
            { Position._3B, "Third Base"},
            { Position.LF, "Left Field"},
            { Position.RF, "Right Field"},
            { Position.CF, "Center Field"},
            { Position.DH, "Designated Hitter"},
            { Position.P, "Pitcher"}
            };

        public static Position ParsePositionPlayer(string position)
        {
            //Handle 1B, 2B, 3B
            string modPosition = position;
            if (!string.IsNullOrEmpty(position) && char.IsDigit(position[0]))
            {
                modPosition = "_" + modPosition;
            }

            Enum.TryParse(modPosition, true, out Position result);
            return result;
        }

        public string PositionDisplay(Position cardSet, bool longName = false)
        {
            return longName ? PositionLongName(cardSet) : PositionShortName(cardSet);
             
        }

        public string PositionLongName(Position cardSet)
        {
            if (PositionToStringLong.ContainsKey(cardSet))
            {
                return PositionToStringLong[cardSet];
            }
            else
            {
                return "";
            }

        }

        public string PositionShortName(Position cardSet)
        {
            if (PositionToString.ContainsKey(cardSet))
            {
                return PositionToString[cardSet];
            }
            else
            {
                return "";
            }

        }
    }


}

