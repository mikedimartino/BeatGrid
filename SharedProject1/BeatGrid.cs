using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BeatGrid.Model
{
	public class Beat
	{
		public Beat(int rows, int columns)
		{
			Cells = new Cell[rows, columns];
			for(int r = 0; r < rows; r++)
			{
				for (int c = 0; c < columns; c++)
				{
					Cells[r, c] = new Cell(c == 2);
				}
			}
		}

		public Beat(TimeSignature timeSignature, NoteType divisionLevel, int measureCount)
		{
			TimeSignature = timeSignature;
			DivisionLevel = divisionLevel;
			MeasureCount = measureCount;
		}

		public Cell[,] Cells { get; set; }
		public TimeSignature TimeSignature { get; set; }
		public NoteType DivisionLevel { get; set; } // Show 32nd, 16th, or 8th notes
		public int MeasureCount { get; set; }
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
	}

	public class Cell
	{
		public Cell(bool isActive = false)
		{
			IsActive = isActive;
		}

		public bool IsActive { get; set; }
	}

	public class TimeSignature
	{
		public int NotesPerMeasure { get; set; }
		public NoteType NoteType { get; set; }
	}

	// Could add more, but 16 is probably the max that could fit on the screen.
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
