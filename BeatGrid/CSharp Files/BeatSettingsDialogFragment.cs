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
	public class BeatSettingsDialogFragment : DialogFragment
	{
		public event BeatSelectedEventHandler BeatSelected;

		public override Dialog OnCreateDialog(Bundle savedInstanceState)
		{
			return base.OnCreateDialog(savedInstanceState);
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			Dialog.SetTitle("Beat Settings:");

			var view = inflater.Inflate(Resource.Layout.BeatSettingsDialog, container, false);
			return view;
		}

		public override void OnActivityCreated(Bundle savedInstanceState)
		{
			base.OnActivityCreated(savedInstanceState);
		}
	}
}