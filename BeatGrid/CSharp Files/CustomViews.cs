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
using Android.Graphics;

namespace BeatGridAndroid.CustomViews
{
	public class ExpandableSoundCategoryView : LinearLayout
	{
		CategoryHeaderView _categoryHeader;
		List<SelectSoundView> _soundsViews;

		public ExpandableSoundCategoryView(Context context, string name, List<Sound> sounds) : base(context)
		{
			Name = name;
			IsExpanded = false;

			LayoutParameters = new LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
			Orientation = Orientation.Vertical;

			_categoryHeader = new CategoryHeaderView(context, name);
			_categoryHeader.Click += _categoryHeader_Click;
			AddView(_categoryHeader);

			_soundsViews = new List<SelectSoundView>();
			foreach(var sound in sounds)
			{
				var soundView = new SelectSoundView(context, sound);
				_soundsViews.Add(soundView);
				AddView(soundView);
			}

		}

		private void _categoryHeader_Click(object sender, EventArgs e)
		{
			IsExpanded = !IsExpanded;
			_categoryHeader.IsExpanded = IsExpanded;
			var childVisibility = IsExpanded ? ViewStates.Visible : ViewStates.Gone; // invisible until category is expanded
			foreach(var soundView in _soundsViews)
			{
				soundView.Visibility = childVisibility;
			}
		}

		public string Name { get; set; }
		public bool IsExpanded { get; set; }
	}

	public class CategoryHeaderView : RelativeLayout
	{
		TextView _arrowTV;
		TextView _nameTV;

		public CategoryHeaderView(Context context, string name) : base(context)
		{
			LayoutParameters = new LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);

			_arrowTV = new TextView(context);
			_arrowTV.Typeface = ((MainActivity)context).FontAwesome;
			_arrowTV.TextSize = 30;
			var arrowParams = new LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
			arrowParams.AddRule(LayoutRules.AlignParentLeft);
			arrowParams.AddRule(LayoutRules.CenterVertical);
			_arrowTV.LayoutParameters = arrowParams;
			_arrowTV.SetPadding(20, 0, 20, 0);
			_arrowTV.Id = GenerateViewId();
			_arrowTV.SetTextColor(Color.ParseColor(Resources.GetString(Resource.Color.whitesmoke)));

			_nameTV = new TextView(context);
			_nameTV.TextSize = 30;
			var nameParams = new LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
			nameParams.AddRule(LayoutRules.RightOf, _arrowTV.Id);
			_nameTV.LayoutParameters = nameParams;
			_nameTV.SetPadding(10, 25, 10, 25);
			_nameTV.SetTextColor(Color.ParseColor(Resources.GetString(Resource.Color.whitesmoke)));
			SetBackgroundResource(Resource.Drawable.sound_category_border);

			Name = name;
			IsExpanded = false;

			AddView(_arrowTV);
			AddView(_nameTV);
		}

		public string Name
		{
			get { return _nameTV.Text; }
			set { _nameTV.Text = value; }
		}

		private bool _IsExpanded = false;
		public bool IsExpanded
		{
			get { return _IsExpanded; }
			set
			{
				_IsExpanded = value;
				// set up / down arrow
				int arrowResId = _IsExpanded ? Resource.String.angle_up : Resource.String.angle_down;
				_arrowTV.Text = Context.Resources.GetString(arrowResId);
				
			}
		}
	}


	public class SelectSoundView : RelativeLayout
	{
		Sound _sound;
		TextView _nameTV;
		Button _listenBtn, _selectBtn;

		public SelectSoundView(Context context, Sound sound) : base(context) 
		{
			_sound = sound;

			LayoutParameters = new LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
			Visibility = ViewStates.Gone; // invisible until category is expanded
			SetBackgroundResource(Resource.Drawable.sound_border);

			_nameTV = new TextView(Context);
			_nameTV.Text = sound.LongName;
			_nameTV.TextSize = 30;
			_nameTV.SetTextColor(Color.ParseColor(Resources.GetString(Resource.Color.black)));
			var nameParams = new LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
			nameParams.AddRule(LayoutRules.AlignParentLeft);
			nameParams.AddRule(LayoutRules.CenterVertical);
			_nameTV.LayoutParameters = nameParams;
			_nameTV.SetPadding(20, 25, 0, 25);

			_selectBtn = new Button(Context);
			_selectBtn.Text = "Select";
			_selectBtn.TextSize = 25;
			_selectBtn.Id = GenerateViewId();
			var selectParams = new LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
			selectParams.AddRule(LayoutRules.AlignParentRight);
			selectParams.AddRule(LayoutRules.CenterVertical);
			_selectBtn.LayoutParameters = selectParams;

			_listenBtn = new Button(Context);
			_listenBtn.Text = "Listen";
			_listenBtn.TextSize = 25;
			var listenParams = new LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
			listenParams.AddRule(LayoutRules.LeftOf, _selectBtn.Id);
			listenParams.AddRule(LayoutRules.CenterVertical);
			_listenBtn.LayoutParameters = listenParams;
			_listenBtn.Click += _listenBtn_Click;


			AddView(_nameTV);
			AddView(_selectBtn);
			AddView(_listenBtn);
		}

		private void _listenBtn_Click(object sender, EventArgs e)
		{
			(Context as MainActivity).SoundManager.PlaySound(_sound);
		}
	}
}