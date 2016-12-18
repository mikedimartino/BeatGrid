using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatGrid
{
	public class Measure
	{
		public Measure() { }
		public Measure(TimeSignature timeSignature, NoteType divisionLevel, int rows)
		{
			Rows = rows;
			TimeSignature = timeSignature;
			DivisionLevel = divisionLevel;

			int columns = timeSignature.NotesPerMeasure *
					(divisionLevel.GetDenominator() / timeSignature.NoteType.GetDenominator());

			Columns = columns;

			Cells = new Cell[rows, columns];
			for (int r = 0; r < rows; r++)
			{
				for (int c = 0; c < columns; c++)
				{
					Cells[r, c] = new Cell()
					{
						On = false,
						Row = r,
						Column = c
					};
				}
			}
		}

		public Measure(int rows, int columns)
		{
			Cells = new Cell[rows, columns];
			Rows = rows;
			Columns = columns;
			TimeSignature = null;
			DivisionLevel = NoteType.Unknown;
		}

		public int Rows { get; set; }
		public int Columns { get; set; }
		public TimeSignature TimeSignature { get; set; }
		public NoteType DivisionLevel { get; set; } // Show 32nd, 16th, or 8th notes
		public Cell[,] Cells { get; set; }

		public Cell this[int row, int column]
		{
			get { return Cells[row, column]; }
			set { Cells[row, column] = value; }
		}

		public void Clear()
		{
			foreach (Cell cell in Cells) cell.On = false;
		}

		public static Measure GetEmptyMeasure()
		{
			var ts = new TimeSignature(4, NoteType.Quarter);
			var measure = new Measure(ts, NoteType.Sixteenth, 8);
			return measure;
		}

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

		public static Measure GetRandomTestMeasure()
		{
			var ts = new TimeSignature(4, NoteType.Quarter);
			var measure = new Measure(ts, NoteType.Sixteenth, 8);
			var rand = new Random();

			for (int i = 0; i < measure.Cells.GetLength(0); i++)
			{
				for (int j = 0; j < measure.Cells.GetLength(1); j++)
				{
					measure.Cells[i, j].On = rand.Next(0, 6) == 0;
				}
			}

			return measure;
		}

	}
}
