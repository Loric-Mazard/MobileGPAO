using System;
using System.Collections.Generic;
using System.Text;

namespace RFID_Android_XAML.RFID
{
    public class Tag
    {
        public Tag(string id, string param1, string param2)
        {
            this.id = id;
            this.param1 = param1;
            this.param2 = param2;
        }

        // ID du tag
        string id;

        // autres paramètres....
        string param1;
        string param2;

        // Accès aux variables
        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        public string Param1
        {
            get { return param1; }
            set { param1 = value; }
        }

        public string Param2
        {
            get { return param2; }
            set { param2 = value; }
        }

        public bool isInListe(List<Tag> liste)
        {
            bool estDedans = false;

            foreach(Tag t in liste)
            {
                if(this.ID == t.ID)
                {
                    estDedans = true;
                }
            }

            return estDedans;
        }
    }
}
