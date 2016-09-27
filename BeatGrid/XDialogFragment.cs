using System;

using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using BeatGrid;

namespace BeatGridAndroid
{
	public class XOptionEventArgs : EventArgs
	{
		public XOption Option { get; set; }
	}
	public delegate void XOptionSelectedEventHandler(object source, XOptionEventArgs args);

	public class XDialogFragment : DialogFragment
	{
		public event XOptionSelectedEventHandler XOptionSelected;

		public override Dialog OnCreateDialog(Bundle savedInstanceState)
		{
			return base.OnCreateDialog(savedInstanceState);
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			Dialog.Window.RequestFeature(WindowFeatures.NoTitle);

			var view = inflater.Inflate(Resource.Layout.XDialog, container, false);
				
			var clearMeasureButton = view.FindViewById<Button>(Resource.Id.clear_measure_button);
			clearMeasureButton.Click += delegate
			{
				XOptionSelected?.Invoke(this, new XOptionEventArgs() { Option = XOption.ClearMeasure });
				Dismiss();
			};

			var deleteMeasureButton = view.FindViewById<Button>(Resource.Id.delete_measure_button);
			deleteMeasureButton.Click += delegate
			{
				XOptionSelected?.Invoke(this, new XOptionEventArgs() { Option = XOption.DeleteMeasure });
				Dismiss();
			};

			var deleteBeatButton = view.FindViewById<Button>(Resource.Id.delete_beat_button);
			deleteBeatButton.Click += delegate
			{
				XOptionSelected?.Invoke(this, new XOptionEventArgs() { Option = XOption.DeleteBeat });
				Dismiss();
			};

			return view;
		}
	}
}