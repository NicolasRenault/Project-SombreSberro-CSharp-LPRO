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
	public partial class IMCView : ContentPage
	{
		public IMCView ()
		{
			InitializeComponent ();
		}

        private void Entry_Digit(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                bool isValid = e.NewTextValue.ToCharArray().All(x => char.IsDigit(x));

                ((Entry)sender).Text = isValid ? e.NewTextValue : e.NewTextValue.Remove(e.NewTextValue.Length - 1);
            }
        }

        private void Validate_IMC_Clicked(object sender, EventArgs e)
        {
            if (EntryIMCHeight.Text != null && EntryIMCWeight.Text != null)
            {
                float height = float.Parse(EntryIMCHeight.Text) / 100;
                float weight = Int32.Parse(EntryIMCWeight.Text);

                float result = weight / (height * height);

                string textResult = result.ToString();

                if (result < 18.5)
                {
                    textResult += "\nInsuffisance pondérale (maigreur)";
                } else if (result < 25)
                {
                    textResult += "\nCorpulence normale";
                } else if (result < 30)
                {
                    textResult += "\nSurpoids";
                } else if (result < 40)
                {
                    textResult += "\nObésité modérée";
                } else
                {
                    textResult += "\nObésité sévère";
                }

                LabelResultIMC.Text = textResult;
                LabelResultIMC.FontSize = 50;
            }
            else
            {
                LabelResultIMC.Text = "Champs incorects";
                LabelResultIMC.FontSize = 30;
            }
        }
    }
}