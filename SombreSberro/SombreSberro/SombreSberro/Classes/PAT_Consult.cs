using System;
using System.Collections.Generic;
using System.Text;

namespace SombreSberro.Classes
{
    class PAT_Consult
    {
        public int id { get; set; }
        public int idConsult { get; set; }
        public int patientId { get; set; }
        public String dateConsult { get; set; }
        public String motif { get; set; }
        public String examinClinique { get; set; }
        public int poids { get; set; }
        public int taille { get; set; }

        public PAT_Consult(int id, int idConsult, int patientId, string dateConsult, string motif, string examinClinique, int poids, int taille)
        {
            this.id = id;
            this.idConsult = idConsult;
            this.patientId = patientId;
            this.dateConsult = dateConsult;
            this.motif = motif;
            this.examinClinique = examinClinique;
            this.poids = poids;
            this.taille = taille;
        }
    }
}
