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

		public SoundLibraryDialogFragment(MainViewModel mvm, SoundManager soundManager)
		{
			_mvm = mvm;
			_soundManager = soundManager;
		}

		public override Dialog OnCreateDialog(Bundle savedInstanceState)
		{
			return base.OnCreateDialog(savedInstanceState);
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			// return inflater.Inflate(Resource.Layout.YourFragment, container, false);

			LinearLayout ll = new LinearLayout(Activity);
			var llParams = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
			ll.LayoutParameters = llParams;
			ll.Orientation = Orientation.Vertical;

			for (int i=0; i<5; i++)
			{
				ll.AddView(new ExpandableSoundCategoryView(Activity, "Sound Category " + i, new List<Sound>()));

				//ll.AddView(borderView);

				//RelativeLayout rl = new RelativeLayout(Activity);
				//var rlParams = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
				//rl.LayoutParameters = rlParams;

				//TextView soundNameTV = new TextView(Activity);
				//soundNameTV.Text = "THIS IS A TEST!";
				//soundNameTV.TextSize = 20;
				//var soundNameParams = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
				//soundNameParams.AddRule(LayoutRules.AlignParentLeft);
				//soundNameTV.LayoutParameters = soundNameParams;

				//Button selectButton = new Button(Activity);
				//selectButton.Text = "Select";
				//selectButton.Id = View.GenerateViewId();
				//var selectButtonParams = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
				//selectButtonParams.AddRule(LayoutRules.AlignParentRight);

				//selectButton.LayoutParameters = selectButtonParams;

				//Button playButton = new Button(Activity);
				//playButton.Text = "Listen";
				//var playButtonParams = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
				//playButtonParams.AddRule(LayoutRules.LeftOf, selectButton.Id);
				//playButton.LayoutParameters = playButtonParams;

				//rl.AddView(soundNameTV);
				//rl.AddView(playButton);
				//rl.AddView(selectButton);

				//ll.AddView(rl);
			}

			

			return ll;

			//return base.OnCreateView(inflater, container, savedInstanceState);
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