using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BeatGrid
{
	public class Beat
	{
		public Beat() { }
		public Beat(string name) { throw new NotImplementedException(); }
		public Beat(string name, List<Measure> measures, TimeSignature timeSignature, NoteType divisionLevel, int measureCount)
		{
			Id = Constants.NEW_BEAT_ID;
			Name = name;
			TimeSignature = timeSignature;
			DivisionLevel = divisionLevel;
			Measures = measures;
		}
		public Beat(List<Measure> measures)
		{
			Id = Constants.NEW_BEAT_ID;
			Name = "_UNNAMED";
			Measures = measures;
		}

		public string Name { get; set; }
		public int Id { get; set; }

		public TimeSignature TimeSignature { get; set; }
		public NoteType DivisionLevel { get; set; } // Show 32nd, 16th, or 8th notes
		public List<Measure> Measures { get; set; }

		public static Beat GetTestBeat(int measureCount = 1)
		{
			var measures = new List<Measure>();
			for(int i = 0; i < measureCount; i++)
			{
				measures.Add(Measure.GetRandomTestMeasure());
			}
			return new Beat(measures);
		}
	}
}
