using Newtonsoft.Json;
using RestSharp;
using SombreSberro.Classes;
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
    public partial class Connexion : ContentPage
    {

        RestClient restclient = new RestClient("http://51.75.30.4/MediPocket");
        User currentLogin = new User();
        AuthenticateModel userLogin;
        public Connexion()
        {
            InitializeComponent();
        }

        private async void ButtonConnexion_Clicked(object sender, EventArgs e)
        {
            {
                var Identifiant = EntryLogin.Text;
                var MotDePasse = EntryMDP.Text;

                AuthenticateModel authenticateModel = new AuthenticateModel();
                authenticateModel.email = Identifiant;
                authenticateModel.password = MotDePasse;

                var resquest = new RestRequest("api/v1/OAuth/authenticate", Method.POST);
                resquest.RequestFormat = DataFormat.Json;
                resquest.AddJsonBody(authenticateModel);

                Task<string> asyncresponse = restclient.PostAsync<string>(resquest);
                string reponse = await asyncresponse;


                if (!reponse.Contains("incorrect") && !reponse.Contains("closed"))
                {
                    currentLogin = JsonConvert.DeserializeObject<User>(reponse);
                    Application.Current.Properties["currentUser"] = currentLogin;
                    Application.Current.Properties["restclient"] = restclient;
                    //await DisplayAlert("Resultat", reponse, null, "ok");

                    await Navigation.PushModalAsync(new MainPage(), true);

                }
                else
                {
                    await DisplayAlert("Resultat", "login erreur", null, "ok");
                }
            }
        }
    }
}
