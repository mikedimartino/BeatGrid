using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatGrid
{
	public class Cell
	{
		public Cell() { }
		public Cell(bool on = false)
		{
			On = on;
		}

		public bool On { get; set; }
		public int Row { get; set; }
		public int Column { get; set; }

		public string GetCoordinate()
		{
			return $"({Row},{Column})";
		}
	}
}
