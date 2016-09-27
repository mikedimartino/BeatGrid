namespace BeatGrid.ViewModel
{
	public interface IMainView
	{
		event CellTouchedEventHandler CellTouched;
		event CellLongTouchedEventHandler CellLongTouched;
		event SoundTouchedEventHandler SoundTouched;
		event SoundLongTouchedEventHandler SoundLongTouched;
		event ClearMeasureSelectedEventHandler ClearMeasureSelected;
		event DeleteMeasureSelectedEventHandler DeleteMeasureSelected;
		event DeleteBeatSelectedEventHandler DeleteBeatSelected;
		event OpenBeatSelectedEventHandler OpenBeatSelected;
	}
}
