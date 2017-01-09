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
using Android.Graphics;
using BeatGrid;

namespace BeatGridAndroid
{
	// From http://www.appliedcodelog.com/2016/06/expandablelistview-in-xamarin-android.html
	public class SelectSoundExpandableListAdapter : BaseExpandableListAdapter
	{
		private Activity _context;
		private List<string> _listDataHeader; // header titles
																					// child data in format of header title, child title
		private Dictionary<string, List<string>> _listDataChild;
		private Dictionary<string, Sound> _groupChildPosToSound; // Key = "groupPosition,childPosition"; Value = Sound
		private SoundManager _soundManager;
		private HashSet<string> _initializedChildViews; // Use this to make sure we don't subscribe to Click events multiple times in GetChildView()

		private Typeface _fontAwesome;

		public SelectSoundExpandableListAdapter(Activity context, SoundManager soundManager)
		{
			_context = context;
			_fontAwesome = ((MainActivity)context).FontAwesome;

			_listDataHeader = new List<string>();
			_listDataChild = new Dictionary<string, List<string>>();
			_groupChildPosToSound = new Dictionary<string, Sound>();
			_initializedChildViews = new HashSet<string>();

			_soundManager = soundManager;

			var soundsGroupedByCategory = soundManager.AllSounds.GroupBy(s => s.Category);
			int groupIndex = 0;
			foreach (var group in soundsGroupedByCategory)
			{
				_listDataHeader.Add(group.Key);
				_listDataChild.Add(group.Key, new List<string>());

				int childIndex = 0;
				foreach(var sound in group)
				{
					_listDataChild[group.Key].Add(sound.LongName);
					_groupChildPosToSound.Add(GetGroupChildPosKey(groupIndex, childIndex), sound);
					childIndex++;
				}
				//_listDataChild.Add(group.Key, group.Select(g => g.LongName).ToList());
				groupIndex++;
			}
		}

		//for child item view
		public override Java.Lang.Object GetChild(int groupPosition, int childPosition)
		{
			return _listDataChild[_listDataHeader[groupPosition]][childPosition];
		}
		public override long GetChildId(int groupPosition, int childPosition)
		{
			return childPosition;
		}

		public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
		{
			convertView = convertView ?? _context.LayoutInflater.Inflate(Resource.Layout.SelectSoundExpandableListViewItem, null);

			string childText = (string)GetChild(groupPosition, childPosition);
			TextView txtListChild = (TextView)convertView.FindViewById(Resource.Id.SelectSoundItemTextView);
			txtListChild.Text = childText;

			Sound sound = _groupChildPosToSound[GetGroupChildPosKey(groupPosition, childPosition)];

			Button playButton = (Button)convertView.FindViewById(Resource.Id.PlaySoundButton);
			playButton.SetTypeface(_fontAwesome, TypefaceStyle.Normal);
			//playButton.SetOnClickListener(null);
			playButton.Click += delegate (object sender, EventArgs e)
			{
				//TODO: Play sound.
				//TODO: Create sound event args, event, and delegate.
				_soundManager.PlaySound(sound);
			};


			Button selectSoundButton = (Button)convertView.FindViewById(Resource.Id.SelectSoundButton);
			selectSoundButton.SetTypeface(_fontAwesome, TypefaceStyle.Normal);
			selectSoundButton.Click += delegate (object sender, EventArgs e)
			{
				//TODO: Select sound.
				//TODO: Create sound event args, event, and delegate.
			};


			return convertView;
		}
		public override int GetChildrenCount(int groupPosition)
		{
			return _listDataChild[_listDataHeader[groupPosition]].Count;
		}
		//For header view
		public override Java.Lang.Object GetGroup(int groupPosition)
		{
			return _listDataHeader[groupPosition];
		}
		public override int GroupCount
		{
			get
			{
				return _listDataHeader.Count;
			}
		}
		public override long GetGroupId(int groupPosition)
		{
			return groupPosition;
		}
		public override View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
		{
			string headerTitle = (string)GetGroup(groupPosition);

			convertView = convertView ?? _context.LayoutInflater.Inflate(Resource.Layout.SelectSoundExpandableListViewHeader, null);
			var expListViewHeader = (TextView)convertView.FindViewById(Resource.Id.ExpListViewHeader);
			expListViewHeader.Text = headerTitle;

			return convertView;
		}
		public override bool HasStableIds
		{
			get
			{
				return false;
			}
		}
		public override bool IsChildSelectable(int groupPosition, int childPosition)
		{
			return true;
		}

		class ViewHolderItem : Java.Lang.Object
		{
		}

		private string GetGroupChildPosKey(int groupPosition, int childPosition)
		{
			return $"{groupPosition},{childPosition}";
		}
	}
}