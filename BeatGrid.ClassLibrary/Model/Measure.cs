using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatGrid
{
	public class Measure
	{
		public Measure(TimeSignature timeSignature, NoteType divisionLevel, int rows)
		{
			TimeSignature = timeSignature;
			DivisionLevel = divisionLevel;

			int columns = timeSignature.NotesPerMeasure *
					(divisionLevel.GetDenominator() / timeSignature.NoteType.GetDenominator());

			Cells = new Cell[rows, columns];
			for (int r = 0; r < rows; r++)
			{
				for (int c = 0; c < columns; c++)
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
}
