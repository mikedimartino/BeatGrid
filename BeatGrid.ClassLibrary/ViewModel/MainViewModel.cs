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
		private IMainView _view;

		public MainViewModel(SQLiteProvider provider, IMainView view)
		{
			_provider = provider;

			_view = view;
			_view.CellTouched += OnCellTouch;
			_view.CellLongTouched += OnCellLongTouch;
			_view.SoundTouched += OnSoundTouch;
			_view.SoundLongTouched += OnSoundLongTouch;
			_view.ClearMeasureSelected += OnClearMeasure;
			_view.DeleteMeasureSelected += OnDeleteMeasure;
			_view.DeleteBeatSelected += OnDeleteBeat;
			_view.OpenBeatSelected += OnOpenBeat;

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


		public void OnCellTouch(object source, CellEventArgs e)
		{
			e.Cell.On = !e.Cell.On;
			OnCellChanged(e.Cell);
		}

		public void OnCellLongTouch(object source, CellEventArgs e)
		{
			//TODO: Implement
		}

		public void OnSoundTouch(object source, SoundEventArgs e)
		{
			//TODO: Implement
		}

		public void OnSoundLongTouch(object source, SoundEventArgs e)
		{
			//TODO: Implement
		}

		public void OnClearMeasure(object source, EventArgs e)
		{
			CurrentMeasure.Clear();
			OnMeasureChanged(CurrentMeasure);
		}

		public void OnDeleteMeasure(object source, EventArgs e)
		{
			//TODO: Implement
		}

		public void OnDeleteBeat(object source, EventArgs e)
		{
			//TODO: Implement
		}

		public void OnSaveClick()
		{
			//TODO: Implement
		}

		public void OnOpenBeat(object source, BeatEventArgs e)
		{
			int beatId = e.Id;
			OnBeatChanged(Beat.GetTestBeat());
			//TODO: Load beat from db and 
		}
		#endregion
	}
}
