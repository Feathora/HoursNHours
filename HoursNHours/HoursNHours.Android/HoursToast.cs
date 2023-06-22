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
using HoursNHours.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(HoursToast))]
namespace HoursNHours.Droid
{
    public class HoursToast : IHoursToast
    {
        public void ShowToast(string text)
        {
            Toast.MakeText(Xamarin.Forms.Forms.Context, text, ToastLength.Short).Show();
        }
    }
}