using System;

using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;

namespace Cal.Playground.Views
{
	public class MultiView : ContentView
	{
		private enum TransitionDirection
		{
			Left,
			Right
		}

		public static BindableProperty SubViewsProperty = BindableProperty.Create<MultiView,IList<View>> (page => page.SubViews, new List<View> (), propertyChanged: (BindableObject bindable, IList<View> oldValue, IList<View> newValue) => {
			var view = bindable as MultiView;

		});

		public IList<View> SubViews {
			get{ return (IList<View>)GetValue (SubViewsProperty); }
			set{ SetValue (SubViewsProperty, value); }
		}

		public static readonly BindableProperty ViewIndexProperty =
			BindableProperty.Create<MultiView, int> (
				p => p.ViewIndex, default(int), propertyChanged: (bindable, oldValue, newValue) => {
				var view = bindable as MultiView;
					//use mod
				var idx = view.ViewIndex % view.SubViews.Count;
				if (view.SubViews.Count > 0 && idx >= 0) {

					view.TransitionTo (view.SubViews [idx], newValue > oldValue ? TransitionDirection.Left : TransitionDirection.Right);
				}

			});

		public int ViewIndex {
			get { return (int)GetValue (ViewIndexProperty); }
			set { SetValue (ViewIndexProperty, value); }
		}

		private AbsoluteLayout _layout;

		public MultiView ()
		{

			_layout = new AbsoluteLayout ();
			this.Content = _layout;

		}

		private  void TransitionTo (View next, TransitionDirection direction)
		{
			var previous = _layout.Children.FirstOrDefault ();
			var rightInvisble = _layout.Width;
			var leftInvisible = 0 - (_layout.Width);
			double fadeOutLocation;



			if (direction == TransitionDirection.Left) {
				_layout.Children.Add (next, new Point (rightInvisble, 0));
				fadeOutLocation = rightInvisble;
			} else {
				_layout.Children.Add (next, new Point (leftInvisible, 0));
				fadeOutLocation = leftInvisible;
			}
			next.LayoutTo (new Rectangle (0, 0, _layout.Width, _layout.Height), 250);

			if (previous != null)
				previous.LayoutTo (new Rectangle (fadeOutLocation, 0, 0, 0), 250);

			var toRemove = _layout.Children.Where (c => c != next).ToArray ();
			foreach (var item in toRemove) {
				_layout.Children.Remove (item);
			}


		}
	}
}


