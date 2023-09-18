﻿using AppPassword.Services;
using AppPassword.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppPassword
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            //DependencyService.Register<MockContactStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
