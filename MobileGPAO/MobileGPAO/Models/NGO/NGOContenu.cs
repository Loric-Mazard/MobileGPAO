using System.Xml.Serialization;

namespace GPAOnGo.NGO
{
    public class NGOContenu
    {
        const string ValeurContenu = "Valeur";
        const string TypeContenu = "TypeContenu";
        const string TitreContenu = "Titre";
        const string OrdreContenu = "Ordre";

        const string ListeNGOChamps = "ListeNGOChamps";
        const string NGOChamps = "NGOChamps";
        public NGOContenu()
        {
            this.A_valeur = string.Empty;
            this.A_type = NGOObjectItemIHM.enumItemIHMType.NA;
            this.A_titre = string.Empty;
            this.A_ordre = string.Empty;
            this.A_champs = new ListeNGOChamps();
            
        }

        string A_valeur { get; set; }
        NGOObjectItemIHM.enumItemIHMType A_type { get; set; }
        string A_titre { get; set; }
        string A_ordre { get; set; }
        ListeNGOChamps A_champs { get; set; }

        [XmlAttribute(ValeurContenu)]
        public string Valeur
        {
            get { return A_valeur; }
            set { A_valeur = value; }
        }

        [XmlAttribute(TypeContenu)]
        public NGOObjectItemIHM.enumItemIHMType Type
        {
            get { return A_type; }
            set { A_type = value; }
        }

        [XmlAttribute(TitreContenu)]
        public string Titre
        {
            get { return A_titre; }
            set { A_titre = value; }
        }

        [XmlAttribute(OrdreContenu)]
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
    }
}
