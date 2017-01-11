using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatGrid
{
	public class Constants
	{
		public const int NEW_BEAT_ID = -1;
		public const int MAX_ACTIVE_SOUNDS = 10;
		public const int MIN_TEMPO = 40;
		public const int MAX_TEMPO = 240;
		public const int DEFAULT_TEMPO = 100;

		public const NoteType DefaultDivisionLevel = NoteType.Sixteenth;
	}
}
