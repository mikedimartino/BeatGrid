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
		MainViewModel _mvm;
		SeekBar _tempoSeekBar;
		TextView _tempoValueTextView;

		public BeatSettingsDialogFragment(MainViewModel mvm)
		{
			_mvm = mvm;
		}

		public override Dialog OnCreateDialog(Bundle savedInstanceState)
		{
			return base.OnCreateDialog(savedInstanceState);
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			Dialog.SetTitle("Beat Settings:");

			var view = inflater.Inflate(Resource.Layout.BeatSettingsDialog, container, false);

			_tempoSeekBar = view.FindViewById<SeekBar>(Resource.Id.BeatSettingsTempoSeekBar);
			_tempoSeekBar.Max = Constants.MAX_TEMPO;
			_tempoSeekBar.Progress = 100;
			_tempoSeekBar.ProgressChanged += _tempoSeekBar_ProgressChanged;

			_tempoValueTextView = view.FindViewById<TextView>(Resource.Id.BeatSettingsTempoValueTextView);
			_tempoValueTextView.Text = _tempoSeekBar.Progress.ToString();

			return view;
		}

		private void _tempoSeekBar_ProgressChanged(object sender, SeekBar.ProgressChangedEventArgs e)
		{
			_mvm.CurrentBeat.Tempo = e.Progress;
			_tempoValueTextView.Text = e.Progress.ToString();
		}

		public override void OnActivityCreated(Bundle savedInstanceState)
		{
			base.OnActivityCreated(savedInstanceState);
		}
	}
}