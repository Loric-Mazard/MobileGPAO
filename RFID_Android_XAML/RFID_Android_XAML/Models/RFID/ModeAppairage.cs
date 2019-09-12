using System;
using System.Collections.Generic;
using System.Text;

namespace RFID_Android_XAML.RFID
{
    public class ModeAppairage
    {
        public ModeAppairage(int pvaleur, string pdescription, int ptype)
        {
            this.valeur = pvaleur;
            this.description = pdescription;
            this.type = ptype;
        }

        int valeur { get; set; }

        string description { get; set; }

        int type { get; set; }

        // Accès aux variables
        public int Valeur
        {
            get { return valeur; }
            set { valeur = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public int Type
        {
            get { return type; }
            set { type = value; }
        }
    }
}
