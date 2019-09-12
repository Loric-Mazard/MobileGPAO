using System;
using System.Collections.Generic;
using System.Text;

namespace RFID_Android_XAML.RFID
{
    public class Scan
    {
        public Scan()
        {
            this.scan_libre = false;
            this.duree = 0;
            this.nb_occur = 0;
        }

        public Scan(bool scan_libre, int duree, int nb_occur)
        {
            this.scan_libre = scan_libre;
            this.duree = duree;
            this.nb_occur = nb_occur;
        }

        // type de scan (manuel ou automatique)
        bool scan_libre { get; set; }

        // durée du scan
        int duree { get; set; }

        // nombre de scan à effectuer
        int nb_occur { get; set; }

        // Accès aux variables
        public bool Scan_libre
        {
            get { return scan_libre; }
            set { scan_libre = value; }
        }

        public int Duree
        {
            get { return duree; }
            set { duree = value; }
        }

        public int Nb_occur
        {
            get { return nb_occur; }
            set { nb_occur = value; }
        }
    }
}
