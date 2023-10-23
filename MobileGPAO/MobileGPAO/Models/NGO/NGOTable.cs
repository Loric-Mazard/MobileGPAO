using System.Xml.Serialization;

namespace GPAOnGo.NGO
{
    public class NGOTable
    {
        const string NomTable = "NomTable";

        const string ListeNGOTable = "ListeNGOTable";
        const string Table = "NGOTable";

        public NGOTable()
        {
            this.A_nom = string.Empty;
            this.A_tables = new ListeNGOTable();
        }

        string A_nom { get; set; }
        ListeNGOTable A_tables { get; set; }

        [XmlAttribute(NomTable)]
        public string Nom
        {
            get { return A_nom; }
            set { A_nom = value; }
        }

        [XmlArray(ListeNGOTable)]
        [XmlArrayItem(Table, Type = typeof(NGOTable))]
        public ListeNGOTable ListeTable
        {
            get { return A_tables; }
            set { A_tables = value; }
        }

    }
}
