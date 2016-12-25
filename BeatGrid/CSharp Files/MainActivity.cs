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
using Android.Media;

namespace BeatGridAndroid
{
	[Activity(Label = "BeatGrid", MainLauncher = true, Icon = "@drawable/BeatGridLogo")]
	public class MainActivity : Activity
	{
		private MainViewModel _mvm;
		private SoundManager _soundManager;

		#region UI Related
		private TableLayout _beatGridTable;
		private TableLayout.LayoutParams _rowParams;
		private TableRow.LayoutParams _cellParams;
		private TableRow.LayoutParams _soundNameParams;

		private Typeface _fontAwesome;
		
		private Button _openButton; 
		private Button _saveButton;
		private Button _xButton;
		//private Button _trashButton;
		private Button _settingsButton;
		private Button _previousButton;
		private Button _playPauseButton;
		private Button _nextButton;

		private Dictionary<string, Button> _cellButtons;
		#endregion

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			Window.RequestFeature(WindowFeatures.NoTitle);

			_mvm = new MainViewModel(GetSQLiteProvider());

			_mvm.CellChanged += OnCellChanged;
			_mvm.MeasureChanged += OnMeasureChanged;
			_mvm.BeatChanged += OnBeatChanged;
			_mvm.PlaySoundsList += OnPlaySounds;

			_fontAwesome = Typeface.CreateFromAsset(Assets, "fontawesome-webfont.ttf");

			SetContentView(Resource.Layout.Main);
			InitLayout();

			_cellButtons = new Dictionary<string, Button>();

			_soundManager = new SoundManager(this);

			//InitSoundPool();
			DrawMeasure(Measure.GetEmptyMeasure());

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
			// Maybe just call MVM directly instead of these events?
			_saveButton = FindViewById<Button>(Resource.Id.SaveButton);
			_saveButton.SetTypeface(_fontAwesome, TypefaceStyle.Normal);
			_saveButton.Click += OnSaveBeatClicked;

			_settingsButton = FindViewById<Button>(Resource.Id.SettingsButton);
			_settingsButton.SetTypeface(_fontAwesome, TypefaceStyle.Normal);
			_settingsButton.Click += OnSettingsClicked;

			_openButton = FindViewById<Button>(Resource.Id.OpenButton);
			_openButton.SetTypeface(_fontAwesome, TypefaceStyle.Normal);
			_openButton.Click += OnOpenBeatClicked;

			_xButton = FindViewById<Button>(Resource.Id.XButton);
			_xButton.SetTypeface(_fontAwesome, TypefaceStyle.Normal);
			_xButton.Click += OnXClicked;

			_previousButton = FindViewById<Button>(Resource.Id.PreviousButton);
			_previousButton.SetTypeface(_fontAwesome, TypefaceStyle.Normal);

			_playPauseButton = FindViewById<Button>(Resource.Id.PlayPauseButton);
			_playPauseButton.SetTypeface(_fontAwesome, TypefaceStyle.Normal);
			_playPauseButton.Click += OnPlayPauseClicked;

			_nextButton = FindViewById<Button>(Resource.Id.NextButton);
			_nextButton.SetTypeface(_fontAwesome, TypefaceStyle.Normal);

			#endregion

		}

		private void DrawMeasure(Measure measure) //TODO: Change to DrawBeat(Beat beat, int measure)
		{
			_cellButtons.Clear();
			_beatGridTable.RemoveAllViews();

			int rows = Constants.MAX_ACTIVE_SOUNDS;
			int columns = measure.Cells.GetLength(1);

			for (int r = 0; r < rows; r++)
			{
				var row = new TableRow(this);

				Sound sound = _mvm.ActiveBeat.Sounds[r];

				var soundName = new TextView(this);
				soundName.Text = sound.ShortName;
				soundName.Gravity = GravityFlags.CenterVertical;
				soundName.SetPadding(10, 0, 0, 0);

				soundName.Click += (sender, eventArgs) => { OnSoundClicked(sound); };
				soundName.LongClick += (sender, eventArgs) => { OnSoundLongClicked(sound); };

				row.AddView(soundName, _soundNameParams);

				// Grid:
				for (int c = 0; c < columns; c++)
				{
					Cell cell = measure.Cells[r, c];

					var button = new Button(this);
					button.Click += (sender, eventArgs) => { OnCellClicked(cell); };

					
					_cellButtons.Add(cell.GetCoordinate(), button);

					row.AddView(button, _cellParams);

					DrawCell(cell);
				}
				_beatGridTable.AddView(row, _rowParams);
			}
		}

