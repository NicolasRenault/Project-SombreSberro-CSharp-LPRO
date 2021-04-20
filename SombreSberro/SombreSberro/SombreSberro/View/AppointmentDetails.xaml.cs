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
    public partial class AppointmentDetails : ContentPage
    {
        private RDV currentRDV;
        public AppointmentDetails(RDV rdv)
        {
            InitializeComponent();
            this.currentRDV = rdv;
            initViewDetailAppoitment();
        }

        private void initViewDetailAppoitment()
        {
            patientNameAppointmentLabel.Text = currentRDV.patient.nom;
            string tmpUrgence = currentRDV.urgence == true ? "Yes" : "No";
            patientIsUrgentLabel.Text = tmpUrgence;
            patientDateAppointmentLabel.Text = currentRDV.dateRendezVous.Replace('T', ' ');
            patientTimeAppointementLabel.Text = currentRDV.duree.ToString() + "min";
            patientDetailsAppointementLabel.Text = currentRDV.libelle;
        }
    }
}
