using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatGrid
{
	public class Cell
	{
		public Cell(bool on = false)
		{
			On = on;
		}

		public bool On { get; set; }
	}
}
