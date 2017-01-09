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
		public ExpandableSoundCategoryView(Context context, string name, List<Sound> sounds) : base(context)
		{
			var test = context as MainActivity;

			Name = name;
			IsExpanded = false;

			LayoutParameters = new LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);

			AddView(new CategoryHeaderView(context, name));
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

			Click += CategoryHeaderView_Click;
		}

		private void CategoryHeaderView_Click(object sender, EventArgs e)
		{
			IsExpanded = !IsExpanded;
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
		TextView _nameTV;
		Button _listenBtn, _selectBtn;

		public SelectSoundView(Context context, Sound sound) : base(context) 
		{
			InitLayout(sound);
		}


		private void InitLayout(Sound sound)
		{
			_nameTV = new TextView(Context);
			_nameTV.Text = sound.LongName;
			_nameTV.LayoutParameters = new LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
			_nameTV.SetPadding(10, 25, 10, 25);
			_nameTV.SetBackgroundResource(Resource.Drawable.sound_border);

			_listenBtn = new Button(Context);
			_listenBtn.Text = "Listen";

			_selectBtn = new Button(Context);
			_selectBtn.Text = "Select";
		}
	}
}