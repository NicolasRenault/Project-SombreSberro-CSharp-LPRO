using System;
using System.Collections.Generic;
using System.Text;

namespace SombreSberro.Classes
{
    class PAT_Ordo
    {
        public int id { get; set; }
        public int idOrdo { get; set; }
        public int patientId { get; set; }
        public String dateOrdo { get; set; }
        public String prescriptions { get; set; }
        public bool ald { get; set; }

        public PAT_Ordo(int id, int idOrdo, int patientId, string dateOrdo, string prescriptions, bool ald)
        {
            this.id = id;
            this.idOrdo = idOrdo;
            this.patientId = patientId;
            this.dateOrdo = dateOrdo;
            this.prescriptions = prescriptions;
            this.ald = ald;
        }
    }
}
