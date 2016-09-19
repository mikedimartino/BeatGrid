using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatGrid.ViewModel
{
	public class MainViewModel
	{
		public void OnCellTouch(Cell cell)
		{
			cell.On = !cell.On;
		}
	}
}
