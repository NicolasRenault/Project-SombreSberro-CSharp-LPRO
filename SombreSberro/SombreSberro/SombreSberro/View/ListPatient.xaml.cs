using Newtonsoft.Json;
using RestSharp;
using SombreSberro.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SombreSberro.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListPatient : TabbedPage
    {
        System.Timers.Timer timer = new System.Timers.Timer();
        User currentUser = (User)Application.Current.Properties["currentUser"];
        RestClient restclient = (RestClient)Application.Current.Properties["restclient"];


        public ListPatient()
        {
            InitializeComponent();
             timer.Interval = 500;
            timer.Elapsed += new ElapsedEventHandler(onTimerTick);
        }

        private void onTimerTick(object sender, ElapsedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                timer.Stop();

                var resquest = new RestRequest("/api/v1/Patients", Method.GET); 
                resquest.AddHeader("Authorization", "Bearer " + currentUser.Token);
                Task<IRestResponse<string>> asyncresponse = restclient.ExecuteAsync<string>(resquest);
                IRestResponse response = await asyncresponse;

                

                //await DisplayAlert("resultat ping", response.ToString() , null, "ok");
                BasicResponse<PAT_Patient> patients = JsonConvert.DeserializeObject<BasicResponse<PAT_Patient>>(response.Content);
                var query = from item in patients.Items
                            where item.nom.Contains(EntryPatient.Text) 
                            select item;

                listPatient.ItemsSource = query;
                //listPatient.BindingContext = patients.Items.ToString();


            });
        }

        async private void ListPatient_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
           PAT_Patient patient = (PAT_Patient)listPatient.SelectedItem;
           await Navigation.PushModalAsync(new PatientView(patient), true);
        }

        private void EntryPatient_TextChanged(object sender, TextChangedEventArgs e)
        {
            timer.Stop();
            timer.Start();
        }

    }
}