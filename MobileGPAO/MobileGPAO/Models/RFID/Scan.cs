using System.Xml.Serialization;

namespace GPAOnGo.RFID
{
    [XmlRoot(App.XML_RFID_PARA)]
    public class Scan
    {

        const string XML_RFID_PARA_LIBRE = "rfid_para_libre";
        const string XML_RFID_PARA_OCCUR = "rfid_para_occur";
        const string XML_RFID_PARA_DUREE = "rfid_para_duree";
        const string XML_RFID_PARA_LEVEL = "rfid_para_level";

        public Scan(bool scan_libre, int duree, int nb_occur, int level)
        {
            this.A_Scan_libre = scan_libre;
            this.A_Duree = duree;
            this.A_Nb_occur = nb_occur;
            this.A_Level = level;
        }

        public Scan():this(false, 0, 0, 0)
        {
        }
        
        bool A_Scan_libre { get; set; }
        int A_Duree { get; set; }
        int A_Nb_occur { get; set; }
        int A_Level { get; set; }

        [XmlElement(XML_RFID_PARA_LIBRE)]
        public bool Scan_libre
        {
            get { return A_Scan_libre; }
            set { A_Scan_libre = value; }
        }

        [XmlElement(XML_RFID_PARA_DUREE)]
        public int Duree
        {
            get { return A_Duree; }
            set { A_Duree = value; }
        }

        [XmlElement(XML_RFID_PARA_OCCUR)]
        public int Nb_occur
        {
            get { return A_Nb_occur; }
            set { A_Nb_occur = value; }
        }

        [XmlElement(XML_RFID_PARA_LEVEL)]
        public int Level
        {
            get { return A_Level; }
            set { A_Level = value; }
        }
    }
}
