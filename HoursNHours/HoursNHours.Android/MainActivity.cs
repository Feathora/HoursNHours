using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace HoursNHours.Droid
{
    [Activity(Label = "HoursNHours", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            global::Xamarin.Forms.Forms.SetFlags("CollectionView_Experimental");
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            var reminderIntent = new Intent(this, typeof(ReminderReceiver));
            var pendingIntent = PendingIntent.GetBroadcast(ApplicationContext, 0, reminderIntent, PendingIntentFlags.CancelCurrent);

            AlarmManager manager = ApplicationContext.GetSystemService(AlarmService) as AlarmManager;

            var reminderTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 18, 0, 0, DateTimeKind.Local).ToUniversalTime();
            if (reminderTime < DateTime.UtcNow) reminderTime = reminderTime.AddDays(1.0);

            long reminderMillis = (long)(reminderTime - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;

            manager.Set(AlarmType.RtcWakeup, reminderMillis, pendingIntent);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}