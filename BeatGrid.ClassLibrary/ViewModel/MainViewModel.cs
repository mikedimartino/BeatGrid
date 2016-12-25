using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeatGrid.ViewModel
{
	public class MainViewModel
	{
		private SQLiteProvider _provider;

		public MainViewModel(SQLiteProvider provider)
		{
			_provider = provider;
			//var beat = _provider.GetAllBeats().FirstOrDefault();
			ActiveBeat = Beat.GetTestBeat();
			CurrentMeasure = ActiveBeat?.Measures.FirstOrDefault() ?? Measure.GetEmptyMeasure();
		}

		#region Public Properties
		public Beat ActiveBeat { get; set; }
		public Measure CurrentMeasure { get; set; }
		#endregion

		#region UI Event Handling

		#region CellChanged
		public delegate void CellChangedEventHandler(object source, CellEventArgs args);

		public event CellChangedEventHandler CellChanged;

		public void OnCellChanged(Cell cell)
		{
			CellChanged?.Invoke(this, new CellEventArgs(cell));
		}
		#endregion

		#region SoundChanged
		public delegate void SoundChangedEventHandler(object source, SoundEventArgs args);

		public event SoundChangedEventHandler SoundChanged;

		public void OnSoundChanged(Sound sound)
		{
			SoundChanged?.Invoke(this, new SoundEventArgs() { Sound = sound });
		}
		#endregion

		#region MeasureChanged
		public delegate void MeasureChangedEventHandler(object source, MeasureEventArgs args);

		public event MeasureChangedEventHandler MeasureChanged;

		public void OnMeasureChanged(Measure measure)
		{
			MeasureChanged?.Invoke(this, new MeasureEventArgs(measure));
		}
		#endregion

		#region BeatChanged
		public delegate void BeatChangedEventHandler(object source, BeatEventArgs args);

		public event BeatChangedEventHandler BeatChanged;

		public void OnBeatChanged(Beat beat)
		{
			BeatChanged?.Invoke(this, new BeatEventArgs() { Beat = beat });
		}
		#endregion

		#region BeatChanged
		public delegate void PlaySoundsListEventHandler(object source, PlaySoundsListEventArgs args);

		public event PlaySoundsListEventHandler PlaySoundsList;

		public void OnPlaySoundsList(List<string> soundFileNames)
		{
			PlaySoundsList?.Invoke(this, new PlaySoundsListEventArgs() { SoundFileNames = soundFileNames });
		}
		#endregion

		public void PlaySound(string soundFileName)
		{
			OnPlaySoundsList(new List<string>() { soundFileName });
		}

		public void PlaySounds(List<string> soundFileNames)
		{
			OnPlaySoundsList(soundFileNames);
		}

		bool isPlaying = false;
		public void PlayPauseBeat()
		{
			//CurrentMeasure
			if (isPlaying) PauseBeat();
			else PlayBeat();
		}

		public async void PlayBeat()
		{
			isPlaying = true;
			// Test:
			var testSounds = new List<string> { SoundFileNames.bell1, SoundFileNames.snare01, SoundFileNames.bd0000 };
			int intervalBetweenBeatsMs = 4000;
			int playbackIntervalMs = intervalBetweenBeatsMs / CurrentMeasure.SubdivisionsPerBeat;
			while(isPlaying)
			{
				PlaySounds(testSounds);
				await Task.Delay(playbackIntervalMs);
			}
			
		}

		public void PauseBeat()
		{
			isPlaying = false;
		}

		public void ToggleCell(Cell cell)
		{
			cell.On = !cell.On;
			OnCellChanged(cell);
		}

		public void ClearCurrentMeasure()
		{
			CurrentMeasure.Clear();
			OnMeasureChanged(CurrentMeasure);
		}

		public void DeleteCurrentMeasure()
		{
			//TODO: Implement
		}

		public void DeleteCurrentBeat()
		{
			//TODO: Implement
		}

		public void OnSaveClick()
		{
			//TODO: Implement
			//TEMP:
			var sounds = new List<string>() { "snare01.ogg", "crash1.ogg" };
			PlaySounds(sounds);
		}

		public void OpenBeat(int beatId)
		{
			OnBeatChanged(Beat.GetTestBeat());
			//TODO: Load beat from db 
		}
		#endregion
	}
}
