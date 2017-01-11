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

		public Measure(Beat beat)
		{
			Rows = beat.Sounds.Count;
			Columns = beat.ColumnsPerMeasure;
			Cells = new Cell[Rows, Columns];
			for (int r = 0; r < Rows; r++)
			{
				for (int c = 0; c < Columns; c++)
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
		}

		public Beat Beat { get; set; } // A reference to the parent beat
		public int Rows { get; set; }
		public int Columns { get; set; }
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

	}
}
