using System;

namespace BeatGrid.ViewModel
{
	public class CellEventArgs : EventArgs
	{
		public CellEventArgs() { }
		public CellEventArgs(Cell cell) { Cell = cell; }

		public Cell Cell { get; set; }
	}
	public delegate void CellTouchedEventHandler(object source, CellEventArgs args);
	public delegate void CellLongTouchedEventHandler(object source, CellEventArgs args);


	public class SoundEventArgs : EventArgs
	{
		public SoundEventArgs() { }
		public SoundEventArgs(Sound sound) { Sound = sound; }

		public Sound Sound { get; set; }
	}
	public delegate void SoundTouchedEventHandler(object source, SoundEventArgs args);
	public delegate void SoundLongTouchedEventHandler(object source, SoundEventArgs args);


	public class MeasureEventArgs : EventArgs
	{
		public MeasureEventArgs() { }
		public MeasureEventArgs(Measure measure) { Measure = measure; }

		public Measure Measure { get; set; }
	}

	public class BeatEventArgs : EventArgs
	{
		public BeatEventArgs() { }
		public BeatEventArgs(Beat beat) { Beat = beat; }

		public Beat Beat { get; set; }
	}

	public class OpenBeatEventArgs : EventArgs
	{
		public OpenBeatEventArgs() { }
		public OpenBeatEventArgs(int beatId) { Id = beatId; }

		public int Id { get; set; }
	}

	public delegate void ClearMeasureSelectedEventHandler(object source, EventArgs args);
	public delegate void DeleteMeasureSelectedEventHandler(object source, EventArgs args);
	public delegate void DeleteBeatSelectedEventHandler(object source, EventArgs args);

	public delegate void OpenBeatSelectedEventHandler(object source, BeatEventArgs args);
}
