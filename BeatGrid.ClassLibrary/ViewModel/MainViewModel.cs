using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatGrid.ViewModel
{
	public class MainViewModel
	{
		private SQLiteProvider _provider;

		public MainViewModel(SQLiteProvider provider)
		{
			_provider = provider;
			var beat = _provider.GetAllBeats().FirstOrDefault();
			CurrentMeasure = beat?.Measures.First() ?? Measure.GetEmptyMeasure();
		}

		#region Public Properties
		public Measure CurrentMeasure { get; set; } // Maybe set raises event?
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
		}

		public void OpenBeat(int beatId)
		{
			OnBeatChanged(Beat.GetTestBeat());
			//TODO: Load beat from db 
		}
		#endregion
	}
}
