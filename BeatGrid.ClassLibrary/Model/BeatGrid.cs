using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BeatGrid
{
	public class Beat
	{
		public Beat(string name) {}

		public Beat(string name, TimeSignature timeSignature, NoteType divisionLevel, int measureCount)
		{
			Name = name;
			TimeSignature = timeSignature;
			DivisionLevel = divisionLevel;
			MeasureCount = measureCount;
		}

		public string Name { get; set; }
		public int Id { get; set; }

		public TimeSignature TimeSignature { get; set; }
		public NoteType DivisionLevel { get; set; } // Show 32nd, 16th, or 8th notes
		public int MeasureCount { get; set; }
		public List<Measure> Measures { get; set; }
	}
}
