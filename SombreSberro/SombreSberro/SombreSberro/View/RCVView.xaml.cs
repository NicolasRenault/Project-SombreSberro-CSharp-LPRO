using Score;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SombreSberro.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RCVView : ContentPage
	{
        private ScoreESC scoreESC = new ScoreESC();
		public RCVView()
		{
			InitializeComponent();
		}

        private void Validate_Clicked_RCV(object sender, EventArgs e)
        {
            if (PickerSexe.SelectedIndex != null && SwitchTabac.TabIndex != null && EntryAge.Text != null && EntryPAS.Text != null && EntryCT.Text != null)
            {
                CultureInfo ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();

                int sexe = PickerSexe.SelectedIndex + 1;
                int tabac = SwitchTabac.TabIndex;
                int age = Int32.Parse(EntryAge.Text);
                int pas = Int32.Parse(EntryPAS.Text);
                float ct = float.Parse(EntryCT.Text, CultureInfo.InvariantCulture.NumberFormat);

                int result = scoreESC.calculateScoreCVD(sexe, tabac, age, pas, ct);
                LabelResultRCV.Text = result.ToString();
                LabelResultRCV.TextColor = scoreESC.getColorFromScore(result);
                LabelResultRCV.FontSize = 100;
            }
            else
            {
                LabelResultRCV.Text = "Champs incorects";
                LabelResultRCV.TextColor = Color.Black;
                LabelResultRCV.FontSize = 30;
            }
            
        }

        private void Entry_Digit(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                bool isValid = e.NewTextValue.ToCharArray().All(x => char.IsDigit(x) || x == '.');

                ((Entry)sender).Text = isValid ? e.NewTextValue : e.NewTextValue.Remove(e.NewTextValue.Length - 1);
            }
        }
    }
}