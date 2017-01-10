using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using BeatGrid.ViewModel;
using Java.Util.Concurrent.Atomic;
using BeatGridAndroid.CustomViews;
using BeatGrid;

namespace BeatGridAndroid
{
	public class SoundLibraryDialogFragment : DialogFragment
	{
		MainViewModel _mvm;
		SoundManager _soundManager;
		MainActivity _mainActivity;

		public SoundLibraryDialogFragment(MainViewModel mvm, SoundManager soundManager)
		{
			_mvm = mvm;
			_soundManager = soundManager;
			_mainActivity = Activity as MainActivity;
		}

		public override Dialog OnCreateDialog(Bundle savedInstanceState)
		{
			return base.OnCreateDialog(savedInstanceState);
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment

			Dialog.SetTitle("Select Sound:");

			LinearLayout ll = new LinearLayout(Activity);
			var llParams = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
			ll.LayoutParameters = llParams;
			ll.Orientation = Orientation.Vertical;

			var soundsGroupedByCategory = (Activity as MainActivity).AllSounds.GroupBy(s => s.Category);
			foreach (var group in soundsGroupedByCategory)
			{
				ll.AddView(new ExpandableSoundCategoryView(Activity, group.Key, group.ToList()));
			}

			ScrollView scrollView = new ScrollView(Activity);
			scrollView.AddView(ll);
			return scrollView;
		}

		private View CreateSelectSoundView()
		{
			LinearLayout selectSoundLL = new LinearLayout(Activity);
			var llParams = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
			selectSoundLL.LayoutParameters = llParams;
			selectSoundLL.Orientation = Orientation.Vertical;

			var soundsGroupedByCategory = _soundManager.AllSounds.GroupBy(s => s.Category);
			foreach (var group in soundsGroupedByCategory)
			{
				string category = group.Key;
				//TODO: Create category view
				RelativeLayout categoryRL = new RelativeLayout(Activity);
				var rlParams = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
				categoryRL.LayoutParameters = rlParams;


				//TODO: Create each of the individual sound view. (Make invisible).



				//_listDataHeader.Add(group.Key);
				//_listDataChild.Add(group.Key, group.Select(g => g.LongName).ToList());
			}


			return selectSoundLL;
		}

		RelativeLayout.LayoutParams categoryRLParams = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
		private View CreateSoundCategoryView(string categoryName)
		{
			RelativeLayout categoryRL = new RelativeLayout(Activity);
			categoryRL.LayoutParameters = categoryRLParams;
			categoryRL.Tag = false; // bool IsExpanded 


			return categoryRL;
		}
	}
}