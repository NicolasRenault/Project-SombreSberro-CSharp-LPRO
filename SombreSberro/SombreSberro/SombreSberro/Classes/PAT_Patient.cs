using System;
using System.Collections.Generic;
using System.Text;

namespace SombreSberro.Classes
{
    public enum Sexe
    {
        Homme = 0,
        Femme = 1,
        Autre = 2,
    }
    public class PAT_Patient
    {
        public int id { get; set; }
        public int idPatient { get; set; }
        public String nom { get; set; }
        public String prenom { get; set; }
        public String civilite { get; set; }
        public Sexe sexe { get; set; }
        public String dateNaissance { get; set; }
        public String adresse { get; set; }
        public String codePostal { get; set; }
        public String ville { get; set; }
        public String telFixe { get; set; }
        public String telMobile { get; set; }
        public String email { get; set; }

        public PAT_Patient(int id, int idPatient, string nom, string prenom, string civilite, Sexe sexe, string dateNaissance, string adresse, string codePostal, string ville, string telFixe, string telMobile, string email)
        {
            this.id = id;
            this.idPatient = idPatient;
            this.nom = nom;
            this.prenom = prenom;
            this.civilite = civilite;
            this.sexe = sexe;
            this.dateNaissance = dateNaissance;
            this.adresse = adresse;
            this.codePostal = codePostal;
            this.ville = ville;
            this.telFixe = telFixe;
            this.telMobile = telMobile;
            this.email = email;
        }
    }
}
