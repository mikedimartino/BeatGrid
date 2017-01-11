using Android;
using Android.Media;
using BeatGrid;
using BeatGrid.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatGridAndroid
{
	public class SoundManager
	{
		// SoundPool builder info: http://www.northborder-software.com/getting_started_part26.html

		private MainActivity _mainActivity;
		private Dictionary<string, int> _soundFileNameToSoundPoolId;

		#region Sound Filename to Resource Id
		private static Dictionary<string, int> _soundFileNameToResourceId = new Dictionary<string, int>()
		{
			{ "bd0000.mp3", Resource.Raw.bd0000 },
			{ "bell1.ogg", Resource.Raw.bell1 },
			{ "bell2.ogg", Resource.Raw.bell2 },
			{ "ch0.mp3", Resource.Raw.ch0 },
			{ "clhat01.ogg", Resource.Raw.clhat01 },
			{ "clhat02.ogg", Resource.Raw.clhat02 },
			{ "clhat03.ogg", Resource.Raw.clhat03 },
			{ "clhat04.ogg", Resource.Raw.clhat04 },
			{ "crash1.ogg", Resource.Raw.crash1 },
			{ "crash2.ogg", Resource.Raw.crash2 },
			{ "crash3.ogg", Resource.Raw.crash3 },
			{ "crash4.ogg", Resource.Raw.crash4 },
			{ "crash5.ogg", Resource.Raw.crash5 },
			{ "hightom1.ogg", Resource.Raw.hightom1 },
			{ "hightom2.ogg", Resource.Raw.hightom2 },
			{ "kick01.ogg", Resource.Raw.kick01 },
			{ "kick02.ogg", Resource.Raw.kick02 },
			{ "kick03.ogg", Resource.Raw.kick03 },
			{ "kick04.ogg", Resource.Raw.kick04 },
			{ "lowtom1.ogg", Resource.Raw.lowtom1 },
			{ "lowtom2.ogg", Resource.Raw.lowtom2},
			{ "midtom1.ogg", Resource.Raw.midtom1 },
			{ "midtom2.ogg", Resource.Raw.midtom2 },
			{ "midtom3.ogg", Resource.Raw.midtom3 },
			{ "ophat01.ogg", Resource.Raw.ophat01 },
			{ "ophat02.ogg", Resource.Raw.ophat02 },
			{ "ophat03.ogg", Resource.Raw.ophat03 },
			{ "ophat04.ogg", Resource.Raw.ophat04 },
			{ "ride1.ogg", Resource.Raw.ride1 },
			{ "ride2.ogg", Resource.Raw.ride2 },
			{ "sd0000.mp3", Resource.Raw.sd0000 },
			{ "sidestick1.ogg", Resource.Raw.sidestick1 },
			{ "sidestick2.ogg", Resource.Raw.sidestick2 },
			{ "sidestick3.ogg", Resource.Raw.sidestick3 },
			{ "snare01.ogg", Resource.Raw.snare01 },
			{ "snare02.ogg", Resource.Raw.snare02 },
			{ "snare03.ogg", Resource.Raw.snare03 },
			{ "snare04.ogg", Resource.Raw.snare04 },
		};
		#endregion

		public SoundManager(MainActivity mainActivity)
		{
			_mainActivity = mainActivity;
			AllSounds = SoundHelper.AllSounds;
			_soundPool = new SoundPool(Constants.MAX_ACTIVE_SOUNDS, Stream.Music, 0);

			_soundFileNameToSoundPoolId = new Dictionary<string, int>();
			foreach (var sound in AllSounds)
			{
				int resourceId = _soundFileNameToResourceId[sound.FileName];
				int soundPoolId = _soundPool.Load(_mainActivity, resourceId, 1);
				_soundFileNameToSoundPoolId.Add(sound.FileName, soundPoolId);
			}
		}

		public List<Sound> AllSounds { get;set; }

		private SoundPool _soundPool;

		public void PlaySound(Sound sound)
		{
			if (!_soundFileNameToSoundPoolId.ContainsKey(sound.FileName)) return;
			_soundPool.Play(_soundFileNameToSoundPoolId[sound.FileName], 1, 1, 1, 0, 1);
		}

		public void PlaySounds(List<string> soundFileNames)
		{
			foreach(string fileName in soundFileNames)
			{
				if (!_soundFileNameToSoundPoolId.ContainsKey(fileName)) continue;
				_soundPool.Play(_soundFileNameToSoundPoolId[fileName], 1, 1, 1, 0, 1);
			}
		}
	}
}
