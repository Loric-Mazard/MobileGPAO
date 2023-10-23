using System.Xml.Serialization;
using GPAOnGo.WEBSERVICE;

namespace GPAOnGo.NGO
{
    [XmlRoot("NGOCheck")]
    public class NGOCheck
    {
        const string Result = "Result";
        const string RowidReturn = "Rowid";
        const string Error = "Error";
        public NGOCheck()
        {
            this.A_result = string.Empty;
            this.A_error = string.Empty;
            this.A_rowid = string.Empty;
            this.oRestServiceStatut = new RestServiceStatut();
        }

        public NGOCheck(RestServiceStatut pRestServiceStatut) : this()
        {
            this.oRestServiceStatut = pRestServiceStatut;
        }

        string A_result { get; set; }
        string A_error { get; set; }
        string A_rowid { get; set; }
        RestServiceStatut oRestServiceStatut { get; set; }


        [XmlAttribute(Result)]
        public string Resultat
        {
            get { return A_result; }
            set { A_result = value; }
        }

        [XmlAttribute(Error)]
        public string Erreur
        {
            get { return A_error; }
            set { A_error = value; }
        }

        [XmlAttribute(RowidReturn)]
        public string Rowid
        {
            get { return A_rowid; }
            set { A_rowid = value; }
        }
    }
}
