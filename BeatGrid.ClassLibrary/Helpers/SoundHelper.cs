using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatGrid
{
	public class SoundFileNames
	{
		public const string bell1 = "bell1.ogg";
		public const string bell2 = "bell2.ogg";
		public const string ch0 = "ch0.mp3";
		public const string clhat01 = "clhat01.ogg";
		public const string clhat02 = "clhat02.ogg";
		public const string clhat03 = "clhat03.ogg";
		public const string clhat04 = "clhat04.ogg";
		public const string crash1 = "crash1.ogg";
		public const string crash2 = "crash2.ogg";
		public const string crash3 = "crash3.ogg";
		public const string crash4 = "crash4.ogg";
		public const string crash5 = "crash5.ogg";
		public const string hightom1 = "hightom1.ogg";
		public const string hightom2 = "hightom2.ogg";
		public const string bd0000 = "bd0000.mp3";
		public const string kick01 = "kick01.ogg";
		public const string kick02 = "kick02.ogg";
		public const string kick03 = "kick03.ogg";
		public const string kick04 = "kick04.ogg";
		public const string lowtom1 = "lowtom1.ogg";
		public const string lowtom2 = "lowtom2.ogg";
		public const string midtom1 = "midtom1.ogg";
		public const string midtom2 = "midtom2.ogg";
		public const string midtom3 = "midtom3.ogg";
		public const string ophat01 = "ophat01.ogg";
		public const string ophat02 = "ophat02.ogg";
		public const string ophat03 = "ophat03.ogg";
		public const string ophat04 = "ophat04.ogg";
		public const string ride1 = "ride1.ogg";
		public const string ride2 = "ride2.ogg";
		public const string sidestick1 = "sidestick1.ogg";
		public const string sidestick2 = "sidestick2.ogg";
		public const string sidestick3 = "sidestick3.ogg";
		public const string sd0000 = "sd0000.mp3";
		public const string snare01 = "snare01.ogg";
		public const string snare02 = "snare02.ogg";
		public const string snare03 = "snare03.ogg";
		public const string snare04 = "snare04.ogg";
	}

	public class SoundHelper
	{
		public static List<Sound> GetAllSounds()
		{
			List<Sound> allSounds = new List<Sound>();

			//Sound(string fileName, string longName, string shortName)
			allSounds.Add(new Sound(SoundFileNames.bell1, "Bell 1", "BELL1"));
			allSounds.Add(new Sound(SoundFileNames.bell2, "Bell 2", "BELL2"));
			//TODO: Use constant for rest of them
			allSounds.Add(new Sound("ch0.mp3", "Closed Hi-hat 1", "CHH1"));
			allSounds.Add(new Sound("clhat01.ogg", "Closed Hi-hat 1", "CHH2"));
			allSounds.Add(new Sound("clhat02.ogg", "Closed Hi-hat 2", "CHH3"));
			allSounds.Add(new Sound("clhat03.ogg", "Closed Hi-hat 3", "CHH4"));
			allSounds.Add(new Sound("clhat04.ogg", "Closed Hi-hat 4", "CHH5"));
			allSounds.Add(new Sound("crash1.ogg", "Crash 1", "CR1"));
			allSounds.Add(new Sound("crash2.ogg", "Crash 2", "CR2"));
			allSounds.Add(new Sound("crash3.ogg", "Crash 3", "CR3"));
			allSounds.Add(new Sound("crash4.ogg", "Crash 4", "CR4"));
			allSounds.Add(new Sound("crash5.ogg", "Crash 5", "CR5"));
			allSounds.Add(new Sound("hightom1.ogg", "High-Tom 1", "HT1"));
			allSounds.Add(new Sound("hightom2.ogg", "High-Tom 2", "HT2"));
			allSounds.Add(new Sound("bd0000.mp3", "Kick 1", "KICK1"));
			allSounds.Add(new Sound("kick01.ogg", "Kick 2", "KICK2"));
			allSounds.Add(new Sound("kick02.ogg", "Kick 3", "KICK3"));
			allSounds.Add(new Sound("kick03.ogg", "Kick 4", "KICK4"));
			allSounds.Add(new Sound("kick04.ogg", "Kick 5", "KICK5"));
			allSounds.Add(new Sound("lowtom1.ogg", "Low-Tom 1", "LT1"));
			allSounds.Add(new Sound("lowtom2.ogg", "Low-Tom 2", "LT2"));
			allSounds.Add(new Sound("midtom1.ogg", "Mid-Tom 1", "MT1"));
			allSounds.Add(new Sound("midtom2.ogg", "Mid-Tom 2", "MT2"));
			allSounds.Add(new Sound("midtom3.ogg", "Mid-Tom 3", "MT3"));
			allSounds.Add(new Sound("ophat01.ogg", "Open Hi-hat 1", "OHH1"));
			allSounds.Add(new Sound("ophat02.ogg", "Open Hi-hat 2", "OHH2"));
			allSounds.Add(new Sound("ophat03.ogg", "Open Hi-hat 3", "OHH3"));
			allSounds.Add(new Sound("ophat04.ogg", "Open Hi-hat 4", "OHH4"));
			allSounds.Add(new Sound("ride1.ogg", "Ride 1", "RIDE1"));
			allSounds.Add(new Sound("ride2.ogg", "Ride 2", "RIDE2"));
			allSounds.Add(new Sound("sidestick1.ogg", "Side Stick 1", "SIDESTK1"));
			allSounds.Add(new Sound("sidestick2.ogg", "Side Stick 2", "SIDESTK2"));
			allSounds.Add(new Sound("sidestick3.ogg", "Side Stick 3", "SIDESTK3"));
			allSounds.Add(new Sound("sd0000.mp3", "Snare Drum 1", "SD1"));
			allSounds.Add(new Sound("snare01.ogg", "Snare Drum 2", "SD2"));
			allSounds.Add(new Sound("snare02.ogg", "Snare Drum 3", "SD3"));
			allSounds.Add(new Sound("snare03.ogg", "Snare Drum 4", "SD4"));
			allSounds.Add(new Sound("snare04.ogg", "Snare Drum 5", "SD5"));

			return allSounds;
		}

		public static List<Sound> GetDefaultSounds()
		{
			List<Sound> sounds = new List<Sound>();

			//Sound(string fileName, string longName, string shortName)
			sounds.Add(new Sound("clhat01.ogg", "Closed Hi-hat 1", "CHH2"));
			sounds.Add(new Sound("ophat01.ogg", "Open Hi-hat 1", "OHH1"));
			sounds.Add(new Sound(SoundFileNames.snare04, "Snare Drum 4", "SD4"));
			sounds.Add(new Sound("bd0000.mp3", "Kick 1", "KICK1"));
			sounds.Add(new Sound("ride1.ogg", "Ride 1", "RIDE1"));
			sounds.Add(new Sound("crash1.ogg", "Crash 1", "CR1"));
			sounds.Add(new Sound("hightom1.ogg", "High-Tom 1", "HT1"));
			sounds.Add(new Sound("midtom1.ogg", "Mid-Tom 1", "MT1"));
			sounds.Add(new Sound("lowtom1.ogg", "Low-Tom 1", "LT1"));
			sounds.Add(new Sound("sidestick1.ogg", "Side Stick 1", "SIDESTK1"));

			int extraRows = Constants.MAX_ACTIVE_SOUNDS - sounds.Count;
			for (int i = 0; i < extraRows; i++)
			{
				sounds.Add(new Sound());
			}

			return sounds;
		}
	}
}
