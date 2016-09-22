namespace BeatGrid.ViewModel
{
	public interface IMainView
	{
		event CellTouchedEventHandler CellTouched;
		event CellLongTouchedEventHandler CellLongTouched;
		event SoundTouchedEventHandler SoundTouched;
		event SoundLongTouchedEventHandler SoundLongTouched;
	}
}
