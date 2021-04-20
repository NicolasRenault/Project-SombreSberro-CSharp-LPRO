using System;
using System.Collections.Generic;
using System.Text;

namespace SombreSberro.Classes
{
    enum AtcdType_Enum
    {
        Personnel = 0,
        Familial = 1,
        Risque = 2,
        Allergie = 3,
    }
    class PAT_Atcd
    {
        public int id { get; set; }
        public int idAtcd { get; set; }
        public int patientId { get; set; }
        public AtcdType_Enum atcdType { get; set; }
        public String libelle { get; set; }

        public PAT_Atcd(int id, int idAtcd, int patientId, AtcdType_Enum atcdType, string libelle)
        {
            this.id = id;
            this.idAtcd = idAtcd;
            this.patientId = patientId;
            this.atcdType = atcdType;
            this.libelle = libelle;
        }
    }
}
