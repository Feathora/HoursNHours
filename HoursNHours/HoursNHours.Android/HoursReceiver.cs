using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HoursNHours.Models;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace HoursNHours.Droid
{
    [BroadcastReceiver]
    public class HoursReceiver : BroadcastReceiver
    {
        private string TodayString => DateTime.Now.ToString("d", CultureInfo.InvariantCulture);

        public override void OnReceive(Context context, Intent intent)
        {
            var Days = JsonConvert.DeserializeObject<List<Day>>(Preferences.Get("Days", "[]"));
            var Today = Days.SingleOrDefault(d => d.Date == TodayString);
            if (Today == null)
            {
                Today = new Day { Date = TodayString };
                Days.Add(Today);
            }

            if (intent.Action == "V++")
            {
                Today.VPlusPlusHours = 8;
                Today.HKUHours = 0;
            }
            else if(intent.Action == "HKU")
            {
                Today.VPlusPlusHours = 0;
                Today.HKUHours = 8;
            }

            Preferences.Set("Days", JsonConvert.SerializeObject(Days));

            ((NotificationManager)context.GetSystemService(Context.NotificationService)).Cancel(1);

            Toast.MakeText(context, $"Worked on {intent.Action}", ToastLength.Short).Show();
        }
    }
}