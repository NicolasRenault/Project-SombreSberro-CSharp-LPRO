using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SombreSberro.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PregnancyView : ContentPage
	{
		public PregnancyView ()
		{
			InitializeComponent ();
		}

        private void DatePickerPregnancy_DateSelected(object sender, DateChangedEventArgs e)
        {
            DateTime date = DatePickerPregnancy.Date;
            date = date.AddDays(280);

            LabelResultPregnancy.Text = "Date prévisoir de l'accouchement :\n" + date.ToShortDateString() + "\nFélicitation, tu as bien ken ! \nOn peut l'appeler Fabrice ?";
        }
    }
}