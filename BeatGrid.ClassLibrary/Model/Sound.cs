using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatGrid
{
	public class Sound
	{
		public Sound() { }
		public Sound(string name, int id)
		{
			Name = name;
			Id = id;
		}

		public string Name { get; set; }
		public int Id { get; set; }
	}
}
