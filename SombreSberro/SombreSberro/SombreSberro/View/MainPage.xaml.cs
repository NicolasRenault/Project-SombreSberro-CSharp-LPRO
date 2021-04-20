using Newtonsoft.Json;
using RestSharp;
using SombreSberro.Classes;
using SombreSberro.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;

namespace SombreSberro
{

    public partial class MainPage : CarouselPage
    {
        private int notepadlocker = 0;
        private int CurrentIndex = 2;

        System.Timers.Timer timer = new System.Timers.Timer();
        User currentUser = (User)Application.Current.Properties["currentUser"];
        RestClient restclient = (RestClient)Application.Current.Properties["restclient"];
        public static List<RDV> listRDV = new List<RDV>();
        public string isAccpeted;


        public MainPage()
        {
            InitializeComponent();
            this.CurrentPage = this.Children[CurrentIndex];

            initListPatient();
            initCalendar();
            initNotePad();
            
        }

        private void CarousselPageName_CurrentPageChanged(object sender, EventArgs e)
        {
            var index = this.Children.IndexOf(this.CurrentPage);

            var ind = CurrentIndex.ToString();

            if (CurrentIndex == 3)
            {
                Notepad_Disappearing(sender, e);
            }

            if (index > -1)
            {
                CurrentIndex = index;
            }
        }

        //Init
        private void initNotePad()
        {
            var scroll = new ScrollView()
            {
                BackgroundColor = Color.LightGray,
                Padding = 1,
                Margin = new Thickness(10, 30, 10, 10),
                VerticalOptions = LayoutOptions.Start
            };
            scroll.Content = Editor_Notpad;
            EditorStackNotepad.Children.Add(scroll);

            Notepad_Appearing(new object(), new EventArgs());
            notepadlocker = 1;
        }

        private void initCalendar()
        {
            initPage();
        }

        private void initListPatient()
        {
            timer.Interval = 500;
            timer.Elapsed += new ElapsedEventHandler(onTimerTick);
        }

        //Notepad
        private void Notepad_Appearing(object sender, EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string fileName = System.IO.Path.Combine(path, "notepad.txt");
            if (System.IO.File.Exists(fileName))
            {
                Editor_Notpad.Text = System.IO.File.ReadAllText(fileName);
            }
            notepadlocker = 1;
        }

        private void Notepad_Disappearing(object sender, EventArgs e)
        {
            if (notepadlocker != 0)
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                string fileName = System.IO.Path.Combine(path, "notepad.txt");
                System.IO.File.WriteAllText(fileName, Editor_Notpad.Text);
            }
        }

        //Calculatrice
        private async void CalculatorButtonRVC_Clicked(object sender, EventArgs e)
        {
                await Navigation.PushModalAsync(new RCVView(), true);
        }

        private async void CalculatorButtonGrocesse_Clicked(object sender, EventArgs e)
        {
                await Navigation.PushModalAsync(new PregnancyView(), true);
        }

        private async void CalculatorButtonIMC_Clicked(object sender, EventArgs e)
        {
                await Navigation.PushModalAsync(new IMCView(), true);
        }

        //ListPatient
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

        //Calendar
        private async void initPage()
        {
            var request = new RestRequest("/api/v1/RendezVous", Method.GET);
            request.AddHeader("Authorization", "Bearer " + currentUser.Token);
            Task<IRestResponse<string>> asyncResponse = restclient.ExecuteAsync<string>(request);
            IRestResponse response = await asyncResponse;

            BasicResponse<RDV> rdv = JsonConvert.DeserializeObject<BasicResponse<RDV>>(response.Content);
            if (rdv != null)
            {
                listRDVPatient.ItemsSource = rdv.Items;
                this.BindingContext = rdv.Items;
                listRDV = rdv.Items;
            }
            else
            {
                await DisplayAlert("Message", "ça marche pas bro", null, "ok");
            }

        }

        private void datePickerAppointment_DateSelected(object sender, DateChangedEventArgs e)
        {
            string[] dateFormat = { datePickerAppointment.Date.Year.ToString(), datePickerAppointment.Date.Month.ToString(), datePickerAppointment.Date.Day.ToString() };
            if (dateFormat[1].Length == 1)
            {
                dateFormat[1] = "0" + dateFormat[1];
            }
            if (dateFormat[2].Length == 1)
            {
                dateFormat[2] = "0" + dateFormat[2];
            }
            testDatePicker.Text = dateFormat[0] + "-" + dateFormat[1] + "-" + dateFormat[2];
            string dateToSearch = dateFormat[0] + "-" + dateFormat[1] + "-" + dateFormat[2];
            var result = from rdvItem in listRDV
                         where rdvItem.dateRendezVous.Contains(dateToSearch)
                         select rdvItem;
            listRDVPatient.ItemsSource = result;
        }

        private async void listRDVPatient_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushModalAsync(new AppointmentDetails((RDV)listRDVPatient.SelectedItem), true);
        }
    }
}
