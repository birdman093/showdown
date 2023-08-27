using System;
using System.Runtime.InteropServices;

namespace showdown.Utility
{
	public enum Position{
		C,
		FirstBase,
		SecondBase,
		SS,
		ThirdBase,
		LF,
		RF,
		CF,
		DH,
		P
	}


    public class PositionInfo
    {

        public static Dictionary<Position, string> PositionToStringShort =
        new Dictionary<Position, string> {
            { Position.C, "C"},
            { Position.FirstBase, "1B"},
            { Position.SecondBase, "2B"},
            { Position.SS, "SS"},
            { Position.ThirdBase, "3B"},
            { Position.LF, "LF"},
            { Position.RF, "RF"},
            { Position.CF, "CF"},
            { Position.DH, "DH"},
            { Position.P, "P"}
        };

        public static Dictionary<Position, string> PositionToStringLong =
            new Dictionary<Position, string> {
            { Position.C, "Catcher"},
            { Position.FirstBase, "First Base"},
            { Position.SecondBase, "Second Base"},
            { Position.SS, "Shortstop"},
            { Position.ThirdBase, "Third Base"},
            { Position.LF, "Left Field"},
            { Position.RF, "Right Field"},
            { Position.CF, "Center Field"},
            { Position.DH, "Designated Hitter"},
            { Position.P, "Pitcher"}
            };

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
            if (PositionToStringShort.ContainsKey(cardSet))
            {
                return PositionToStringShort[cardSet];
            }
            else
            {
                return "";
            }

        }
    }


}

