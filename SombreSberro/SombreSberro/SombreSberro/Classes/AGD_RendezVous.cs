using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SombreSberro.Classes
{
    public class AGD_RendezVous
    {
        public int id { get; set; }
        public int idRdv { get; set; }
        public String dateRendezVous { get; set; }
        public String libelle { get; set; }
        public int duree { get; set; }
        public bool urgence { get; set; }
        public bool estAnnule { get; set; }
        public bool estVu { get; set; }
        public int userId { get; set; }
        public int patientId { get; set; }

        public string isAccepted
        {
            get
            {
                if (estVu)
                {
                    return "yes";
                }
                else
                {
                    return "no";
                }
            }
        }
        public Color isUrgence
        {
            get
            {
                if (urgence)
                {
                    return Color.Red;
                }
                else
                {
                    return Color.DodgerBlue;
                }
            }
        }
    }

    public class RDV : AGD_RendezVous
    {
        public PAT_Patient patient { get; set; }
    }
}
