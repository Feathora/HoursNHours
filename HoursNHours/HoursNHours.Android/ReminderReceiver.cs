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
    public class ReminderReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            var channel = new NotificationChannel("reminders", "Reminder", NotificationImportance.Default);
            channel.EnableVibration(true);
            channel.LockscreenVisibility = NotificationVisibility.Public;

            var manager = (NotificationManager)context.GetSystemService(Context.NotificationService);
            manager.CreateNotificationChannel(channel);

            var builder = new Notification.Builder(context, "reminders").SetContentTitle("Waar heb je vandaag aan gewerkt?").SetAutoCancel(true).SetSmallIcon(Resource.Drawable.tab_about);

            var openAppIntent = new Intent(context, typeof(MainActivity));
            var pendingIntent = PendingIntent.GetActivity(context, 0, openAppIntent, 0);
            builder.SetContentIntent(pendingIntent);

            builder.AddAction(new Notification.Action.Builder(null, "V++", PendingIntent.GetBroadcast(context, 0, new Intent(context, typeof(HoursReceiver)).SetAction("V++"), 0)).Build());
            builder.AddAction(new Notification.Action.Builder(null, "HKU", PendingIntent.GetBroadcast(context, 0, new Intent(context, typeof(HoursReceiver)).SetAction("HKU"), 0)).Build());

            manager.Notify(1, builder.Build());

            var reminderIntent = new Intent(Application.Context, typeof(ReminderReceiver));
            pendingIntent = PendingIntent.GetBroadcast(Application.Context, 0, reminderIntent, PendingIntentFlags.CancelCurrent);

            var reminderTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 18, 0, 0, DateTimeKind.Local).ToUniversalTime();
            if (reminderTime < DateTime.UtcNow) reminderTime = reminderTime.AddDays(1.0);

            long reminderMillis = (long)(reminderTime - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;

            (Application.Context.GetSystemService(Context.AlarmService) as AlarmManager).Set(AlarmType.RtcWakeup, reminderMillis, pendingIntent);
        }
    }
}