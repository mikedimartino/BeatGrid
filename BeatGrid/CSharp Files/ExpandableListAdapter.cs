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

namespace BeatGridAndroid
{
	// From http://www.appliedcodelog.com/2016/06/expandablelistview-in-xamarin-android.html
	public class ExpandableListAdapter : BaseExpandableListAdapter
	{
		private Activity _context;
		private List<string> _listDataHeader; // header titles
																					// child data in format of header title, child title
		private Dictionary<string, List<string>> _listDataChild;

		private Typeface _fontAwesome;

		public ExpandableListAdapter(Activity context, List<string> listDataHeader, Dictionary<String, List<string>> listChildData)
		{
			_context = context;
			_listDataHeader = listDataHeader;
			_listDataChild = listChildData;

			_fontAwesome = Typeface.CreateFromAsset(context.Assets, "fontawesome-webfont.ttf");
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
			string childText = (string)GetChild(groupPosition, childPosition);
			convertView = convertView ?? _context.LayoutInflater.Inflate(Resource.Layout.ExpandableListViewItem, null);

			TextView txtListChild = (TextView)convertView.FindViewById(Resource.Id.SelectSoundItemTextView);
			txtListChild.Text = childText;

			Button playButton = (Button)convertView.FindViewById(Resource.Id.PlaySoundButton);
			playButton.SetTypeface(_fontAwesome, TypefaceStyle.Normal);
			playButton.Click += delegate (object sender, EventArgs e)
			{
				//TODO: Play sound.
				//TODO: Create sound event args, event, and delegate.
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

			convertView = convertView ?? _context.LayoutInflater.Inflate(Resource.Layout.ExpandableListViewHeader, null);
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
	}
}