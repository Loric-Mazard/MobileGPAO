using System.Xml.Serialization;

namespace GPAOnGo.NGO
{
    public class NGOObject
    {
        const string CodeObject = "CodeObject";
        const string NomObject = "NomObject";
        const string TypeObject = "TypeObject";
        const string OrdreObject = "Ordre";

        const string ListeNGOChamps = "ListeNGOChamps";
        const string NGOChamps = "NGOChamps";

        public NGOObject()
        {
            this.A_num = string.Empty;
            this.A_nom = string.Empty;
            this.A_type = NGOObjectItemIHM.enumItemIHMType.NA;
            this.A_ordre = string.Empty;
            this.A_champs = new ListeNGOChamps();
        }

        string A_num { get; set; }
        string A_nom { get; set; }
        NGOObjectItemIHM.enumItemIHMType A_type { get; set; }
        string A_ordre { get; set; }
        ListeNGOChamps A_champs { get; set; }

        [XmlAttribute(CodeObject)]
        public string Num
        {
            get { return A_num; }
            set { A_num = value; }
        }

        [XmlAttribute(NomObject)]
        public string Nom
        {
            get { return A_nom; }
            set { A_nom = value; }
        }

        [XmlAttribute(TypeObject)]
        public NGOObjectItemIHM.enumItemIHMType Type
        {
            get { return A_type; }
            set { A_type = value; }
        }

        [XmlAttribute(OrdreObject)]
        public string Ordre
        {
            get { return A_ordre; }
            set { A_ordre = value; }
        }

        [XmlArray(ListeNGOChamps)]
        [XmlArrayItem(NGOChamps, Type = typeof(NGOChamps))]
        public ListeNGOChamps ListeChamps
        {
            get { return A_champs; }
            set { A_champs = value; }
        }

        public NGOObject Recherche(string vStringRecherche)
        {
            NGOObject o = new NGOObject();
            o.Num = this.Num;
            o.Nom = this.Nom;
            o.Ordre = this.Ordre;
            o.Type = this.Type;
            foreach(NGOChamps n in this.ListeChamps)
            {
                if (n.Contains(vStringRecherche))
                {
                    o.ListeChamps.Add(n);
                }
            }
            return o;
        }
    }
}
