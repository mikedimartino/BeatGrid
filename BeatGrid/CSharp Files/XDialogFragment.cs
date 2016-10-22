using System;

using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using BeatGrid;

namespace BeatGridAndroid
{
	public class XDialogFragment : DialogFragment
	{
		const string CLEAR_MEASURE = "Clear Measure";
		const string DELETE_MEASURE = "Delete Measure";
		const string DELETE_BEAT = "Delete Beat";

		private ListView _optionsList;

		public event XOptionSelectedEventHandler XOptionSelected;

		public override Dialog OnCreateDialog(Bundle savedInstanceState)
		{
			return base.OnCreateDialog(savedInstanceState);
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			//Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
			Dialog.SetTitle("Select Option:");

			var view = inflater.Inflate(Resource.Layout.XDialog, container, false);
			_optionsList = (ListView)view.FindViewById(Resource.Id.XOptionList);

			#region Using Buttons
			//var clearMeasureButton = view.FindViewById<Button>(Resource.Id.clear_measure_button);
			//clearMeasureButton.Click += delegate
			//{
			//	XOptionSelected?.Invoke(this, new XOptionEventArgs() { Option = XOption.ClearMeasure });
			//	Dismiss();
			//};

			//var deleteMeasureButton = view.FindViewById<Button>(Resource.Id.delete_measure_button);
			//deleteMeasureButton.Click += delegate
			//{
			//	XOptionSelected?.Invoke(this, new XOptionEventArgs() { Option = XOption.DeleteMeasure });
			//	Dismiss();
			//};

			//var deleteBeatButton = view.FindViewById<Button>(Resource.Id.delete_beat_button);
			//deleteBeatButton.Click += delegate
			//{
			//	XOptionSelected?.Invoke(this, new XOptionEventArgs() { Option = XOption.DeleteBeat });
			//	Dismiss();
			//};
			#endregion

			return view;
		}

		public override void OnActivityCreated(Bundle savedInstanceState)
		{
			base.OnActivityCreated(savedInstanceState);

			string[] options = new[] { CLEAR_MEASURE, DELETE_MEASURE, DELETE_BEAT };
			var adapter = new ArrayAdapter<string>(Activity, Resource.Layout.XOptionTextViewItem, options);
			_optionsList.Adapter = adapter;

			_optionsList.ItemClick += (sender, args) =>
			{
				XOption option = default(XOption);
				string optionName = (string)_optionsList.GetItemAtPosition(args.Position);
				switch (optionName)
				{
					case CLEAR_MEASURE: option = XOption.ClearMeasure; break;
					case DELETE_MEASURE: option = XOption.DeleteMeasure; break;
					case DELETE_BEAT: option = XOption.DeleteBeat; break;
				}
				XOptionSelected?.Invoke(this, new XOptionEventArgs(option));
				Dismiss();
			};
		}
	}

	public class XOptionEventArgs : EventArgs
	{
		public XOptionEventArgs(XOption option) { Option = option; }
				
		public XOption Option { get; set; }
	}

	public delegate void XOptionSelectedEventHandler(object source, XOptionEventArgs args);
}