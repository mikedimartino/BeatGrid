using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics;
using BeatGrid;
using BeatGrid.ViewModel;
using System.Collections.Generic;

namespace BeatGridAndroid
{
	[Activity(Label = "BeatGrid", MainLauncher = true, Icon = "@drawable/BeatGridLogo")]
	public class MainActivity : Activity, IMainView
	{
		private MainViewModel _mvm;

		private TableLayout _beatGridTable;
		private TableLayout.LayoutParams _rowParams;
		private TableRow.LayoutParams _cellParams;
		private TableRow.LayoutParams _soundNameParams;

		private Typeface _fontAwesome;
		
		private Button _homeButton; 
		private Button _saveButton;
		private Button _trashButton;
		private Button _settingsButton;
		private Button _previousButton;
		private Button _playPauseButton;
		private Button _nextButton;

		private Dictionary<string, Button> _cellButtons; 

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			_mvm = new MainViewModel(GetSQLiteProvider(), this);
			_mvm.CellChanged += OnCellChanged;

			_fontAwesome = Typeface.CreateFromAsset(Assets, "fontawesome-webfont.ttf");

			SetContentView(Resource.Layout.Main);
			InitLayout();


			_cellButtons = new Dictionary<string, Button>();

			DrawGrid(Measure.GetEmptyMeasure());
		}

		// Initialize buttons and other layout items
		private void InitLayout()
		{
			#region BeatGrid Table
			_beatGridTable = FindViewById<TableLayout>(Resource.Id.BeatGrid);

			// From http://stackoverflow.com/questions/2393847/how-can-i-get-an-android-tablelayout-to-fill-the-screen
			_rowParams = new TableLayout.LayoutParams(
					ViewGroup.LayoutParams.MatchParent,
					ViewGroup.LayoutParams.MatchParent,
					1.0f);

			_cellParams = new TableRow.LayoutParams(
					ViewGroup.LayoutParams.MatchParent,
					ViewGroup.LayoutParams.MatchParent,
					1.0f);
			_cellParams.Width = 0;
			_cellParams.SetMargins(5, 5, 5, 5);

			_soundNameParams = new TableRow.LayoutParams(
					ViewGroup.LayoutParams.MatchParent,
					ViewGroup.LayoutParams.MatchParent,
					2.0f);
			_soundNameParams.Width = 0;
			#endregion

			#region Top Bar Buttons
			_homeButton = FindViewById<Button>(Resource.Id.HomeButton);
			_saveButton = FindViewById<Button>(Resource.Id.SaveButton);
			_trashButton = FindViewById<Button>(Resource.Id.TrashButton);
			_settingsButton = FindViewById<Button>(Resource.Id.SettingsButton);
			_previousButton = FindViewById<Button>(Resource.Id.PreviousButton);
			_playPauseButton = FindViewById<Button>(Resource.Id.PlayPauseButton);
			_nextButton = FindViewById<Button>(Resource.Id.NextButton);

			_homeButton.SetTypeface(_fontAwesome, TypefaceStyle.Normal);
			_saveButton.SetTypeface(_fontAwesome, TypefaceStyle.Normal);
			_trashButton.SetTypeface(_fontAwesome, TypefaceStyle.Normal);
			_settingsButton.SetTypeface(_fontAwesome, TypefaceStyle.Normal);
			_previousButton.SetTypeface(_fontAwesome, TypefaceStyle.Normal);
			_playPauseButton.SetTypeface(_fontAwesome, TypefaceStyle.Normal);
			_nextButton.SetTypeface(_fontAwesome, TypefaceStyle.Normal);
			#endregion

			#region Main Menu Button
			//mainMenuButton = FindViewById<Button>(Resource.Id.MainMenuButton);
			//mainMenuButton.SetTypeface(fontAwesome, TypefaceStyle.Normal);
			#endregion
		}

