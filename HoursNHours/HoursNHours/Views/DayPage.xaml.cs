using HoursNHours.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HoursNHours.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DayPage : ContentPage
    {
        private List<Day> Days;
        public Day Today { get; set; }

        private string TodayString => DateTime.Now.ToString("d", CultureInfo.InvariantCulture);

        public DayPage()
        {
            Days = JsonConvert.DeserializeObject<List<Day>>(Preferences.Get("Days", "[]"));
            Today = Days.SingleOrDefault(d => d.Date == TodayString);
            if(Today == null)
            {
                Today = new Day { Date = TodayString };
                Days.Add(Today);
            }

            InitializeComponent();

            VPlusPlusEntry.Text = Today.VPlusPlusHours.ToString();
            HKUEntry.Text = Today.HKUHours.ToString();
        }

        private void Submit_Clicked(object sender, EventArgs e)
        {
            Today.VPlusPlusHours = int.Parse(VPlusPlusEntry.Text);
            Today.HKUHours = int.Parse(HKUEntry.Text);

            Preferences.Set("Days", JsonConvert.SerializeObject(Days));

            DependencyService.Get<IHoursToast>().ShowToast("Hours submitted");
        }
    }
}