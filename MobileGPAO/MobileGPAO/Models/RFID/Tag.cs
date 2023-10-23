using System.Collections.Generic;

namespace GPAOnGo.RFID
{
    public class Tag
    {
        public Tag(string id, string param1, string param2)
        {
            this.A_ID = id;
            this.A_Param1 = param1;
            this.A_Param2 = param2;
        }

        // ID du tag
        string A_ID {get; set;}

        // autres paramètres....
        string A_Param1 { get; set; }
        string A_Param2 { get; set; }

        // Accès aux variabless
        public string ID
        {
            get { return A_ID; }
            set { A_ID = value; }
        }

        public string Param1
        {
            get { return A_Param1; }
            set { A_Param1 = value; }
        }
        public string Param2
        {
            get { return A_Param2; }
            set { A_Param2 = value; }
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Tag objAsTag = obj as Tag;
            if (objAsTag == null) return false;
            else return Equals(objAsTag);
        }
        
        public bool Equals(Tag other)
        {
            if (other == null) return false;
            return (A_ID.Equals(other.A_ID));
        }

        public bool IsInListe(List<Tag> liste)
        {
            bool estDedans = false;

            foreach(Tag t in liste)
            {
                if(this == t)
                {
                    estDedans = true;
                }
            }

            return estDedans;
        }
    }
}
