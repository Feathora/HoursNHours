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

namespace HoursNHours.Droid
{
    [BroadcastReceiver]
    [IntentFilter(new string[] { Intent.ActionBootCompleted })]
    public class BootReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            if(intent.Action == Intent.ActionBootCompleted)
            {
                var reminderIntent = new Intent(Application.Context, typeof(ReminderReceiver));
                var pendingIntent = PendingIntent.GetBroadcast(Application.Context, 0, reminderIntent, PendingIntentFlags.CancelCurrent);

                AlarmManager manager = Application.Context.GetSystemService(Context.AlarmService) as AlarmManager;

                var reminderTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 18, 0, 0, DateTimeKind.Local).ToUniversalTime();
                if (reminderTime < DateTime.UtcNow) reminderTime = reminderTime.AddDays(1.0);

                long reminderMillis = (long)(reminderTime - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;

                manager.Set(AlarmType.RtcWakeup, reminderMillis, pendingIntent);
            }
        }
    }
}