﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppPassword.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PopupPageAjouter : Rg.Plugins.Popup.Pages.PopupPage
    {
		public PopupPageAjouter ()
		{
			InitializeComponent ();
            BindingContext = new AppPassword.ViewModels.PopupPageAjouterViewModel();
        }
	}
}