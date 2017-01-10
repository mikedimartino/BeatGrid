using System.Linq;

using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using BeatGrid.ViewModel;
using BeatGridAndroid.CustomViews;
using BeatGrid;

namespace BeatGridAndroid
{
	public class SoundLibraryDialogFragment : DialogFragment
	{
		MainViewModel _mvm;
		MainActivity _mainActivity;

		public SoundLibraryDialogFragment(MainViewModel mvm, SoundManager soundManager)
		{
			_mvm = mvm;
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
	}
}