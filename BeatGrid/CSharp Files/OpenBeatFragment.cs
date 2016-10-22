using System;
using System.Collections.Generic;
using System.Linq;

using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using BeatGrid;
using BeatGrid.ViewModel;

namespace BeatGridAndroid
{
	public class OpenBeatDialogFragment : DialogFragment
	{
		public Dictionary<string, int> BeatNameIds { get; set; }

		private ListView _beatNamesList;

		public event BeatSelectedEventHandler BeatSelected;

		public override Dialog OnCreateDialog(Bundle savedInstanceState)
		{
			return base.OnCreateDialog(savedInstanceState);
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			Dialog.SetTitle("Open Beat:");

			var view = inflater.Inflate(Resource.Layout.OpenBeatDialog, container, false);
			_beatNamesList = (ListView)view.FindViewById(Resource.Id.BeatList);

			return view;
		}

		public override void OnActivityCreated(Bundle savedInstanceState)
		{
			base.OnActivityCreated(savedInstanceState);

			// TEMP
			BeatNameIds = new Dictionary<string, int>()
			{
				{ "Beat 1", -1 },
				{ "Beat 2", -2 },
				{ "Beat 3", -3 },
				{ "Beat 4", -4 },
				{ "Beat 5", -5 },
				{ "Beat 6", -6 },
				{ "Beat 7", -7 },
				{ "Beat 8", -8 },
				{ "Beat 9", -9 },
			};
			// END TEMP

			string[] beatNames = BeatNameIds.Keys.ToArray();
			var adapter = new ArrayAdapter<string>(Activity, Resource.Layout.OpenBeatTextViewItem, beatNames);
			_beatNamesList.Adapter = adapter;

			_beatNamesList.ItemClick += (sender, args) =>
			{
				string beatName = (string)_beatNamesList.GetItemAtPosition(args.Position);
				BeatSelected?.Invoke(this, new OpenBeatEventArgs() { Id = BeatNameIds[beatName] });
				Dismiss();
			};
		}
	}
	public delegate void BeatSelectedEventHandler(object source, OpenBeatEventArgs args);
}