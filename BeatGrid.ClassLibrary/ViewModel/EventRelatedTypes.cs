using System;

namespace BeatGrid.ViewModel
{
	public delegate void CellTouchedEventHandler(object source, CellEventArgs args);
	public class CellEventArgs : EventArgs
	{
		public Cell Cell { get; set; }
	}

	public delegate void CellLongTouchedEventHandler(object source, CellEventArgs args);
	public class SoundEventArgs : EventArgs
	{
		public Sound Sound { get; set; }
	}

	public delegate void SoundTouchedEventHandler(object source, SoundEventArgs args);
	public class MeasureEventArgs : EventArgs
	{
		public Measure Measure { get; set; }
	}

	public delegate void SoundLongTouchedEventHandler(object source, SoundEventArgs args);
	public class BeatEventArgs : EventArgs
	{
		public Beat Beat { get; set; }
	}
}