		private void DrawGrid(Measure measure)
		{
			_cellButtons.Clear();

			int rows = measure.Cells.GetLength(0);
			int columns = measure.Cells.GetLength(1);

			for (int r = 0; r < rows; r++)
			{
				var row = new TableRow(this);

				// Sounds:
				Sound sound = new Sound($"Sound {r}");
				var soundName = new TextView(this);
				soundName.Text = sound.Name; // Decide on max length
				soundName.Gravity = GravityFlags.CenterVertical;
				soundName.SetPadding(10, 0, 0, 0);

				soundName.Click += (sender, eventArgs) => { OnSoundTouched(sound); };
				soundName.LongClick += (sender, eventArgs) => { OnSoundLongTouched(sound); };

				row.AddView(soundName, _soundNameParams);

				// Grid:
				for (int c = 0; c < columns; c++)
				{
					Cell cell = measure.Cells[r, c];

					var button = new Button(this);
					button.Click += (sender, eventArgs) => { OnCellTouched(cell); };
					button.LongClick += (sender, eventArgs) => { OnCellLongTouched(cell); };
					_cellButtons.Add(cell.GetCoordinate(), button);

					row.AddView(button, _cellParams);

					DrawCell(cell);
				}
				_beatGridTable.AddView(row, _rowParams);
			}
		}

		#region Event Handlers
		#region From ViewModel
		private void OnCellChanged(object source, CellEventArgs e)
		{
			DrawCell(e.Cell);
		}
		#endregion

		#region To ViewModel
		#region CellTouched
		public event CellTouchedEventHandler CellTouched;
		public void OnCellTouched(Cell cell)
		{
			CellTouched?.Invoke(this, new CellEventArgs() { Cell = cell });
		}
		#endregion
		#region CellLongTouched
		public event CellLongTouchedEventHandler CellLongTouched;
		public void OnCellLongTouched(Cell cell)
		{
			CellLongTouched?.Invoke(this, new CellEventArgs() { Cell = cell });
		}
		#endregion
		#region SoundTouched
		public event SoundTouchedEventHandler SoundTouched;
		public void OnSoundTouched(Sound Sound)
		{
			SoundTouched?.Invoke(this, new SoundEventArgs() { Sound = Sound });
		}
		#endregion
		#region SoundLongTouched
		public event SoundLongTouchedEventHandler SoundLongTouched;
		public void OnSoundLongTouched(Sound Sound)
		{
			SoundLongTouched?.Invoke(this, new SoundEventArgs() { Sound = Sound });
		}
		#endregion
		#endregion
		#endregion

		private void DrawCell(Cell cell)
		{
			int backgroundResId = cell.On ? Resource.Drawable.button_on : Resource.Drawable.button_off;
			Button button = _cellButtons[cell.GetCoordinate()];
			button.SetBackgroundResource(backgroundResId);
		}

		private SQLiteProvider GetSQLiteProvider()
		{
			// From http://stackoverflow.com/questions/25882837/sqlite-could-not-open-database-file
			string applicationFolderPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "BeatGridDb");

			// Create the folder path.
			System.IO.Directory.CreateDirectory(applicationFolderPath);

			string databaseFileName = System.IO.Path.Combine(applicationFolderPath, "BeatGrid.db");

			return new SQLiteProvider(databaseFileName);
		}

		public void TestSQLite()
		{
			try
			{
				// From http://stackoverflow.com/questions/25882837/sqlite-could-not-open-database-file
				string applicationFolderPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "BeatGridDb");

				// Create the folder path.
				System.IO.Directory.CreateDirectory(applicationFolderPath);

				string databaseFileName = System.IO.Path.Combine(applicationFolderPath, "BeatGrid.db");

				SQLiteProvider provider = new SQLiteProvider(databaseFileName);
				provider.SaveBeat(Beat.GetTestBeat());
				var beats = provider.GetAllBeats();
				var firstBeat = provider.GetBeat(beats.First().Id);
			}
			catch(Exception ex) { }
		}

	}
}

