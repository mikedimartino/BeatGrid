using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BeatGrid.Model
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

	public class Measure
	{
		public Measure(TimeSignature timeSignature, NoteType divisionLevel, int rows)
		{
			TimeSignature = timeSignature;
			DivisionLevel = divisionLevel;

			int columns = timeSignature.NotesPerMeasure * 
					(divisionLevel.GetDenominator() / timeSignature.NoteType.GetDenominator());

			Cells = new Cell[rows, columns];
			for(int r = 0; r < rows; r++)
			{
				for(int c = 0; c < columns; c++)
				{
					Cells[r, c] = new Cell();
				}
			}
		}

		public TimeSignature TimeSignature { get; set; }
		public NoteType DivisionLevel { get; set; } // Show 32nd, 16th, or 8th notes
		public Cell[,] Cells { get; set; }

		public static Measure GetTestMeasure()
		{
			var ts = new TimeSignature(4, NoteType.Quarter);
			var measure = new Measure(ts, NoteType.Sixteenth, 8);

			measure.Cells[0, 0].On = true;
			measure.Cells[0, 2].On = true;
			measure.Cells[0, 4].On = true;
			measure.Cells[0, 6].On = true;
			measure.Cells[0, 8].On = true;
			measure.Cells[0, 10].On = true;
			measure.Cells[0, 12].On = true;
			measure.Cells[0, 14].On = true;

			measure.Cells[1, 4].On = true;
			measure.Cells[1, 12].On = true;

			measure.Cells[2, 0].On = true;
			measure.Cells[2, 8].On = true;

			return measure;
		}
	}

	public class Cell
	{
		public Cell(bool on = false)
		{
			On = on;
		}

		public bool On { get; set; }
	}

	public class TimeSignature
	{
		public TimeSignature(int notesPearMeasure, NoteType noteType)
		{
			NotesPerMeasure = notesPearMeasure;
			NoteType = noteType;
		}

		public int NotesPerMeasure { get; set; }
		public NoteType NoteType { get; set; }
	}

	//  16 is probably the max that could fit on the screen.
	//	Can just add more measures and increase tempo to simulate 32nd, 64th, etc..
	public enum NoteType
	{
		Whole,
		Half,
		Quarter,
		Eight,
		Sixteenth,
		ThirtySecond
	}

}
