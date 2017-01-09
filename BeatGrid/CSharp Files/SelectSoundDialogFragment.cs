using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BeatGrid;

namespace BeatGridAndroid
{
	// Some of this code taken from http://www.appliedcodelog.com/2016/06/expandablelistview-in-xamarin-android.html
	public class SelectSoundDialogFragment : DialogFragment
	{
		SoundManager _soundManager;
		SelectSoundExpandableListAdapter _listAdapter;
		ExpandableListView _expListView;
		List<string> _listDataHeader;
		Dictionary<string, List<string>> _listDataChild;

		public SelectSoundDialogFragment(SoundManager soundManager)
		{
			_soundManager = soundManager;
		}

		public override Dialog OnCreateDialog(Bundle savedInstanceState)
		{
			return base.OnCreateDialog(savedInstanceState);
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			Dialog.SetTitle("Select Sound:");

			var view = inflater.Inflate(Resource.Layout.SelectSoundDialog, container, false);
			_expListView = (ExpandableListView)view.FindViewById(Resource.Id.SelectSoundExpListView);

			//GetTestData();
			//GetAllSoundData();

			//_listAdapter = new SelectSoundExpandableListAdapter(Activity, _listDataHeader, _listDataChild);
			_listAdapter = new SelectSoundExpandableListAdapter(Activity, _soundManager);
			_expListView.SetAdapter(_listAdapter);

			return view;
		}

		public override void OnActivityCreated(Bundle savedInstanceState)
		{
			base.OnActivityCreated(savedInstanceState);

			#region Event Listeners

			_expListView.ChildClick += delegate (object sender, ExpandableListView.ChildClickEventArgs e)
			{
				// TODO: Handle sound selection
				//Dismiss();
			};

			#region Commented out
			//// When opening group, close previously opened group:
			//_expListView.GroupExpand += delegate (object sender, ExpandableListView.GroupExpandEventArgs e)
			//{
			//	if(e.GroupPosition != _previousGroup)
			//	{
			//		_expListView.CollapseGroup(_previousGroup);
			//	}
			//	_previousGroup = e.GroupPosition;
			//};
			#endregion

			#endregion
		}

		private void GetTestData()
		{
			_listDataHeader = new List<string>();
			_listDataChild = new Dictionary<string, List<string>>();

			string hiHat = "Hi-Hat";
			string rideCymbal = "Ride Cymbal";
			string snare = "Snare";
			string kick = "Kick";

			// Headers
			_listDataHeader.AddRange(new[] { hiHat, rideCymbal, snare, kick });

			var hiHatList = new List<string> { "hh1", "hh2", "hh3" };
			var rideCymbalList = new List<string> { "ride1", "ride2", "ride3" };
			var snareList = new List<string> { "snare1", "snare2", "snare3" };
			var kickList = new List<string> { "kick1", "kick2", "kick3" };

			_listDataChild.Add(hiHat, hiHatList);
			_listDataChild.Add(rideCymbal, rideCymbalList);
			_listDataChild.Add(snare, snareList);
			_listDataChild.Add(kick, kickList);
		}

		private void GetAllSoundData()
		{
			_listDataHeader = new List<string>();
			_listDataChild = new Dictionary<string, List<string>>();

			var soundsGroupedByCategory = _soundManager.AllSounds.GroupBy(s => s.Category);
			foreach(var group in soundsGroupedByCategory)
			{
				_listDataHeader.Add(group.Key);
				_listDataChild.Add(group.Key, group.Select(g => g.LongName).ToList());
			}
		}
	}
}