namespace BeatGrid.ViewModel
{
	public interface IMainView
	{

		//TODO: Delete this. Unnecessary.
		event CellTouchedEventHandler CellTouched;
		event CellLongTouchedEventHandler CellLongTouched;
		event SoundTouchedEventHandler SoundTouched;
		event SoundLongTouchedEventHandler SoundLongTouched;
		event ClearMeasureSelectedEventHandler ClearMeasureSelected;
		event DeleteMeasureSelectedEventHandler DeleteMeasureSelected;
		event DeleteBeatSelectedEventHandler DeleteBeatSelected;
		//event OpenBeatSelectedEventHandler OpenBeatSelected;
	}
}
