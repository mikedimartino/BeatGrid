using System;
using System.Collections.Generic;
using System.Text;

namespace BeatGrid.Model
{
    public static class ExtensionMethods
    {
			// The "denominator" is the bottom number in the time signature (e.g. in 3/4 time the denominator = 4).
			public static int GetDenominator(this NoteType noteType)
			{
				switch (noteType)
				{
					case NoteType.Whole: return 1;
					case NoteType.Half: return 2;
					case NoteType.Quarter: return 4;
					case NoteType.Eight: return 8;
					case NoteType.Sixteenth: return 16;
					case NoteType.ThirtySecond: return 32;
					default: return 4; // Should never happen.
				}
			}
	}
}