		#region Event Handlers
		private void OnCellChanged(object source, CellEventArgs e)
		{
			DrawCell(e.Cell);
		}

		private void OnMeasureChanged(object source, MeasureEventArgs e)
		{
			foreach (var cell in e.Measure.Cells) DrawCell(cell);
		}

		private void OnBeatChanged(object source, BeatEventArgs e)
		{
			OnMeasureChanged(this, new MeasureEventArgs(e.Beat.CurrentMeasure));
			//TODO: Handle other stuff
		}

		private void OnCellClicked(Cell cell)
		{
			_mvm.ToggleCell(cell);
		}

		private void OnSoundClicked(Sound sound)
		{
			//TODO: Implement
			_soundManager.PlaySound(sound);
		}

		private void OnSoundLongClicked(Sound sound)
		{
			//TODO: Implement
			var transaction = FragmentManager.BeginTransaction();
			var selectSoundDialog = new SelectSoundDialogFragment();
			selectSoundDialog.Show(transaction, "select_sound_dialog_fragment");
		}

		private void OnPlaySounds(object source, PlaySoundsListEventArgs e)
		{
			_soundManager.PlaySounds(e.SoundFileNames);
		}

		#region XClicked
		public void OnXClicked(object sender, EventArgs e)
		{
			var transaction = FragmentManager.BeginTransaction();
			var xDialog = new XDialogFragment();
			xDialog.XOptionSelected += OnXSelectionMade;
			xDialog.Show(transaction, "x_dialog_fragment");
		}

		public void OnXSelectionMade(object source, XOptionEventArgs e)
		{
			switch (e.Option)
			{
				case XOption.ClearMeasure:
					_mvm.ClearCurrentMeasure();
					break;
				case XOption.DeleteMeasure:
					_mvm.DeleteCurrentMeasure();
					break;
				case XOption.DeleteBeat:
					_mvm.DeleteCurrentBeat();
					break;
			}
		}
		#endregion


		#region OpenBeat
		/// <summary>
		/// Called when the open beat button is clicked.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void OnOpenBeatClicked(object sender, EventArgs e)
		{
			var transaction = FragmentManager.BeginTransaction();
			var openBeatDialog = new OpenBeatDialogFragment();
			openBeatDialog.BeatSelected += OnOpenBeatSelected;
			openBeatDialog.Show(transaction, "open_beat_dialog_fragment");
		}

		/// <summary>
		/// Called when user has selected a beat in the open beat dialog.
		/// </summary>
		/// <param name="source"></param>
		/// <param name="e"></param>
		public void OnOpenBeatSelected(object source, OpenBeatEventArgs e)
		{
			_mvm.OpenBeat(e.Id);
			//TODO: Show prompt if unsaved changes.
			//TODO: Open beat.
		}
		#endregion

		#region SaveBeat
		public void OnSaveBeatClicked(object sender, EventArgs e)
		{
			_mvm.OnSaveClick();
			//TestSoundPool();
		}
		#endregion

		#region Settings
		public void OnSettingsClicked(object sender, EventArgs e)
		{
			var transaction = FragmentManager.BeginTransaction();
			var beatSettingsDialog = new BeatSettingsDialogFragment();
			beatSettingsDialog.Show(transaction, "beat_settings_dialog_fragment");
		}
		#endregion

		public void OnPlayPauseClicked(object sender, EventArgs e)
		{
			_mvm.PlayPauseBeat();
		}

		#endregion

		// Assumes that the cell coordinate will map to an existing position in the measure
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
			catch(Exception) { }
		}

		SoundPool soundPool;
		int soundId1;
		int soundId2;
		int soundId3;
		int soundId4;

		public void TestSoundPool()
		{
			soundPool.Play(soundId1, 1, 1, 0, 0, 1);
		}

		public void InitSoundPool()
		{
			soundPool = new SoundPool(8, Stream.Music, 0);
			soundId1 = soundPool.Load(this, Resource.Raw.clhat01, 1);
			soundId2 = soundPool.Load(this, Resource.Raw.ride1, 1);
			soundId3 = soundPool.Load(this, Resource.Raw.snare01, 1);
			soundId4 = soundPool.Load(this, Resource.Raw.kick01, 1);
		}


	}
}

