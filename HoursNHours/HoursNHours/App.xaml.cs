using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HoursNHours.Services;
using HoursNHours.Views;

namespace HoursNHours
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

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
