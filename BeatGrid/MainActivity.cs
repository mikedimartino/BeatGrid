using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using BeatGrid.Model;

namespace BeatGrid
{
	[Activity(Label = "BeatGrid", MainLauncher = true, Icon = "@drawable/BeatGridLogo")]
	public class MainActivity : Activity
	{
		private TableLayout beatGridTable;
		private TableLayout.LayoutParams rowLp;
		private TableRow.LayoutParams cellLp;


		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			InitLayout();
			DrawBeatGrid(new Beat(8, 16));
		}

		private void InitLayout()
		{
			beatGridTable = FindViewById<TableLayout>(Resource.Id.BeatGrid);

			// From http://stackoverflow.com/questions/2393847/how-can-i-get-an-android-tablelayout-to-fill-the-screen
			rowLp = new TableLayout.LayoutParams(
					ViewGroup.LayoutParams.MatchParent,
					ViewGroup.LayoutParams.MatchParent,
					1.0f);

			cellLp = new TableRow.LayoutParams(
					ViewGroup.LayoutParams.MatchParent,
					ViewGroup.LayoutParams.MatchParent,
					1.0f);

			cellLp.SetMargins(5, 5, 5, 5);
		}

		private void DrawBeatGrid(Beat beat)
		{
			int rows = beat.Cells.GetLength(0);
			int columns = beat.Cells.GetLength(1);

			for (int r = 0; r < rows; r++)
			{
				var row = new TableRow(this);
				for (int c = 0; c < columns; c++)
				{
					var background = beat.Cells[r, c].IsActive ?
						Resource.Drawable.button_on :
						Resource.Drawable.button_off;

					var button = new Button(this);
					button.SetBackgroundResource(background);
					row.AddView(button, cellLp);
				}
				beatGridTable.AddView(row, rowLp);
			}
		}
	}
}

