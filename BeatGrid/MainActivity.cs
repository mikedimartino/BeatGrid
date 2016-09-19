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

namespace BeatGridAndroid
{
	[Activity(Label = "BeatGrid", MainLauncher = true, Icon = "@drawable/BeatGridLogo")]
	public class MainActivity : Activity
	{
		MainViewModel _mvm;

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

		protected override void OnCreate(Bundle bundle)
		{
			_mvm = new MainViewModel();

			_fontAwesome = Typeface.CreateFromAsset(Assets, "fontawesome-webfont.ttf");

			base.OnCreate(bundle);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			InitLayout();

			//DrawGrid(Measure.GetRandomTestMeasure());
			DrawGrid(Measure.GetEmptyMeasure());

			//TestSQLite();
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
					button.SetBackgroundResource(GetCellBackgroundResId(cell));

					button.Click += (sender, eventArgs) => { OnCellTouched(button, cell); };
					button.LongClick += (sender, eventArgs) => { OnCellLongTouched(cell); };
						

					row.AddView(button, _cellParams);
				}
				_beatGridTable.AddView(row, _rowParams);
			}
		}

		#region Event Handlers
		private void OnCellTouched(View view, Cell cell)
		{
			_mvm.OnCellTouch(cell);
			view.SetBackgroundResource(GetCellBackgroundResId(cell));
		}

		private void OnCellLongTouched(Cell cell)
		{

		}

		private void OnSoundTouched(Sound sound)
		{

		}

		private void OnSoundLongTouched(Sound sound)
		{

		}
		#endregion

		private int GetCellBackgroundResId(Cell cell)
		{
			return cell.On ? Resource.Drawable.button_on : Resource.Drawable.button_off;
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

