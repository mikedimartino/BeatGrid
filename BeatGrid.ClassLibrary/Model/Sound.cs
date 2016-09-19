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
		public Sound(string name)
		{
			Name = name;
		}

		public string Name { get; set; }
		// TODO: path to audio file
	}
}
