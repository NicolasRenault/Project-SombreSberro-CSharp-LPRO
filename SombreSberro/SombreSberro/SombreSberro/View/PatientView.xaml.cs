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

    public partial class PatientView : ContentPage
    {
        User currentUser = (User)Application.Current.Properties["currentUser"];
        RestClient restclient = (RestClient)Application.Current.Properties["restclient"];
        private PAT_Patient patientLoc;

        public PatientView(PAT_Patient patient)
        {
            InitializeComponent();
            this.BindingContext = patient;
            patientLoc = patient;
        }

        async private void ContentPagePatient_Appearing(object sender, EventArgs e)
        {
            var resquest = new RestRequest("/api/v1/Antecedent/{id}", Method.GET);
            resquest.AddHeader("Authorization", "Bearer " + currentUser.Token);
            Task<IRestResponse<string>> asyncresponse = restclient.ExecuteAsync<string>(resquest);
            IRestResponse response = await asyncresponse;

            BasicResponse<PAT_Atcd> atcd = JsonConvert.DeserializeObject<BasicResponse<PAT_Atcd>>(response.Content);
            listAtcd.ItemsSource = atcd.Items.Where(t => t.atcdType == AtcdType_Enum.Personnel).Where(t => t.patientId == patientLoc.id);
            listAtcd.ItemsSource = atcd.Items.Where(t => t.atcdType == AtcdType_Enum.Familial).Where(t => t.patientId == patientLoc.id);
            listAtcd.ItemsSource = atcd.Items.Where(t => t.atcdType == AtcdType_Enum.Risque).Where(t => t.patientId == patientLoc.id);
            listAtcd.ItemsSource = atcd.Items.Where(t => t.atcdType == AtcdType_Enum.Allergie).Where(t => t.patientId == patientLoc.id);
        }
    }
}
