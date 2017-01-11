using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatGrid
{
	public class TimeSignature
	{
		public TimeSignature() { }
		public TimeSignature(int notesPearMeasure, NoteType noteType)
		{
			NotesPerMeasure = notesPearMeasure;
			NoteType = noteType;
		}

		public int NotesPerMeasure { get; set; }
		public NoteType NoteType { get; set; }

		public static TimeSignature Default = new TimeSignature(4, NoteType.Quarter);
	}
}
