using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatGrid
{
	public class Sound
	{
		public Sound()
		{
			FileName = string.Empty;
			LongName = "(empty)";
			ShortName = "(empty)";
			Category = SoundCategories.Other;
		}
		public Sound(string fileName, string longName, string shortName, string category = SoundCategories.Other)
		{
			FileName = fileName;
			LongName = longName;
			ShortName = shortName;
			Category = category;
		}

		public string FileName { get; set; }
		public string LongName { get; set; }
		public string ShortName { get; set; }
		public string Category { get; set; }
	}
}
