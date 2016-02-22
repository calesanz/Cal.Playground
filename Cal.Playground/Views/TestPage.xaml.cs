using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.ComponentModel;

namespace Cal.Playground.Views
{

	public partial class TestPage : ContentPage
	{

		public TestPage ()
		{
		
			InitializeComponent ();

		}

		private void doNext (object sender, EventArgs eargs)
		{
			multiView.ViewIndex++;
		}

		private void doPrev (object sender, EventArgs eargs)
		{
			multiView.ViewIndex--;
		}
	}


}

